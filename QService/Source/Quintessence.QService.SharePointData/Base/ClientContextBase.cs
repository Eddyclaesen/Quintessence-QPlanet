using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using Quintessence.QService.Core.Configuration;

namespace Quintessence.QService.SharePointData.Base
{
    public abstract class ClientContextBase<TContext> : ClientContext
    {
        private readonly Dictionary<Type, IEnumerable<PropertyInfo>> _registeredTypes = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        protected ClientContextBase(IConfiguration configuration)
            : base(ParseConnectionString(configuration.GetConnectionStringConfiguration<TContext>())["url"])
        {
            var connectionSettings = ParseConnectionString(configuration.GetConnectionStringConfiguration<TContext>());

            Credentials = new NetworkCredential(connectionSettings["username"], connectionSettings["password"], connectionSettings["domain"]);
        }

        /// <summary>
        /// Parses the connection string.
        /// </summary>
        /// <param name="connectionStringConfiguration">The connection string.</param>
        /// <returns></returns>
        private static Dictionary<string, string> ParseConnectionString(string connectionStringConfiguration)
        {
            var connectionstring = ConfigurationManager.ConnectionStrings[connectionStringConfiguration].ConnectionString;
            return
                connectionstring.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => part.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(keyValue => keyValue[0].ToLowerInvariant(), keyValue => keyValue[1]);
        }

        protected IEnumerable<TEntity> List<TEntity>(params Expression<Func<TEntity, object>>[] filters) where TEntity : new()
        {
            var website = DetermineWebsite<TEntity>();

            var list = website.Lists.GetById(Guid.Parse(GetTableName<TEntity>()));
            Load(list);
            ExecuteQuery();

            var query = new CamlQuery { ViewXml = CreateViewXml(filters) };

            var listItemCollection = list.GetItems(query);

            Load(listItemCollection);
            ExecuteQuery();

            return listItemCollection.Select(ConvertListItem<TEntity>);
        }

        protected int CreateItem<TEntity>(Dictionary<string, object> propertyValues = null) where TEntity : new()
        {
            var website = DetermineWebsite<TEntity>();

            var list = website.Lists.GetById(Guid.Parse(GetTableName<TEntity>()));
            Load(list);
            ExecuteQuery();

            var item = list.AddItem(new ListItemCreationInformation());

            item.Update();

            ExecuteQuery();

            return item.Id;
        }

        private TEntity ConvertListItem<TEntity>(ListItem listItem)
            where TEntity : new()
        {
            var propertyInfos = ColumnPropertyInfos<TEntity>();

            var entity = new TEntity();

            foreach (var propertyInfo in propertyInfos)
            {
                var column = propertyInfo.GetCustomAttribute<ColumnAttribute>();

                if (!listItem.FieldValues.ContainsKey(column.Name))
                    continue;

                var value = listItem.FieldValues[column.Name];

                if (value != null)
                {
                    if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        propertyInfo.SetValue(entity, Convert.ChangeType(value, propertyInfo.PropertyType.GetGenericArguments()[0]));
                    else
                        propertyInfo.SetValue(entity, Convert.ChangeType(value, propertyInfo.PropertyType));
                }

            }

            return entity;
        }

        private IEnumerable<PropertyInfo> ColumnPropertyInfos<TEntity>()
        {
            if (!_registeredTypes.ContainsKey(typeof(TEntity)))
                _registeredTypes.Add(typeof(TEntity), typeof(TEntity).GetProperties().Where(pi => pi.GetCustomAttributes(true).OfType<ColumnAttribute>().Any()));
            return _registeredTypes[typeof(TEntity)];
        }

        private string GetTableName<TEntity>()
        {
            var tableAttribute = typeof(TEntity).GetCustomAttribute<TableAttribute>();

            if (tableAttribute != null)
                return tableAttribute.Name;

            throw new ArgumentNullException(string.Format("Unable to determine table name for type '{0}'", typeof(TEntity).Name));
        }

        private string GetSchemaName<TEntity>()
        {
            var tableAttribute = typeof(TEntity).GetCustomAttribute<TableAttribute>();

            if (tableAttribute != null)
                return tableAttribute.Schema;

            throw new ArgumentNullException(string.Format("Unable to determine table name for type '{0}'", typeof(TEntity).Name));
        }

        private Web DetermineWebsite<TEntity>() where TEntity : new()
        {
            var schemaName = GetSchemaName<TEntity>();

            var website = Web;

            if (!string.IsNullOrWhiteSpace(schemaName))
            {
                var subsiteNames = schemaName.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var subsiteName in subsiteNames)
                {
                    Load(website.Webs);
                    ExecuteQuery();

                    foreach (var sub in website.Webs)
                    {
                        if (sub.Title == subsiteName)
                        {
                            website = sub;
                            break;
                        }
                    }
                }
            }
            return website;
        }

        private string CreateViewXml<TEntity>(params Expression<Func<TEntity, object>>[] filters)
        {
            var viewXml = new XElement("View");

            var viewFields = new XElement("ViewFields");
            viewFields.Add(ColumnPropertyInfos<TEntity>().Select(c =>
            {
                var fieldRef = new XElement("FieldRef");
                var column = c.GetCustomAttribute<ColumnAttribute>();
                fieldRef.Add(new XAttribute("Name", column.Name));
                return fieldRef;
            }));
            viewXml.Add(viewFields);

            if (filters.Length > 0)
            {
                var query = new XElement("Query");
                var where = new XElement("Where");

                query.Add(where);

                where.Add(filters
                    .OfType<LambdaExpression>()
                    .Where(filter => filter.Body.NodeType == ExpressionType.Convert
                                     && filter.Body is UnaryExpression
                                     && ((UnaryExpression)filter.Body).Operand is BinaryExpression)
                    .Select(filter =>
                    {
                        var binaryExpression = ((UnaryExpression)filter.Body).Operand as BinaryExpression;

                        var eq = new XElement("Eq");

                        var fieldRef = new XElement("FieldRef", new XAttribute("Name", GetMemberName(binaryExpression)));
                        var value = new XElement("Value", new XAttribute("Type", "Text"));
                        value.Add(GetValue(binaryExpression));

                        eq.Add(fieldRef);
                        eq.Add(value);

                        return eq;
                    }));

                viewXml.Add(query);
            }

            return viewXml.ToString();
        }

        private string GetMemberName(BinaryExpression binaryExpression)
        {
            if (binaryExpression.Left is MemberExpression)
                return ((MemberExpression)binaryExpression.Left).Member.GetCustomAttribute<ColumnAttribute>().Name;
            else if (binaryExpression.Right is MemberExpression)
                return ((MemberExpression)binaryExpression.Right).Member.GetCustomAttribute<ColumnAttribute>().Name;
            else
                throw new InvalidExpressionException("Expression not supported.");
        }

        private object GetValue(BinaryExpression binaryExpression)
        {
            Expression valueExpression = null;
            if (binaryExpression.Left is MemberExpression)
                valueExpression = binaryExpression.Right;
            else if (binaryExpression.Right is MemberExpression)
                valueExpression = binaryExpression.Left;
            else
                throw new InvalidExpressionException("Expression not supported.");

            return Expression.Lambda(valueExpression).Compile().DynamicInvoke();
        }
    }
}

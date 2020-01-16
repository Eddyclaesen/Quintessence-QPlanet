using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Fin;

namespace Quintessence.QPlanet.Webshell.Infrastructure.ModelBinders
{
    public class EditAccountantInvoicingEntryModelModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var formKeys = controllerContext.HttpContext.Request.Form.AllKeys;
            var dictionary = new Dictionary<string, string>();
            foreach (var formKey in formKeys)
            {
                var originalKey = formKey;
                var formattedKey = formKey.Split('.').Last();
                var formValue = controllerContext.HttpContext.Request.Form[originalKey];

                dictionary.Add(formattedKey, formValue);
            }

            //Do casting.
            var typeName = dictionary["DetailType"];

            if (typeName.ToLower() == typeof(EditAccountantProjectCandidateInvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantProjectCandidateInvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() ==
                     typeof (EditAccountantProjectCandidateCategoryType1InvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantProjectCandidateCategoryType1InvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() ==
                     typeof (EditAccountantProjectCandidateCategoryType2InvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantProjectCandidateCategoryType2InvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() ==
                     typeof (EditAccountantProjectCandidateCategoryType3InvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantProjectCandidateCategoryType3InvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() == typeof (EditAccountantProjectProductInvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantProjectProductInvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() ==
                     typeof (EditAccountantAcdcProjectFixedPriceInvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantAcdcProjectFixedPriceInvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() ==
                     typeof(EditAccountantConsultancyProjectFixedPriceInvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantConsultancyProjectFixedPriceInvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() ==
                     typeof(EditAccountantProductSheetEntryInvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantProductSheetEntryInvoicingEntryModel>(dictionary);
            }

            if (typeName.ToLower() ==
                     typeof(EditAccountantTimesheetEntryInvoicingEntryModel).FullName.ToLower())
            {
                return SetModelProperties<EditAccountantTimesheetEntryInvoicingEntryModel>(dictionary);
            }

            throw new InvalidCastException("Can't cast form to appropriate invoicing model.");
        }

        private T SetModelProperties<T>(Dictionary<string, string> formDictionary)
        {
            var model = Activator.CreateInstance<T>();
            var type = model.GetType();
            foreach (var property in type.GetProperties())
            {
                if (formDictionary.ContainsKey(property.Name) && (property.GetMethod != null && property.SetMethod != null))
                {
                    var propertyType = property.PropertyType.IsGenericType ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType;
                    var value = string.IsNullOrEmpty(formDictionary[property.Name]) ? null : TypeDescriptor.GetConverter(propertyType).ConvertFromString(formDictionary[property.Name]);
                    property.SetValue(model, value);
                }
            }
            return model;
        }
    }
}
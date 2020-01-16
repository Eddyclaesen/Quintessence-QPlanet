using System;
using System.Linq;

namespace Quintessence.QService.QueryModel.Base
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumMemberNameAttribute : Attribute
    {
        private readonly string _name;

        public EnumMemberNameAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public static string GetName<TEnum>(TEnum e)
        {
            var members = typeof(TEnum).GetMember(e.ToString());

            if (members.Length == 0)
                return e.ToString();

            var member = members[0];

            var attributes = member.GetCustomAttributes(typeof(EnumMemberNameAttribute), false).OfType<EnumMemberNameAttribute>().ToList();
            var attribute = attributes.FirstOrDefault();

            return attribute == null 
                ? e.ToString() 
                : attribute.Name;
        }
    }
}

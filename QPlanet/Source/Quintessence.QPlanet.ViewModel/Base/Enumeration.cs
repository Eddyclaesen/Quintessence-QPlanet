using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Quintessence.QPlanet.ViewModel.Base
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration()
        {
        }

        protected Enumeration(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return ((IEnumerable<FieldInfo>)typeof(T).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public)).Select<FieldInfo, object>((Func<FieldInfo, object>)(f => f.GetValue((object)null))).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            Enumeration enumeration = obj as Enumeration;
            return !(enumeration == (Enumeration)null) && this.GetType().Equals(obj.GetType()) & this.Id.Equals(enumeration.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(Enumeration firstValue, Enumeration secondValue)
        {
            if ((object)firstValue == (object)secondValue)
                return true;
            return (object)firstValue != null && (object)secondValue != null && firstValue.Id == secondValue.Id;
        }

        public static bool operator !=(Enumeration firstValue, Enumeration secondValue)
        {
            return !(firstValue == secondValue);
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            return Math.Abs(firstValue.Id - secondValue.Id);
        }

        public static T FromId<T>(int id) where T : Enumeration
        {
            return Enumeration.Parse<T, int>(id, "Id", (Func<T, bool>)(item => item.Id == id));
        }

        public static T FromName<T>(string name) where T : Enumeration
        {
            return Enumeration.Parse<T, string>(name, "Name", (Func<T, bool>)(item => item.Name == name));
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            T obj = Enumeration.GetAll<T>().FirstOrDefault<T>(predicate);
            if (!((Enumeration)obj == (Enumeration)null))
                return obj;
            throw new InvalidOperationException(string.Format("'{0}' is not a valid {1} in {2}", (object)value, (object)description, (object)typeof(T)));
        }

        public int CompareTo(object other)
        {
            return this.Id.CompareTo(((Enumeration)other).Id);
        }
    }

}
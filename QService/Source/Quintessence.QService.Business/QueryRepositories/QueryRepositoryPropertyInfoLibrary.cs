using System;
using System.Collections.Generic;
using System.Reflection;

namespace Quintessence.QService.Business.QueryRepositories
{
    public static class QueryRepositoryPropertyInfoLibrary
    {
        private static readonly Dictionary<Type, PropertyInfo> PropertyInfos = new Dictionary<Type, PropertyInfo>();

        //Create a lock object since multi threaded environment
        private static readonly object PropertyInfosLock = new object();

        public static PropertyInfo GetPropertyInfo<TEntity>(Func<PropertyInfo> func)
        {
            //Check if not already there
            if (!PropertyInfos.ContainsKey(typeof(TEntity)))
                //Create lock
                lock (PropertyInfosLock)
                    //Extra check if not created during previous check.
                    if (!PropertyInfos.ContainsKey(typeof(TEntity)))
                        //Execute function to retrieve property info
                        PropertyInfos[typeof(TEntity)] = func.Invoke();

            return PropertyInfos[typeof(TEntity)];
        }
    }
}

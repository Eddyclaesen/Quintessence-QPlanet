using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Business.Base;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;
using Quintessence.CulturalFit.Infra.Exceptions;
using Quintessence.CulturalFit.Infra.Logging;

namespace Quintessence.CulturalFit.Business
{
    public class CrmRepository : BaseRepository, ICrmRepository
    {
        #region Constructor(s)
        public CrmRepository(IUnityContainer container)
            : base(container)
        {
        }
        #endregion

        #region Retrieve methods
        /// <summary>
        /// Retrieves the associate.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        /// <exception cref="Quintessence.CulturalFit.Infra.Exceptions.BusinessException"></exception>
        public Associate RetrieveAssociate(string username)
        {
            using (new DurationLog())
            {
                try
                {
                    Associate associate;

                    using (var context = CreateContext())
                    {
                        var query = from a in context.Associates
                                    where a.UserName == username
                                    select a;

                        associate = query.FirstOrDefault(a => a.UserName == username);
                    }

                    return associate;
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to retrieve associate with username {0}.", username);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        public Project RetrieveProject(Guid id)
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.Projects.FirstOrDefault(p => p.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to retrieve Project with id {0}.", id);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        public Associate RetrieveAssociateByUserId(Guid userId)
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.Associates.FirstOrDefault(a => a.UserId == userId);
                    }
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to retrieve Associate with id {0}.", userId);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        #endregion

    }
}

using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq;
using System.Linq;
using Quintessence.CulturalFit.Data.Exceptions;
using Quintessence.CulturalFit.Data.Interfaces;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;
using Quintessence.CulturalFit.Infra.Configuration;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.Data
{
    public class QContext : DbContext, IQContext
    {
        public QContext(IConfiguration configuration)
            : base(configuration.GetConnectionStringConfiguration<IQContext>())
        {
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Associate>().Map(a => a.ToTable("CulturalFitAssociateView"));
            modelBuilder.Entity<Contact>().Map(a => a.ToTable("CrmReplicationContact"));
        }

        public IDbSet<TheoremList> TheoremLists { get; set; }

        public IDbSet<TheoremListRequest> TheoremListRequests { get; set; }

        public IDbSet<Language> Languages { get; set; }

        public IDbSet<Theorem> Theorems { get; set; }

        public IDbSet<TheoremTranslation> TheoremTranslations { get; set; }

        public IDbSet<Associate> Associates { get; set; }

        public IDbSet<EmailTemplate> EmailTemplates { get; set; }

        public IDbSet<Setting> Settings { get; set; }

        public IDbSet<Project2CrmProject> Project2CrmProjects { get; set; }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Contact> Contacts { get; set; }

        #region Save Methods

        public TheoremListRequest Save(TheoremListRequest theoremListRequest)
        {
            return CreateOrUpdate(TheoremListRequests, theoremListRequest, storeTheoremListRequest =>
            {
                storeTheoremListRequest.ContactId = theoremListRequest.ContactId;
                storeTheoremListRequest.RequestDate = theoremListRequest.RequestDate;
                storeTheoremListRequest.TheoremListRequestTypeId = theoremListRequest.TheoremListRequestTypeId;
                storeTheoremListRequest.VerificationCode = theoremListRequest.VerificationCode;
                storeTheoremListRequest.Deadline = theoremListRequest.Deadline;
                storeTheoremListRequest.IsMailSent = theoremListRequest.IsMailSent;

                storeTheoremListRequest.CrmEmailId = theoremListRequest.CrmEmailId;
                storeTheoremListRequest.CandidateId = theoremListRequest.CandidateId;

                foreach (var theoremList in theoremListRequest.TheoremLists)
                    storeTheoremListRequest.AddTheoremList(Save(theoremList));

            });
        }

        public TheoremList Save(TheoremList theoremList)
        {
            return CreateOrUpdate(TheoremLists, theoremList, storeTheoremList =>
            {
                storeTheoremList.TheoremListTypeId = theoremList.TheoremListTypeId;
                storeTheoremList.VerificationCode = theoremList.VerificationCode;
                storeTheoremList.IsCompleted = theoremList.IsCompleted;

                foreach (var theorem in theoremList.Theorems)
                    storeTheoremList.AddTheorem(Save(theorem));
            });
        }

        private Theorem Save(Theorem theorem)
        {
            return CreateOrUpdate(Theorems, theorem, storeTheorem =>
            {
                storeTheorem.IsLeastApplicable = theorem.IsLeastApplicable;
                storeTheorem.IsMostApplicable = theorem.IsMostApplicable;

                foreach (var theoremTranslation in theorem.TheoremTranslations)
                    storeTheorem.AddTheoremTranslation(Save(theoremTranslation));
            });
        }

        private TheoremTranslation Save(TheoremTranslation theoremTranslation)
        {
            return CreateOrUpdate(TheoremTranslations, theoremTranslation, storeTheoremTranslation =>
            {
                storeTheoremTranslation.LanguageId = theoremTranslation.LanguageId;
                storeTheoremTranslation.Quote = theoremTranslation.Quote;
            });
        }

        private TEntity CreateOrUpdate<TEntity>(IDbSet<TEntity> set, TEntity entity, Action<TEntity> update)
            where TEntity : class, IEntity
        {
            LogManager.LogTrace("Query datastore for {0} with Id {1} in set {0}s.", typeof(TEntity).Name, entity.Id);
            var storeEntity = set.FirstOrDefault(tlr => tlr.Id == entity.Id);

            if (storeEntity != null && Entry(storeEntity).State != EntityState.Unchanged)
                return storeEntity;

            var domainUser = string.Format(@"{0}\{1}", Environment.UserDomainName, Environment.UserName);

            if (storeEntity == null)
            {
                LogManager.LogTrace("Entity {0} with Id {1} not found. Prepare for insert.", typeof(TEntity).Name, entity.Id);
                storeEntity = set.Create();
                storeEntity.Id = entity.Id;
                storeEntity.Audit = new Audit { CreatedBy = domainUser, CreatedOn = DateTime.Now, IsDeleted = false, VersionId = Guid.NewGuid() };
                set.Add(storeEntity);
            }
            else
            {
                LogManager.LogTrace("Entity {0} with Id {1} found. Prepare for update.", typeof(TEntity).Name, entity.Id);
                LogManager.LogTrace("Verifying version. Store: {0} - candidate: {1}.", storeEntity.Audit.VersionId, entity.Audit.VersionId);
                if (storeEntity.Audit.VersionId != entity.Audit.VersionId)
                    throw new EntityHasBeenModifiedBusinessException<IEntity>(storeEntity);

                storeEntity.Audit.ModifiedBy = domainUser;
                storeEntity.Audit.ModifiedOn = DateTime.Now;
                storeEntity.Audit.VersionId = Guid.NewGuid();
            }

            update(storeEntity);

            return storeEntity;
        }

        #endregion
    }
}

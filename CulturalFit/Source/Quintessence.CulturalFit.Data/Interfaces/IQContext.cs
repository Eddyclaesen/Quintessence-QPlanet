using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;

namespace Quintessence.CulturalFit.Data.Interfaces
{
    public interface IQContext: IDisposable
    {
        IDbSet<TheoremList> TheoremLists { get; set; }

        IDbSet<TheoremListRequest> TheoremListRequests { get; set; }

        IDbSet<Language> Languages { get; set; }

        IDbSet<Theorem> Theorems { get; set; }
        
        IDbSet<TheoremTranslation> TheoremTranslations { get; set; }
        
        IDbSet<Associate> Associates { get; set; }

        IDbSet<EmailTemplate> EmailTemplates { get; set; }

        IDbSet<Setting> Settings { get; set; }

        IDbSet<Project2CrmProject> Project2CrmProjects { get; set; }

        IDbSet<Project> Projects { get; set; }

        IDbSet<Contact> Contacts { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;
        
        int SaveChanges();

        TheoremListRequest Save(TheoremListRequest theoremListRequest);

        TheoremList Save(TheoremList theoremList);

    }
}

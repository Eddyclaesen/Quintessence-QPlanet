using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Prm;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : IPrmCommandContext
    {
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<ProjectPlan> ProjectPlans { get; set; }
        public IDbSet<ProjectPlanPhase> ProjectPlanPhases { get; set; }
        public IDbSet<ProjectPlanPhaseEntry> ProjectPlanPhaseEntries { get; set; }
        public IDbSet<ProjectPlanPhaseActivity> ProjectPlanPhaseActivities { get; set; }
        public IDbSet<ProjectPlanPhaseProduct> ProjectPlanPhaseProducts { get; set; }
        public IDbSet<ProjectCandidate> ProjectCandidates { get; set; }
        public IDbSet<Project2CrmProject> Project2CrmProjects { get; set; }
        public IDbSet<ProjectType> ProjectTypes { get; set; }
        public IDbSet<ProjectCategoryDetail> ProjectCategoryDetails { get; set; }
        public IDbSet<ProjectTypeCategory> ProjectTypeCategories { get; set; }
        public IDbSet<ProjectCategoryDetail2DictionaryIndicator> ProjectCategoryDetail2DictionaryIndicators { get; set; }
        public IDbSet<ProjectRole> ProjectRoles { get; set; }
        public IDbSet<ProjectRoleTranslation> ProjectRoleTranslations { get; set; }
        public IDbSet<ProjectCategoryDetail2SimulationCombination> ProjectCategoryDetail2SimulationCombinations { get; set; }
        public IDbSet<ProjectCategoryDetail2Competence2Combination> ProjectCategoryDetail2Competence2Combinations { get; set; }
        public IDbSet<ProjectRole2DictionaryLevel> ProjectRole2DictionaryLevels { get; set; }
        public IDbSet<ProjectPriceIndex> ProjectPriceIndices { get; set; }
        public IDbSet<ProjectFixedPrice> ProjectFixedPrices { get; set; }
        public IDbSet<TimesheetEntry> TimesheetEntries { get; set; }
        public IDbSet<ProjectTypeCategoryDefaultValue> ProjectTypeCategoryDefaultValues { get; set; }
        public IDbSet<ProjectCategoryDetailType1> ProjectCategoryDetailTypes1 { get; set; }
        public IDbSet<ProjectCategoryDetailType2> ProjectCategoryDetailTypes2 { get; set; }
        public IDbSet<ProjectCategoryDetailType3> ProjectCategoryDetailTypes3 { get; set; }
        public IDbSet<ProductSheetEntry> ProductSheetEntries { get; set; }
        public IDbSet<SubProject> SubProjects { get; set; }
        public IDbSet<ProjectCandidateCategoryDetailType1> ProjectCandidateCategoryDetailTypes1 { get; set; }
        public IDbSet<ProjectCandidateCategoryDetailType2> ProjectCandidateCategoryDetailTypes2 { get; set; }
        public IDbSet<ProjectCandidateCategoryDetailType3> ProjectCandidateCategoryDetailTypes3 { get; set; }
        public IDbSet<ProjectType2ProjectTypeCategory> ProjectType2ProjectTypeCategories { get; set; }
        public IDbSet<ProjectCandidateIndicatorSimulationScore> ProjectCandidateIndicatorSimulationScores { get; set; }
        public IDbSet<ProjectCandidateCompetenceSimulationScore> ProjectCandidateCompetenceSimulationScores { get; set; }
        public IDbSet<ProjectRole2DictionaryIndicator> ProjectRole2DictionaryIndicators { get; set; }
        public IDbSet<ProjectCandidateClusterScore> ProjectCandidateClusterScores { get; set; }
        public IDbSet<ProjectCandidateCompetenceScore> ProjectCandidateCompetenceScores { get; set; }
        public IDbSet<ProjectCandidateIndicatorScore> ProjectCandidateIndicatorScores { get; set; }
        public IDbSet<ProjectCandidateResume> ProjectCandidateResumes { get; set; }
        public IDbSet<ProjectCandidateResumeField> ProjectCandidateResumeFields { get; set; }
        public IDbSet<ProjectTypeCategoryUnitPrice> ProjectTypeCategoryUnitPrices { get; set; }
        public IDbSet<ProjectTypeCategoryLevel> ProjectTypeCategoryLevels { get; set; }
        public IDbSet<Proposal> Proposals { get; set; }
        public IDbSet<FrameworkAgreement> FrameworkAgreements { get; set; }
        public IDbSet<ProjectCandidateReportRecipient> ProjectCandidateReportRecipients { get; set; }
        public IDbSet<ProjectReportRecipient> ProjectReportRecipients { get; set; }
        public IDbSet<ReportStatus> ReportStatuses { get; set; }
        public IDbSet<ProjectDocumentMetadata> ProjectDocumentMetadatas { get; set; }
        public IDbSet<ProjectDna> ProjectDnas { get; set; }
        public IDbSet<ProjectDnaCommercialTranslation> ProjectDnaCommercialTranslations { get; set; }
        public IDbSet<ProjectDna2ProjectDnaType> ProjectDna2ProjectDnaTypes { get; set; }
        public IDbSet<ProjectProduct> ProjectProducts { get; set; }
        public IDbSet<ProjectEvaluation> ProjectEvaluations { get; set; }
        public IDbSet<EvaluationForm> EvaluationForms { get; set; }
        public IDbSet<EvaluationFormAcdc> EvaluationFormsAcdc { get; set; }
        public IDbSet<EvaluationFormCoaching> EvaluationFormsCoaching { get; set; }
        public IDbSet<EvaluationFormCustomProjects> EvaluationFormsCustomProjects { get; set; }
        public IDbSet<ProjectComplaint> ProjectComplaints { get; set; }
        public IDbSet<ComplaintType> ComplaintTypes { get; set; }
        public IDbSet<TheoremListRequest> TheoremListRequests { get; set; }
        public IDbSet<TheoremList> TheoremLists { get; set; }
        public IDbSet<Theorem> Theorems { get; set; }
        public IDbSet<TheoremTranslation> TheoremTranslations { get; set; }
        public IDbSet<TheoremListTemplate> TheoremListTemplates { get; set; }
        public IDbSet<TheoremTemplate> TheoremTemplates { get; set; }
        public IDbSet<TheoremTemplateTranslation> TheoremTemplateTranslations { get; set; }
        public IDbSet<ProjectDna2CrmPerson> ProjectDna2CrmPersons { get; set; }
        public IDbSet<ProjectRevenueDistribution> ProjectRevenueDistributions { get; set; }

        public string GenerateType3Code(int crmParticipantId, string firstName, string lastName, string languageCode, string categoryCode)
        {
            var query = Database.SqlQuery<string>("External_GenerateType3Code {0}, {1}, {2}, {3}, {4}", 
                                                            crmParticipantId,
                                                            firstName, 
                                                            lastName,
                                                            languageCode,
                                                            categoryCode);
            return query.Single();
        }

        public void DeleteProjectCandidateClusteScores(Guid projectCandidateId, string userName)
        {
            Database.ExecuteSqlCommand("ProjectCandidate_DeleteClusterScores {0}, {1}", projectCandidateId, userName);
        }

        public void DeleteProjectCandidateCompetenceScores(Guid projectCandidateId, string userName)
        {
            Database.ExecuteSqlCommand("ProjectCandidate_DeleteCompetenceScores {0}, {1}", projectCandidateId, userName);
        }

        public void DeleteProjectCandidateIndicatorScores(Guid projectCandidateId, string userName)
        {
            Database.ExecuteSqlCommand("ProjectCandidate_DeleteIndicatorScores {0}, {1}", projectCandidateId, userName);
        }

        public void DeleteProjectCandidateCompetenceSimulationScore(Guid id)
        {
            Database.ExecuteSqlCommand("ProjectCandidate_DeleteCompetenceSimulationScore {0}", id);
        }

        public void DeleteProjectCandidateFocusedIndicatorSimulationScores(Guid id)
        {
            Database.ExecuteSqlCommand("ProjectCandidate_DeleteFocusedIndicatorSimulationScore {0}", id);
        }

        public void DeleteProjectCandidateStandardIndicatorSimulationScores(Guid id)
        {
            Database.ExecuteSqlCommand("ProjectCandidate_DeleteStandardIndicatorSimulationScore {0}", id);
        }
    }
}

using System;
using System.Data.Entity;
using Quintessence.QService.DataModel.Prm;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    /// <summary>
    /// Interface for the Project Management data context
    /// </summary>
    public interface IPrmCommandContext : IQuintessenceCommandContext
    {
        IDbSet<Project> Projects { get; set; }
        IDbSet<ProjectPlan> ProjectPlans { get; set; }
        IDbSet<ProjectPlanPhase> ProjectPlanPhases { get; set; }
        IDbSet<ProjectPlanPhaseEntry> ProjectPlanPhaseEntries { get; set; }
        IDbSet<ProjectPlanPhaseActivity> ProjectPlanPhaseActivities { get; set; }
        IDbSet<ProjectPlanPhaseProduct> ProjectPlanPhaseProducts { get; set; }
        IDbSet<ProjectCandidate> ProjectCandidates { get; set; }
        IDbSet<Project2CrmProject> Project2CrmProjects { get; set; }
        IDbSet<ProjectType> ProjectTypes { get; set; }
        IDbSet<ProjectCategoryDetail> ProjectCategoryDetails { get; set; }
        IDbSet<ProjectTypeCategory> ProjectTypeCategories { get; set; }
        IDbSet<ProjectCategoryDetail2DictionaryIndicator> ProjectCategoryDetail2DictionaryIndicators { get; set; }
        IDbSet<ProjectRole> ProjectRoles { get; set; }
        IDbSet<ProjectRoleTranslation> ProjectRoleTranslations { get; set; }
        IDbSet<ProjectCategoryDetail2SimulationCombination> ProjectCategoryDetail2SimulationCombinations { get; set; }
        IDbSet<ProjectCategoryDetail2Competence2Combination> ProjectCategoryDetail2Competence2Combinations { get; set; }
        IDbSet<ProjectRole2DictionaryLevel> ProjectRole2DictionaryLevels { get; set; }
        IDbSet<ProjectPriceIndex> ProjectPriceIndices { get; set; }
        IDbSet<ProjectFixedPrice> ProjectFixedPrices { get; set; }
        IDbSet<TimesheetEntry> TimesheetEntries { get; set; }
        IDbSet<ProjectTypeCategoryDefaultValue> ProjectTypeCategoryDefaultValues { get; set; }
        IDbSet<ProjectCategoryDetailType1> ProjectCategoryDetailTypes1 { get; set; }
        IDbSet<ProjectCategoryDetailType2> ProjectCategoryDetailTypes2 { get; set; }
        IDbSet<ProjectCategoryDetailType3> ProjectCategoryDetailTypes3 { get; set; }
        IDbSet<ProductSheetEntry> ProductSheetEntries { get; set; }
        IDbSet<SubProject> SubProjects { get; set; }
        IDbSet<ProjectCandidateCategoryDetailType1> ProjectCandidateCategoryDetailTypes1 { get; set; }
        IDbSet<ProjectCandidateCategoryDetailType2> ProjectCandidateCategoryDetailTypes2 { get; set; }
        IDbSet<ProjectCandidateCategoryDetailType3> ProjectCandidateCategoryDetailTypes3 { get; set; }
        IDbSet<ProjectType2ProjectTypeCategory> ProjectType2ProjectTypeCategories { get; set; }
        IDbSet<ProjectCandidateIndicatorSimulationScore> ProjectCandidateIndicatorSimulationScores { get; set; }
        IDbSet<ProjectCandidateCompetenceSimulationScore> ProjectCandidateCompetenceSimulationScores { get; set; }
        IDbSet<ProjectRole2DictionaryIndicator> ProjectRole2DictionaryIndicators { get; set; }
        IDbSet<ProjectCandidateClusterScore> ProjectCandidateClusterScores { get; set; }
        IDbSet<ProjectCandidateCompetenceScore> ProjectCandidateCompetenceScores { get; set; }
        IDbSet<ProjectCandidateIndicatorScore> ProjectCandidateIndicatorScores { get; set; }
        IDbSet<ProjectCandidateResume> ProjectCandidateResumes { get; set; }
        IDbSet<ProjectCandidateResumeField> ProjectCandidateResumeFields { get; set; }
        IDbSet<ProjectTypeCategoryUnitPrice> ProjectTypeCategoryUnitPrices { get; set; }
        IDbSet<ProjectTypeCategoryLevel> ProjectTypeCategoryLevels { get; set; }
        IDbSet<Proposal> Proposals { get; set; }
        IDbSet<FrameworkAgreement> FrameworkAgreements { get; set; }
        IDbSet<ProjectCandidateReportRecipient> ProjectCandidateReportRecipients { get; set; }
        IDbSet<ProjectReportRecipient> ProjectReportRecipients { get; set; }
        IDbSet<ReportStatus> ReportStatuses { get; set; }
        IDbSet<ProjectDocumentMetadata> ProjectDocumentMetadatas { get; set; }
        IDbSet<ProjectDna> ProjectDnas { get; set; }
        IDbSet<ProjectDnaCommercialTranslation> ProjectDnaCommercialTranslations { get; set; }
        IDbSet<ProjectDna2ProjectDnaType> ProjectDna2ProjectDnaTypes { get; set; }
        IDbSet<ProjectProduct> ProjectProducts { get; set; }
        IDbSet<ProjectEvaluation> ProjectEvaluations { get; set; }
        IDbSet<EvaluationForm> EvaluationForms { get; set; }
        IDbSet<EvaluationFormAcdc> EvaluationFormsAcdc { get; set; }
        IDbSet<EvaluationFormCoaching> EvaluationFormsCoaching { get; set; }
        IDbSet<EvaluationFormCustomProjects> EvaluationFormsCustomProjects { get; set; }
        IDbSet<ProjectComplaint> ProjectComplaints { get; set; }
        IDbSet<ComplaintType> ComplaintTypes { get; set; }
        IDbSet<TheoremListRequest> TheoremListRequests { get; set; }
        IDbSet<TheoremList> TheoremLists { get; set; }
        IDbSet<Theorem> Theorems { get; set; }
        IDbSet<TheoremTranslation> TheoremTranslations { get; set; }
        IDbSet<TheoremListTemplate> TheoremListTemplates { get; set; }
        IDbSet<TheoremTemplate> TheoremTemplates { get; set; }
        IDbSet<TheoremTemplateTranslation> TheoremTemplateTranslations { get; set; }
        IDbSet<ProjectDna2CrmPerson> ProjectDna2CrmPersons { get; set; }
        IDbSet<ProjectRevenueDistribution> ProjectRevenueDistributions { get; set; }

        string GenerateType3Code(int crmParticipantId, string firstName, string lastName, string languageCode, string categoryCode);
        void DeleteProjectCandidateClusteScores(Guid projectCandidateId, string userName);
        void DeleteProjectCandidateCompetenceScores(Guid projectCandidateId, string userName);
        void DeleteProjectCandidateIndicatorScores(Guid projectCandidateId, string userName);
        void DeleteProjectCandidateCompetenceSimulationScore(Guid id);
        void DeleteProjectCandidateFocusedIndicatorSimulationScores(Guid id);
        void DeleteProjectCandidateStandardIndicatorSimulationScores(Guid id);
    }
}

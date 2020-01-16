using System;
using System.Collections.Generic;
using Quintessence.QService.DataModel.Prm;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface IProjectManagementCommandRepository : ICommandRepository
    {
        Project PrepareProject(Guid projectTypeId, string projectName);
        Guid Save(Project project);
        void LinkProject2CrmProject(Guid projectId, int crmProjectId);
        void UnlinkProject2CrmProject(Guid id, int crmProjectId);
        Project RetrieveProject(Guid id);
        List<Project2CrmProject> ListProject2CrmProject(Guid projectId);
        List<ProjectCategoryDetail> ListProjectCategoryDetails(Guid projectId);
        ProjectCategoryDetail PrepareProjectCategoryDetail(Guid projectTypeCategoryId);
        ProjectCategoryDetail RetrieveProjectCategoryDetail(Guid id);
        Guid Save(ProjectCategoryDetail projectCategoryDetail);
        ProjectTypeCategory RetrieveProjectTypeCategory(Guid id);
        ProjectCategoryDetail2DictionaryIndicator RetrieveProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetailId, Guid dictionaryIndicatorId);
        ProjectCategoryDetail2DictionaryIndicator RetrieveProjectCategoryDetail2DictionaryIndicator(Guid id);
        void LinkProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetailId, Guid dictionaryIndicatorId, bool isDefinedByRole = false, bool isStandard = false, bool isDistinctive = false);
        void DeleteProjectCategoryDetail2DictionaryIndicator(Guid id);
        void DeleteProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetailId, Guid dictionaryIndicatorId);
        ProjectCategoryDetail2SimulationCombination RetrieveProjectCategoryDetail2SimulationCombination(Guid projectCategoryDetailId, Guid simulationCombinationId);
        void LinkProjectCategoryDetail2SimulationCombination(Guid projectCategoryDetailId, Guid simulationCombinationId);
        void DeleteProjectCategoryDetail2SimulationCombination(Guid projectCategoryDetail2SimulationCombinationId);
        ProjectCategoryDetail2Competence2Combination RetrieveProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId, Guid dictionaryCompetenceId, Guid simulationCombinationId);
        ProjectCategoryDetail2Competence2Combination PrepareProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId);
        void Save(ProjectCategoryDetail2Competence2Combination projectCategoryDetail2Competence2Combination);
        void Delete(ProjectCategoryDetail2Competence2Combination projectCategoryDetail2Competence2Combination);
        void UnlinkProjectRole2DictionaryLevel(Guid projectRoleId, Guid dictionaryLevelId);
        void LinkProjectRole2DictionaryLevel(Guid projectRoleId, Guid selectedLevelId);
        List<ProjectCategoryDetail2Competence2Combination> ListProjectCategoryDetail2Competence2CombinationByCompetence(Guid projectCategoryDetailId, Guid? dictionaryCompetenceId);
       
        ProjectCandidate RetrieveProjectCandidate(Guid id);
        ProjectCandidateCategoryDetailType1 ProjectCandidateCategoryType1(Guid id);
        ProjectCandidateCategoryDetailType2 ProjectCandidateCategoryType2(Guid id);
        ProjectCandidateCategoryDetailType3 ProjectCandidateCategoryType3(Guid id);
        ProjectProduct RetrieveProjectProduct(Guid id);
        TimesheetEntry RetrieveTimeSheetEntry(Guid id);
        ProductSheetEntry RetrieveProductSheetEntry(Guid id);
        ProjectFixedPrice RetrieveProjectFixedPrice(Guid id);

        ProjectCandidate RetrieveProject2Candidate(Guid projectId, Guid candidateId);
        void LinkProject2Candidate(Guid projectId, Guid candidateId);
        void DeleteProjectCategoryDetail2DictionaryLevel(Guid id);
        void UnlinkProject2Candidate(Guid projectId, Guid candidateId);
        void LinkSubProject(Guid projectId, Guid subProjectId, Guid? projectCandidateId = null);
        Guid LinkProjectCandidateCategoryDetailType1(Guid projectCandidateId, Guid projectId, Guid projectCategoryDetailTypeId, decimal invoiceAmount, DateTime? scheduledDate = null);
        Guid LinkProjectCandidateCategoryDetailType2(Guid projectCandidateId, Guid projectId, Guid projectCategoryDetailTypeId, decimal invoiceAmount);
        Guid LinkProjectCandidateCategoryDetailType3(Guid projectCandidateId, Guid projectId, Guid projectCategoryDetailTypeId, string loginCode, DateTime? scheduledDate, decimal invoiceAmount);
        void LinkProjectType2ProjectTypeCategory(Guid projectTypeId, Guid projectTypeCategoryId, bool isMain);
        void CreateSubcategoryDefaultValues(Guid id, int subcategoryType);
        TProjectCandidateCategoryDetailType RetrieveProjectCandidateCategoryDetailType<TProjectCandidateCategoryDetailType>(Guid Id)
            where TProjectCandidateCategoryDetailType : class, IProjectCandidateCategoryDetailType;
        void Save(IProjectCandidateCategoryDetailType projectCandidateCategoryDetailType);
        void LinkProjectRole2DictionaryIndicator(Guid projectRoleId, Guid dictionaryIndicatorId);
        void UpdateProjectRoleDictionaryIndicatorNorm(Guid projectRoleId, Guid dictionaryIndicatorId, int norm);
        void DeleteProjectRoleDictionaryIndicator(Guid projectRoleId, Guid dictionaryIndicatorId);
        void DeleteProjectCandidateScores(Guid projectCandidateId);
        void DeleteProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId);
        void DeleteProjectCategoryDetailCompetenceSimulations(Guid projectCategoryDetailId);
        void DeleteProjectCategoryDetailSimulationCombinations(Guid projectCategoryDetailId);
        Proposal PrepareProposal();
        string GenerateType3Code(int crmParticipantId, string firstName, string lastName, string languageCode, string categoryCode);
        void MarkDocumentAsImportant(Guid projectId, Guid uniqueId);
        void UnmarkDocumentAsImportant(Guid projectId, Guid uniqueId);
        List<ProjectDna2ProjectDnaType> ListProjectDnaProjectTypes(Guid projectDnaId);
        void DeleteProjectDna2ProjectDnaType(Guid id);
        ProjectTypeCategoryUnitPrice RetrieveProjectTypeCategoryUnitPrice(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId);
        ProjectTypeCategoryUnitPrice PrepareProjectTypeCategoryUnitPrice(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId, decimal unitPrice);
        ProjectEvaluation RetrieveProjectEvaluationByCrmProject(int crmProjectId);
        List<ProjectCategoryDetail2DictionaryIndicator> ListProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId);
        Guid CreateTheoremListRequestContact(int contactId, Guid projectId, int crmEmailId, DateTime deadline, int theoremListRequestTypeId, string description);
        List<TheoremList> RetrieveTheoremListsByTheoremListRequestId(Guid id);
        void MarkProjectCategoryDetail2DictionaryIndicatorAsStandard(Guid id);
        void MarkProjectCategoryDetail2DictionaryIndicatorAsDistinctive(Guid id);
        void UnMarkProjectCategoryDetail2DictionaryIndicator(Guid id);
        List<ProjectDna2CrmPerson> ListProjectDnaProjectContactPersons(Guid projectDnaId);
        void DeleteProjectDna2CrmContactPerson(Guid id);
        Guid CreateTheoremListRequestCandidate(int contactId, Guid projectId, Guid candidateId, DateTime deadline);
        void DeleteProjectCandidateCategoryDetail(List<Guid> candidateIds, Guid projectCategoryDetailId);
        void DeleteProjectCandidatesCompetenceScoring(IEnumerable<Guid> projectCandidateIds);
        void DeleteProjectCandidatesClusterScore(IEnumerable<Guid> projectCandidateIds);
        void DeleteProjectCandidateCompetenceSimulationScores(IEnumerable<Guid> ids);
        void DeleteProjectCandidateFocusedIndicatorSimulationScores(IEnumerable<Guid> ids);
        void DeleteProjectCandidateStandardIndicatorSimulationScores(IEnumerable<Guid> ids);
        void CreateProjectRenevueDistribution(Guid projectId, int crmProjectId);
        void DeleteProjectRevenueDistribution(Guid projectId, int crmProjectId);
    }
}

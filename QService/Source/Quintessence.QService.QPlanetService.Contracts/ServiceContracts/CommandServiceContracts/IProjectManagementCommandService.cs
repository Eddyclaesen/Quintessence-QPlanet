using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface IProjectManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProject(CreateNewProjectRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void LinkProject2CrmProject(Guid id, int crmProjectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnlinkProject2CrmProject(Guid id, int crmId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateAssessmentDevelopmentProject(
            UpdateAssessmentDevelopmentProjectRequest updateAssessmentDevelopmentProjectRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateConsultancyProject(UpdateConsultancyProjectRequest updateProjectRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void LinkProject2Candidate(Guid projectId, Guid candidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnlinkProject2Candidate(Guid projectId, Guid candidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void LinkProjectCategoryDetail2DictionaryIndicators(
            LinkProjectCategoryDetail2DictionaryIndicatorsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectRole(UpdateProjectRoleRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void MarkProjectCategoryDetailDictionaryIndicator(Guid id, bool isStandard = false, bool isDistinctive = false);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnlinkProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetail2DictionaryIndicatorId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void LinkProjectCategoryDetail2SimulationCombinations(Guid projectCategoryDetailId,
                                                              List<Guid> simulationCombinationIds);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnlinkProjectCategoryDetail2Combination(Guid projectCategoryDetail2SimulationCombinationId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void LinkProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId, Guid dictionaryCompetenceId,
                                                              Guid simulationCombinationId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void SetRoiOrder(string projectId, string detailId, string[] order);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void LockRoiOrder(string projectId, string lockRoi);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void SaveRoiScores(Guid id, int? score);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnlinkProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId,
                                                                Guid dictionaryCompetenceId,
                                                                Guid simulationCombinationId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateProjectRole(CreateProjectRoleRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnlinkProjectRoleDictionaryLevel(Guid projectRoleId, Guid dictionaryLevelId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void LinkProjectRole2DictionaryLevels(Guid projectRoleId, List<Guid> selectedLevelIds);

        [OperationContract]
        [FaultContract(typeof (ValidationContainer))]
        void UpdateProjectCategoryDetailSimulationInformation(UpdateProjectCategoryDetailSimulationInformationRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCategoryDetailMatrixRemarks(Guid projectCategoryDetailId, string matrixRemarks, int scoringTypeCode);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void AssignProjectRole(Guid projectCategoryDetailId, Guid projectRoleId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnlinkProjectCategoryDetail2DictionaryLevel(UnlinkProjectCategoryDetail2DictionaryLevelRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CopyProjectRole(CopyProjectRoleRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectPlanPhase(CreateNewProjectPlanPhaseRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectPlanPhaseEntry(CreateNewProjectPlanPhaseEntryRequest createNewProjectPlanEntryRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectPlanPhaseEntry(UpdateProjectPlanPhaseEntryRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectPlanPhaseEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectPlanPhaseProduct(Guid id, Guid entryId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectPlanPhaseEntryDeadline(UpdateProjectPlanPhaseEntryDeadlineRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectPriceIndex(CreateNewProjectPriceIndexRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectPriceIndex(UpdateProjectPriceIndexRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectPriceIndex(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void SaveTimesheetEntries(SaveTimesheetEntriesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteTimesheetEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectFixedPrice(CreateNewProjectFixedPriceRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectFixedPrice(UpdateProjectFixedPriceRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectFixedPrice(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProject(Guid id);

        //[OperationContract]
        //[FaultContract(typeof(ValidationContainer))]
        //void SaveProductsheetEntries(SaveProductsheetEntriesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProductsheetEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnassignProjectRole(Guid projectCategoryDetailId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid AddProjectCandidate(AddProjectCandidateRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectCandidate(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCategoryDetailType1(UpdateProjectCategoryDetailTypeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCategoryDetailType2(UpdateProjectCategoryDetailTypeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCategoryDetailType3(UpdateProjectCategoryDetailTypeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateCategoryDetailType(Guid projectCandidateId, Guid projectCategoryDetailTypeId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateSubcategory(CreateSubcategoryRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSubcategory(UpdateSubcategoryRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateCategoryDetailTypes(List<UpdateProjectCandidateCategoryDetailTypeRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectReporting(UpdateProjectReportingRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateIndicatorSimulationScore(
            CreateNewProjectCandidateIndicatorSimulationScoreRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateCompetenceSimulationScore(
            CreateNewProjectCandidateCompetenceSimulationScoreRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateCompetenceSimulationScore(
            UpdateProjectCandidateCompetenceSimulationScoreRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void MarkDictionaryIndicatorAsStandard(Guid projectRoleId, Guid dictionaryIndicatorId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void MarkDictionaryIndicatorAsDistinctive(Guid projectRoleId, Guid dictionaryIndicatorId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectRoleDictionaryIndicator(Guid projectRoleId, Guid dictionaryIndicatorId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateClusterScore(CreateNewProjectCandidateClusterScoreRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateCompetenceScore(CreateNewProjectCandidateCompetenceScoreRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateIndicatorScore(CreateNewProjectCandidateIndicatorScoreRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateCompetenceScores(
            List<UpdateProjectCandidateCompetenceScoreRequest> projectCandidateCompetenceScores);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateClusterScores(
            List<UpdateProjectCandidateClusterScoreRequest> projectCandidateClusterScores);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateResume(CreateNewProjectCandidateResumeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateResume(UpdateProjectCandidateResumeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectCandidateResumeField(CreateNewProjectCandidateResumeFieldRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProposal(CreateNewProposalRequest request);

        [OperationContract]
        [FaultContract(typeof (ValidationContainer))]
        Guid UpdateProjectCandidateProposal(UpdateProjectCandidateProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateProjectCandidateCategoryType1Proposal(UpdateProjectCandidateDetailType1ProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateProjectCandidateCategoryType2Proposal(UpdateProjectCandidateDetailType2ProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateProjectCandidateCategoryType3Proposal(UpdateProjectCandidateDetailType3ProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateProjectProductProposal(UpdateProjectProductProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateTimeSheetEntryProposal(UpdateTimeSheetEntryProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateProductSheetEntryProposal(UpdateProductSheetEntryProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateAcdcProjectFixedPriceProposal(UpdateAcdcProjectFixedPriceProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateConsultancyProjectFixedPriceProposal(UpdateConsultancyProjectFixedPriceProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProposal(UpdateProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateFrameworkAgreement(CreateFrameworkAgreementRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid UpdateFrameworkAgreement(UpdateFrameworkAgreementRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteFrameworkAgreement(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidatesDetails(List<UpdateProjectCandidateDetailRequest> candidateRequests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidatesDetail(UpdateProjectCandidateDetailRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateProjectCandidateReportRecipient(CreateProjectCandidateReportRecipientRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectCandidateReportRecipient(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateReportRecipient(UpdateProjectCandidateReportRecipientRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateProjectCandidateReportRecipients(List<CreateProjectCandidateReportRecipientRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CancelProjectCandidate(CancelProjectCandidateRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateCancelledProjectCandidates(List<UpdateCancelledProjectCandidateRequest> cancelledRequests);

        [OperationContract]
        [FaultContract(typeof (ValidationContainer))]
        void UpdateCancelledProjectCandidate(UpdateCancelledProjectCandidateRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCategoryDetailUnitPrices(List<UpdateUnitPriceRequest> unitPriceRequests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateProjectTypeCategoryUnitPrice(CreateProjectTypeCategoryUnitPriceRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectTypeCategoryUnitPrice(UpdateProjectTypeCategoryUnitPriceRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectTypeCategoryUnitPrice(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void MarkDocumentAsImportant(Guid projectId, Guid uniqueId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UnmarkDocumentAsImportant(Guid projectId, Guid uniqueId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectDna(CreateNewProjectDnaRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectDna(UpdateProjectDnaRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectTypeCategoryUnitPrices(List<UpdateProjectTypeCategoryUnitPriceRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateInvoicing(List<UpdateBaseInvoicingRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateProjectProducts(List<CreateProjectProductRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateProjectProduct(CreateProjectProductRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectProduct(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProposal(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateProjectEvaluation(CreateProjectEvaluationRequest request);

        [OperationContract]
        [FaultContract(typeof (ValidationContainer))]
        void UpdateEvaluationForm(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectEvaluation(UpdateProjectEvaluationRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateEvaluationForm(CreateEvaluationFormRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateProjectComplaint(CreateProjectComplaintRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectComplaint(UpdateProjectComplaintRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectPlanPhase(UpdateProjectPlanPhaseRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectPlanPhase(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateScoringCoAssessorId(UpdateProjectCandidateScoringCoAssessorIdRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateCustomerFeedback(UpdateCustomerFeedbackRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectUnitPrices(UpdateProjectUnitPricesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateProjectReportRecipients(List<CreateProjectReportRecipientRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectReportRecipient(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateOverviewEntryProjectCandidateField(UpdateProjectCandidateOverviewEntryProjectCandidateFieldRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateOverviewEntryProjectCandidateCategoryField(UpdateProjectCandidateOverviewEntryProjectCandidateCategoryFieldRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingPart1(UpdateEvaluationFormCoachingPart1Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingPart2(UpdateEvaluationFormCoachingPart2Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingPart3(UpdateEvaluationFormCoachingPart3Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingPart4(UpdateEvaluationFormCoachingPart4Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingPart5(UpdateEvaluationFormCoachingPart5Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingPart6(UpdateEvaluationFormCoachingPart6Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingPart7(UpdateEvaluationFormCoachingPart7Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCoachingCompleted(UpdateEvaluationFormCoachingCompletedRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateCulturalFitContactRequest(CreateCulturalFitContactRequestRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateCulturalFitContactRequest(UpdateCulturalFitContactRequestRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectProduct(UpdateProjectProductRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void ReopenCulturalFitContactRequest(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormAcdcPart1(UpdateEvaluationFormAcdcPart1Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormAcdcPart2(UpdateEvaluationFormAcdcPart2Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormAcdcPart3(UpdateEvaluationFormAcdcPart3Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormAcdcPart4(UpdateEvaluationFormAcdcPart4Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormAcdcPart5(UpdateEvaluationFormAcdcPart5Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormAcdcPart6(UpdateEvaluationFormAcdcPart6Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormAcdcCompleted(UpdateEvaluationFormAcdcCompletedRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCustomProjectsPart1(UpdateEvaluationFormCustomProjectsPart1Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCustomProjectsPart2(UpdateEvaluationFormCustomProjectsPart2Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCustomProjectsPart3(UpdateEvaluationFormCustomProjectsPart3Request request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateEvaluationFormCustomProjectsCompleted(UpdateEvaluationFormCustomProjectsCompletedRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateReportingOverviewEntryProjectCandidateField(UpdateProjectCandidateReportingOverviewEntryProjectCandidateFieldRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateReportingOverviewEntryProjectField(UpdateProjectCandidateReportingOverviewEntryProjectFieldRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectCandidateRemarks(UpdateProjectCandidateRemarksRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateCulturalFitCandidateRequest(CreateCulturalFitCandidateRequestRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProjectRoleLanguage(CreateNewProjectRoleLanguageRequest request);

        /// <summary>
        /// Updates the invoicing entries of the invoicing overview.
        /// </summary>
        /// <param name="requests">The requests.</param>
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectManagerInvoicingEntries(List<UpdateInvoicingBaseRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateCustomerAssistantInvoicingEntries(List<UpdateInvoicingBaseRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateAccountantInvoicingEntry(UpdateAccountantInvoicingBaseRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateNewProjectDnaCommercialTranslations(CreateNewProjectDnaCommercialTranslationsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewWonProposal(CreateNewWonProposalRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void EnsureProjectRevenueDistributions(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProjectRevenueDistributions(List<UpdateProjectRevenueDistributionRequest> requests);
    }
}

using System;

namespace Quintessence.QService.QueryModel.Prm
{
    public interface IProjectCandidateCategoryDetailTypeView
    {
        Guid Id { get; }
        Guid ProjectCandidateId { get; }
        ProjectCandidateView ProjectCandidate { get; }
        Guid ProjectCategoryDetailTypeId { get; }
        string ProjectCategoryDetailTypeName { get; }
        int SurveyPlanningId { get; }
        int InvoiceStatusCode { get; }
        decimal InvoiceAmount { get; }
        string PurchaseOrderNumber { get; set; }
        string InvoiceNumber { get; set; }

    }
}
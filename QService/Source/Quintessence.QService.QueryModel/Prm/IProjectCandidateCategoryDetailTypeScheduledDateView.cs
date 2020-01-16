using System;

namespace Quintessence.QService.QueryModel.Prm
{
    public interface IProjectCandidateCategoryDetailTypeScheduledDateView : IProjectCandidateCategoryDetailTypeView
    {
        DateTime? ScheduledDate { get; }
    }
}
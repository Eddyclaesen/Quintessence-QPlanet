using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Rep
{
    public class ReportParameterValue : EntityBase
    {
        public Guid ReportParameterId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
    }
}
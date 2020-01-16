using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class Activity : EntityBase
    {
        public string Name { get; set; }
	    public Guid ActivityTypeId { get; set; }
	    public Guid ProjectId { get; set; }
    }
}

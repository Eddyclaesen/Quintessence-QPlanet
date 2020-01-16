using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CrmProjectLinkModel
    {
        public int Id { get; set; }

        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public string ProjectStatusName { get; set; }
    }
}

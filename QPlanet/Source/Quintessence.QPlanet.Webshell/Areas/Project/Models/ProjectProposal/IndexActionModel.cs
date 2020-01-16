using System.Collections.Generic;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectProposal
{
    public class IndexActionModel
    {
        public List<ProposalView> Proposals { get; set; }

        public List<int> ProposalYears { get; set; }
    }
}
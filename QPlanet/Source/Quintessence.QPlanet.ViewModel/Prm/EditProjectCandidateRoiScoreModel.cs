using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateRoiScoreModel
    {
        public Guid ProjectCandidateRoiScoreId { get; set; }
        public Guid ProjectCandidateId { get; set; }
        public Guid RoiId { get; set; }
        public string RoiQuestion { get; set; }
        public int? Score { get; set; }
    }
}
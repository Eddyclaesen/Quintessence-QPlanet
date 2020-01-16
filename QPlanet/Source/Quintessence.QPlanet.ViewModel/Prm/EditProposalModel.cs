using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProposalModel : BaseEntityModel
    {
        [Required]
        [Display(Name = "Proposal Name")]
        public string Name { get; set; }

        public int? ContactId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string ContactFullName { get; set; }

        [AllowHtml]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Received on")]
        public DateTime? DateReceived { get; set; }

        [Display(Name = "Deadline")]
        public DateTime? Deadline { get; set; }

        [Display(Name = "Sent on")]
        public DateTime? DateSent { get; set; }

        [Display(Name = "Won on")]
        public DateTime? DateWon { get; set; }

        [Display(Name = "Status")]
        public int StatusCode { get; set; }

        [Display(Name = "Written proposal")]
        public bool WrittenProposal { get; set; }

        [AllowHtml]
        [Display(Name = "Status reason")]
        public string StatusReason { get; set; }

        public Guid? BusinessDeveloperId { get; set; }

        [Display(Name = "Business Developer")]
        public string BusinessDeveloperFullName { get; set; }

        public Guid? ExecutorId { get; set; }

        [Display(Name = "Estimation")]
        public decimal? PriceEstimation { get; set; }

        [Display(Name = "Prognosis (%)")]
        public decimal? Prognosis { get; set; }

        [Display(Name = "Final Budget")]
        public decimal? FinalBudget { get; set; }

        [Display(Name = "Executor")]
        public string ExecutorFullName { get; set; }

        public int TypeOfSubmit { get; set; }

        public IEnumerable<SelectListItem> CreateStatusCodeList(int currentStatusCode)
        {
            switch ((ProposalStatusType)currentStatusCode)
            {
                case ProposalStatusType.ToEvaluate:
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate, true);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown);
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;
            
                case ProposalStatusType.TurnedDown:
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose); //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown, true);
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;
            
                case ProposalStatusType.ToPropose:
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose, true);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed);
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;
            
                case ProposalStatusType.NotProposed:
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate); //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed); //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed, true);
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;
            
                case ProposalStatusType.Proposed:
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate); //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed, true);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed); //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown); //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;
            
                case ProposalStatusType.Won:
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won, true);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;

                case ProposalStatusType.Lost:
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost, true);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;

                case ProposalStatusType.Informative:
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative, true);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped);
                    break;

                case ProposalStatusType.Stopped:
                    //ADDED
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToEvaluate); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.ToPropose);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.NotProposed); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.TurnedDown); 
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Won);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Lost);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Informative);
                    yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Stopped, true);
                    break;

                    //yield return CreateProposalStatusTypeSelectItem(ProposalStatusType.Proposed);
                    //yield return CreateProposalStatusTypeSelectItem((ProposalStatusType)currentStatusCode, true);
                    //break;
            
                default:
                    yield return CreateProposalStatusTypeSelectItem((ProposalStatusType)currentStatusCode, true);
                    break;
            }
        }

        public IEnumerable<SelectListItem> CreatePrognosisList(decimal currentPrognosis)
        {
            var list = new List<SelectListItem>();
            for (decimal d = 0; d <= 1; d += 0.05m)
                list.Add(new SelectListItem { Selected = d == currentPrognosis, Text = string.Format("{0}%", d * 100), Value = d.ToString() });
            return list;
        }

        private SelectListItem CreateProposalStatusTypeSelectItem(ProposalStatusType proposalStatusType, bool isSelected = false)
        {
            return new SelectListItem { Selected = isSelected, Text = EnumMemberNameAttribute.GetName(proposalStatusType), Value = ((int)proposalStatusType).ToString(CultureInfo.InvariantCulture) };
        }
    }
}

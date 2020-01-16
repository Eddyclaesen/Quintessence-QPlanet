using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectPlanPhaseModel : BaseEntityModel
    {
        private List<EditProjectPlanPhaseEntryModel> _projectPlanPhaseEntries;

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        public Guid ProjectPlanId { get; set; }

        public List<EditProjectPlanPhaseEntryModel> ConsolidatedProjectPlanPhaseEntries
        {
            get
            {
                var consolidatedList = new List<EditProjectPlanPhaseEntryModel>();

                foreach (var entry in ProjectPlanPhaseEntries.OfType<EditProjectPlanPhaseActivityModel>())
                {
                    var current = consolidatedList
                        .OfType<EditProjectPlanPhaseActivityModel>()
                        .FirstOrDefault(e => e.ActivityId == entry.ActivityId
                                             && e.ProfileId == entry.ProfileId
                                             && e.Duration == entry.Duration);

                    if (current == null)
                    {
                        consolidatedList.Add(current = new EditProjectPlanPhaseActivityModel());
                        Mapper.DynamicMap(entry, current);
                    }
                    else
                    {
                        current.Quantity += entry.Quantity;
                    }
                }

                foreach (var entry in ProjectPlanPhaseEntries.OfType<EditProjectPlanPhaseProductModel>())
                {
                    EditProjectPlanPhaseProductModel current = new EditProjectPlanPhaseProductModel();
                    Mapper.DynamicMap(entry, current);
                    consolidatedList.Add(current);                 
                }

                return consolidatedList.ToList(); //.Where(e => e.Quantity != 0).ToList();
            }
        }

        public List<EditProjectPlanPhaseEntryModel> ProjectPlanPhaseEntries
        {
            get { return _projectPlanPhaseEntries ?? (_projectPlanPhaseEntries = new List<EditProjectPlanPhaseEntryModel>()); }
            set { _projectPlanPhaseEntries = value; }
        }

        public decimal TotalDays
        {
            get { return ProjectPlanPhaseEntries.OfType<EditProjectPlanPhaseActivityModel>().Sum(e => e.Days); }
        }

        public decimal TotalPrice
        {
            get { return ProjectPlanPhaseEntries.Sum(e => e.Price); }
        }
    }
}
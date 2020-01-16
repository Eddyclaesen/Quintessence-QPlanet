using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityDetailTrainingModel : EditActivityDetailModel
    {
        private List<EditActivityDetailTrainingTypeModel> _activityDetailTrainingTypes;
        private List<EditActivityDetailTrainingLanguageModel> _activityDetailTrainingLanguages;

        [Display(Name = "Training demand")]
        public override string Description { get; set; }

        [Display(Name = "Target group")]
        public string TargetGroup { get; set; }

        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Display(Name = "Extra information")]
        public string ExtraInfo { get; set; }

        [Display(Name = "Checklist (SharePoint)")]
        public string ChecklistLink { get; set; }

        [Display(Name = "CRM code")]
        public string Code { get; set; }

        public List<EditActivityDetailTrainingLanguageModel> ActivityDetailTrainingLanguages
        {
            get { return _activityDetailTrainingLanguages ?? (_activityDetailTrainingLanguages = new List<EditActivityDetailTrainingLanguageModel>()); }
            set { _activityDetailTrainingLanguages = value; }
        }

        public List<EditActivityDetailTrainingTypeModel> TrainingTypes
        {
            get { return _activityDetailTrainingTypes ?? (_activityDetailTrainingTypes = new List<EditActivityDetailTrainingTypeModel>()); }
        }

        public List<ActivityDetailTrainingAppointmentView> TrainingAppointments { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Quintessence.CulturalFit.UI.Webshell.Models.Questionnaire
{
    public class QuestionnaireModel
    {
        private List<TheoremEntity> _theorems;
        private List<ErrorMessage> _errorMessages;
        private List<LanguageEntity> _languages;

        public List<TheoremEntity> Theorems
        {
            get { return _theorems ?? (_theorems = new List<TheoremEntity>()); }
            set { _theorems = value; }
        }

        public string ListCode { get; set; }

        public bool IsCompleted { get; set; }

        public int TheoremListCount { get; set; }

        public List<ErrorMessage> ErrorMessages
        {
            get { return _errorMessages ?? (_errorMessages = new List<ErrorMessage>()); }
            set { _errorMessages = value; }
        }

        public List<LanguageEntity> Languages
        {
            get { return _languages ?? (_languages = new List<LanguageEntity>()); }
            set { _languages = value; }
        }

        public LanguageEntity SelectedLanguage { get; set; }

        public string TheoremListType { get; set; }

        public Guid TheoremListRequestId { get; set; }
    }
}
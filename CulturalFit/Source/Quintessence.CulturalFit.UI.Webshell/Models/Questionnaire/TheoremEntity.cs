using System;

namespace Quintessence.CulturalFit.UI.Webshell.Models.Questionnaire
{
    public class TheoremEntity
    {
        public Guid Id { get; set; }

        public string Quote { get; set; }

        public bool IsMost { get; set; }

        public bool IsLeast { get; set; }
    }
}
using System;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class SelectProductTypeModel
    {
        public bool IsSelected { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        
    }
}
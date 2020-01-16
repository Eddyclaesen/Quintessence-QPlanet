using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class SelectCrmEmailModel
    {
        public bool IsSelected { get; set; }
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DirectPhone { get; set; }
        public string MobilePhone { get; set; }
    }
}
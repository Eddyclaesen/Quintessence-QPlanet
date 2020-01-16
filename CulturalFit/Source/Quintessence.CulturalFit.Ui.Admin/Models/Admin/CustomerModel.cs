using System.Collections.Generic;
using Quintessence.CulturalFit.DataModel.Cfi;

namespace Quintessence.CulturalFit.UI.Admin.Models.Admin
{
    public class CustomerModel
    {
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>
        /// The project id.
        /// </value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the theorem list requests.
        /// </summary>
        /// <value>
        /// The theorem list requests.
        /// </value>
        public List<TheoremListRequest> TheoremListRequests { get; set; }

        /// <summary>
        /// Gets or sets the error messages.
        /// </summary>
        /// <value>
        /// The error messages.
        /// </value>
        public List<string> ErrorMessages { get; set; }

        /// <summary>
        /// Gets or sets the selected language id.
        /// </summary>
        /// <value>
        /// The selected language id.
        /// </value>
        public int SelectedLanguageId { get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public List<Language> Languages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Quintessence.CulturalFit.DataModel.Cfi;

namespace Quintessence.CulturalFit.UI.Admin.Models.Admin.Entities
{
    public class CandidateEntity
    {
        private bool? _hasRequest;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>
        /// The project id.
        /// </value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has request.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has request; otherwise, <c>false</c>.
        /// </value>
        public bool HasRequest
        {
            get { return _hasRequest.GetValueOrDefault(true); }
            set { _hasRequest = value; }
        }

        /// <summary>
        /// Gets or sets the deadline.
        /// </summary>
        /// <value>
        /// The deadline.
        /// </value>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Gets or sets the theorem list request id.
        /// </summary>
        /// <value>
        /// The theorem list request id.
        /// </value>
        public Guid TheoremListRequestId { get; set; }

        /// <summary>
        /// Gets or sets the theorem list verification code.
        /// </summary>
        /// <value>
        /// The theorem list verification code.
        /// </value>
        public string TheoremListVerificationCode { get; set; }

        /// <summary>
        /// Gets or sets the theorem list request date.
        /// </summary>
        /// <value>
        /// The theorem list request date.
        /// </value>
        public DateTime TheoremListRequestDate { get; set; }

        /// <summary>
        /// Gets or sets the existing theorem list requests.
        /// </summary>
        /// <value>
        /// The existing theorem list requests.
        /// </value>
        public List<TheoremListRequest> ExistingTheoremListRequest { get; set; }
    }
}
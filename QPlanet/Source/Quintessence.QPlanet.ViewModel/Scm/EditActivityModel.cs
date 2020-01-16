using System;
using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityModel : BaseEntityModel
    {
        public Guid ActivityTypeId { get; set; }

        public string ActivityTypeName { get; set; }

        public List<EditActivityProfileModel> ActivityProfiles { get; set; }

        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Dim;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminProject
{
    public class DictionaryLevelsModel
    {
        public Guid ProjectRoleId { get; set; }
        //public IEnumerable<DictionaryView> Dictionaries { get; set; }

        public List<DictionaryModel> Dictionaries { get; set; }
    }
}

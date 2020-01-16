using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectTypeCategoryDefaultValueModel : BaseEntityModel
    {
        public string Code { get; set; }

        [AllowHtml]
        public string Value { get; set; }
    }
}
using System.Collections.Generic;

namespace Quintessence.QPlanet.Infrastructure.Web
{
    public class DropDownListGroupItem
    {
        public string Name { get; set; }
        
        public List<DropDownListItem> Items { get; set; }
    }
}

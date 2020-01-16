using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Quintessence.CulturalFit.DataModel.Reports
{
    [DataContract]
    public enum OutputFormat
    {
        [EnumMember]
        Pdf,
        [EnumMember]
        MHtml,
        [EnumMember]
        Csv,
        [EnumMember]
        Excel,
        [EnumMember]
        Word,
        [EnumMember]
        Xml,
        [EnumMember]
        Image,
        [EnumMember]
        Html4,
        [EnumMember]
        Html32
    }
}

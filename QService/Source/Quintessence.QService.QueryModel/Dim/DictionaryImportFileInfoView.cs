using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryImportFileInfoView
    {
        [DataMember]
        public DateTime? CreationTime { get; set; }

        [DataMember]
        public string Extension { get; set; }

        [DataMember]
        public long? Length { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
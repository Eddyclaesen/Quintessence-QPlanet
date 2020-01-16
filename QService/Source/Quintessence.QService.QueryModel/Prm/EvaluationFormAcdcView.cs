using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class EvaluationFormAcdcView : EvaluationFormView
    {
        [DataMember]
        public int? Question01 { get; set; }

        [DataMember]
        public int? Question02 { get; set; }

        [DataMember]
        public int? Question03 { get; set; }

        [DataMember]
        public int? Question04 { get; set; }

        [DataMember]
        public int? Question05 { get; set; }

        [DataMember]
        public int? Question06 { get; set; }

        [DataMember]
        public int? Question07 { get; set; }

        [DataMember]
        public string Question08 { get; set; }

        [DataMember]
        public string Question09 { get; set; }

        [DataMember]
        public string Question10 { get; set; }

        [DataMember]
        public bool? Question11 { get; set; }

        [DataMember]
        public bool? Question12 { get; set; }

        [DataMember]
        public bool? Question13 { get; set; }

        [DataMember]
        public bool? Question14 { get; set; }

        [DataMember]
        public bool? Question15 { get; set; }

        [DataMember]
        public bool? Question16 { get; set; }

        [DataMember]
        public bool? Question17 { get; set; }

        [DataMember]
        public string Question18 { get; set; }

        [DataMember]
        public bool? Question19 { get; set; }

        [DataMember]
        public string Question19_19A { get; set; }

        [DataMember]
        public string Question19_19B { get; set; }

        [DataMember]
        public string Question20 { get; set; }

        [DataMember]
        public string Question21 { get; set; }

        [DataMember]
        public string Question22 { get; set; }

        [DataMember]
        public string Question23 { get; set; }

        [DataMember]
        public string Question24 { get; set; }

        [DataMember]
        public string Question25 { get; set; }

        [DataMember]
        public string Question26 { get; set; }		
    }
}
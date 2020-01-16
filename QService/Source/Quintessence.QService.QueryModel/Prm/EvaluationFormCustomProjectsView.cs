using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class EvaluationFormCustomProjectsView : EvaluationFormView
    {
        [DataMember]
        public string Question01 { get; set; }

        [DataMember]
        public string Question02 { get; set; }

        [DataMember]
        public string Question03 { get; set; }

        [DataMember]
        public string Question04 { get; set; }

        [DataMember]
        public string Question05 { get; set; }

        [DataMember]
        public string Question06 { get; set; }

        [DataMember]
        public string Question07 { get; set; }

        [DataMember]
        public string Question08 { get; set; }

        [DataMember]
        public string Question09 { get; set; }

        [DataMember]
        public string Question10 { get; set; }

        [DataMember]
        public string Question11 { get; set; }

        [DataMember]
        public string Question12 { get; set; }

        [DataMember]
        public string Question13 { get; set; }

        [DataMember]
        public string Question14 { get; set; }

        [DataMember]
        public string Question15 { get; set; }
    }
}
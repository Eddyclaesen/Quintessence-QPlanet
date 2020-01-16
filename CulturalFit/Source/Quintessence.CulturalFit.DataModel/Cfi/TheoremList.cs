using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremList : IEntity
    {
        private List<Theorem> _theorems;

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int TheoremListTypeId { get; set; }

        [DataMember]
        public Guid TheoremListRequestId { get; set; }

        [DataMember]
        public TheoremListType TheoremListType { get; set; }

        [DataMember]
        public string VerificationCode { get; set; }

        [DataMember]
        public bool IsCompleted { get; set; }

        [DataMember]
        public TheoremListRequest TheoremListRequest { get; set; }

        [DataMember]
        public List<Theorem> Theorems
        {
            get { return _theorems ?? (_theorems = new List<Theorem>()); }
            set { _theorems = value; }
        }

        [DataMember]
        public Audit Audit { get; set; }

        public Theorem AddTheorem(Theorem theorem = null)
        {
            if (theorem == null)
                theorem = new Theorem { Id = Guid.NewGuid() };

            Theorems.Add(theorem);
            theorem.TheoremListId = Id;
            theorem.TheoremList = this;
            return theorem;
        }
    }
}

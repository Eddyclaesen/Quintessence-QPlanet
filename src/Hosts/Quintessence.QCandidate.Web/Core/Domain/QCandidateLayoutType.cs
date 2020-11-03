using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class QCandidateLayoutType : Enumeration
    {
        public static readonly QCandidateLayoutType Hide = new QCandidateLayoutType(1, "Hide");
        public static readonly QCandidateLayoutType Show = new QCandidateLayoutType(2, "Show");
        public static readonly QCandidateLayoutType Pdf = new QCandidateLayoutType(3, "PDF");
        public static readonly QCandidateLayoutType Memo = new QCandidateLayoutType(4, "Memo");


        public QCandidateLayoutType(int id, string name)
            : base(id, name)
        {
            
        }


    }
}

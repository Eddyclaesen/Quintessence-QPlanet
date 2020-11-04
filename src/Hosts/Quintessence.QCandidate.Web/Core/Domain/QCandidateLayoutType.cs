﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class QCandidateLayoutType : Enumeration
    {
        public static readonly QCandidateLayoutType Hide = new QCandidateLayoutType(1, nameof(Hide));
        public static readonly QCandidateLayoutType Show = new QCandidateLayoutType(2, nameof(Show));
        public static readonly QCandidateLayoutType Pdf = new QCandidateLayoutType(3, nameof(Pdf));
        public static readonly QCandidateLayoutType Memo = new QCandidateLayoutType(4, nameof(Memo));


        public QCandidateLayoutType(int id, string name)
            : base(id, name)
        {
        }
        
    }
}

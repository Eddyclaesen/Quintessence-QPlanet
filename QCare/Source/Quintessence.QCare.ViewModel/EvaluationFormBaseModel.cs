using System;
using Quintessence.QCare.ViewModel.Base;

namespace Quintessence.QCare.ViewModel
{
    public class EvaluationFormBaseModel : BaseEntityModel
    {
        public string VerificationCode { get; set; }
        public int Navigation { get; set; }
    }
}
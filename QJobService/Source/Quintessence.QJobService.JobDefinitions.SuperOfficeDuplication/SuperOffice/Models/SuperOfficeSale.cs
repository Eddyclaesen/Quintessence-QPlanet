using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficeSale
    {
        [JsonProperty(PropertyName = "saleId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "contactId")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "projectId")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "personId")]
        public int? PersonId { get; set; }

        [JsonProperty(PropertyName = "associateId")]
        public string AssociateId { get; set; }

        [JsonProperty(PropertyName = "associate/fullName")]
        public string AssociateFullName { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "stage")]
        public string Stage { get; set; }

        [JsonProperty(PropertyName = "saleStatus")]
        public string SaleStatus { get; set; }
        
        [JsonProperty(PropertyName = "date")]
        public DateTime? SaleDate { get; set; }

        [JsonProperty(PropertyName = "Source")]
        public string Source { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal? Amount { get; set; }

        [JsonProperty(PropertyName = "quote/version/alternative/quoteline/totalCost")]
        public decimal? TotalCost { get; set; }

        [JsonProperty(PropertyName = "earning")]
        public decimal? Earning { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string SaleText { get; set; }

        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "updatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }

        #region Enriched Properties

        public string SourceParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(Source); }
        }


        #endregion
    }
}

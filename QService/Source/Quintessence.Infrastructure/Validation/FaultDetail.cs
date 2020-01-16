using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.Infrastructure.Validation
{
    [DataContract]
    [KnownType(typeof(VersionMismatchFaultEntry))]
    [KnownType(typeof(AuthenticationFaultEntry))]
    [KnownType(typeof(EntityValidationFaultEntry))]
    [KnownType(typeof(ValidationFaultEntry))]
    public class FaultDetail
    {
        private List<FaultEntry> _faultEntries;

        [DataMember]
        public List<FaultEntry> FaultEntries
        {
            get { return _faultEntries ?? (_faultEntries = new List<FaultEntry>()); }
            set { _faultEntries = value; }
        }

        public bool IsValid
        {
            get { return FaultEntries.Count == 0; }
        }

        public string Reason
        {
            get
            {
                var entry = FaultEntries.FirstOrDefault();
                return entry == null ? string.Empty : entry.Message;
            }
        }

        public void RegisterVersionMismatchEntry(object updateObject, IEntity storeObject)
        {
            var entry = new VersionMismatchFaultEntry
                {
                    Message = "The object you are trying to update has been updated by {0} on {1} since you last opened it.",
                    ModifiedBy = storeObject.Audit.ModifiedBy,
                    ModifiedOn = storeObject.Audit.ModifiedOn
                };

            var updateObjectSerializer = new DataContractSerializer(updateObject.GetType());
            using (var updateObjectMemoryStream = new MemoryStream())
            {
                updateObjectSerializer.WriteObject(updateObjectMemoryStream, updateObject);
                updateObjectMemoryStream.Position = 0;
                using (var reader = new StreamReader(updateObjectMemoryStream))
                {
                    entry.UpdateObject = reader.ReadToEnd();
                }
            }

            var storeObjectSerializer = new DataContractSerializer(storeObject.GetType());
            using (var storeObjectMemoryStream = new MemoryStream())
            {
                storeObjectSerializer.WriteObject(storeObjectMemoryStream, storeObject);
                storeObjectMemoryStream.Position = 0;
                using (var reader = new StreamReader(storeObjectMemoryStream))
                {
                    entry.StoreObject = reader.ReadToEnd();
                }
            }

            FaultEntries.Add(entry);
        }

        public void RegisterException(Exception exception)
        {
            while (exception != null)
            {
                FaultEntries.Add(new FaultEntry { Message = exception.Message });
                exception = exception.InnerException;
            }
        }

        public void RegisterValidationFaultEntry(string code, string message)
        {
            var entry = new ValidationFaultEntry
            {
                Code = code,
                Message = message
            };
            FaultEntries.Add(entry);
        }

        public void RegisterAuthenticationFaultEntry(string message)
        {
            var entry = new AuthenticationFaultEntry { Message = message };
            FaultEntries.Add(entry);
        }

        public void RegisterEntityValidationFaultEntry(string errorMessage, string[] memberNames)
        {
            var entry = new EntityValidationFaultEntry
                {
                    MemberNames = memberNames.ToList(),
                    Message = errorMessage
                };
            FaultEntries.Add(entry);
        }
    }
}

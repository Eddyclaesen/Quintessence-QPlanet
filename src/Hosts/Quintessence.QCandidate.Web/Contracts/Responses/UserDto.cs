using System;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
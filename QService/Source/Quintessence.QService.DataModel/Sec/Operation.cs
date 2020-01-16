using System;

namespace Quintessence.QService.DataModel.Sec
{
    public class Operation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Domain { get; set; }
    }
}

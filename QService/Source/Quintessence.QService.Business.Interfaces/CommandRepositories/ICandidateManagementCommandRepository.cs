using System;
using Quintessence.QService.DataModel.Cam;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface ICandidateManagementCommandRepository : ICommandRepository
    {
        void DeleteProjectCandidateProgramComponents(Guid projectCandidateId);
        void DeleteRoomProgramComponents(Guid roomId);
    }
}

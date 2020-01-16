
namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication
{
    public class Constants
    {
        public class JobNames
        {
            public class TeamLeader
            {
                public const string TeamLeaderEventReplication = "TeamLeaderCrmReplicationTeamLeaderEvent";               
                public const string ProjectIdReplication = "TeamLeaderCrmReplicationProjectId";                
                public const string AssociateReplication = "TeamLeaderCrmReplicationAssociate";
                public const string AssociateEmailReplication = "TeamLeaderCrmReplicationAssociateEmail";
                public const string ContactIdReplication = "TeamLeaderCrmReplicationContactId";
                public const string PersonIdReplication = "TeamLeaderCrmReplicationPersonId";
            }
        }

        public class UserGroupNames
        {
            public const string FromTeamLeader = "From TeamLeader";
        }
    }
}


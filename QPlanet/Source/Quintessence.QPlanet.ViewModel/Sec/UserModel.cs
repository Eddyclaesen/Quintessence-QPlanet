using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sec
{
    public class UserModel : BaseEntityModel
    {
        private List<OperationModel> _operations;
        private List<UserGroupModel> _userGroups;
        private List<RoleModel> _roles;
        private List<AuthenticationTokenModel> _authenticationTokens;

        public int AssociateId { get; set; }

        public string UserName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public byte[] Password { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public string FullName { get; set; }

        public List<OperationModel> Operations
        {
            get { return _operations ?? (_operations = new List<OperationModel>()); }
        }

        public List<UserGroupModel> UserGroups
        {
            get { return _userGroups ?? (_userGroups = new List<UserGroupModel>()); }
        }

        public List<RoleModel> Roles
        {
            get { return _roles ?? (_roles = new List<RoleModel>()); }
        }

        public List<AuthenticationTokenModel> AuthenticationTokens
        {
            get { return _authenticationTokens ?? (_authenticationTokens = new List<AuthenticationTokenModel>()); }
        }
    }
}

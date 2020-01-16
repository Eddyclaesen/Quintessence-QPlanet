namespace Quintessence.QPlanet.Webshell.Models.Login
{
    public class ChangePasswordActionModel
    {
        public string UserName { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
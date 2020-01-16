using System;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Quintessence.Infrastructure.Validation;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Services;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Models.Login;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Authentication;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QPlanet.Webshell.Controllers
{
    public class LoginController : QPlanetControllerBase
    {
        public ActionResult Login()
        {
            var credential = new Credential();
            credential.ReturnUrl = Request.QueryString["ReturnUrl"];
            return View(credential);
        }

        [HttpPost]
        public ActionResult Login(Credential credential)
        {
                try
                {
                    var queryServiceInvoker = new ServiceInvoker<IAuthenticationQueryService>();
                    var commandServiceInvoker = new ServiceInvoker<IAuthenticationCommandService>();
                    var authenticationResponse = commandServiceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context, service => service.NegociateAuthenticationToken(credential.UserName, credential.Password));
                    var tokenId = authenticationResponse.AuthenticationTokenId;
                    var token = queryServiceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context, service => service.RetrieveAuthenticationToken(tokenId));

                    var ticket = new FormsAuthenticationTicket(1, credential.UserName, token.IssuedOn, token.ValidTo, true, token.Id.ToString(), FormsAuthentication.FormsCookiePath);

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                    {
                        Path = FormsAuthentication.FormsCookiePath,
                        Expires = token.ValidTo
                    };

                    Response.Cookies.Add(cookie);

                    var returnUrl = credential.ReturnUrl;

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var url = Request.Cookies["LastVisitedUrl"];
                        if (url != null)
                        {
                            return Redirect(url.Value);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (FaultException<ValidationContainer> exception)
                {
                    if (exception.Message == "Your password is expired.")
                    {
                        return RedirectToAction("ChangePassword");
                    }
                    throw;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                return HandleStatusCodeErrorHtml(exception);
                }
            }

        public ActionResult Logout()
        {
            if (Response.Cookies["LastVisitedUrl"] != null)
            {
                Response.Cookies["LastVisitedUrl"].Value = Request.UrlReferrer == null ? "~/" : Request.UrlReferrer.ToString();
            }
            else
            {
                var cookie = new HttpCookie("LastVisitedUrl")
                    {
                        Value = Request.UrlReferrer == null ? "~/" : Request.UrlReferrer.ToString(),
                        Expires = DateTime.Now.AddDays(3)
                    };
                Response.Cookies.Add(cookie);

            }
            
            Thread.FreeNamedDataSlot("AuthenticationToken");
            
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new ChangePasswordActionModel();

                    //Try to get the current usertoken.
                    //This is available if the user has initiated the password change.
                    //If this is due to a reset of the password, the authentication token can't be retrieved.
                    try
                    {
                        if (GetAuthenticationToken() != null)
                        {
                            model.UserName = GetAuthenticationToken().User.UserName;
                        }
                    }
                    catch (Exception exception)
                    {
                    }

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, () => View());
                }
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<ChangePasswordRequest>(model);
                    this.InvokeService<IAuthenticationCommandService>(service => service.ChangePassword(request));
                    Credential credential = new Credential();
                    credential.UserName = model.UserName;
                    credential.Password = model.NewPassword;
                    credential.ReturnUrl = Request.QueryString["ReturnUrl"];
                    return Login(credential);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, () => View(model));
                }
            }
        }
    }
}

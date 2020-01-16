using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Models.General;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Dom;

namespace Quintessence.QPlanet.Webshell.Controllers
{
    public class GeneralController : Controller
    {
        public ActionResult BackgroundStyle()
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (Session["BackgroundImageUrl"] == null)
                    {
                        var directoryInfo = new DirectoryInfo(Server.MapPath("~/Images/Background/"));
                        //var files = directoryInfo.GetFiles("Background*.png");
                        //var imageUrl = Url.Content("~/Images/Background/" + files[new Random().Next(files.Length)].Name);

                        var files = directoryInfo.GetFiles("qplanet_bg*.jpg");
                        var imageUrl = Url.Content("~/Images/Background/" + files[new Random().Next(files.Length)].Name);

                        Session["BackgroundImageUrl"] = imageUrl;
                    }

                    ViewBag.BackgroundImageUrl = Session["BackgroundImageUrl"];
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                }
            }

            return PartialView();
        }

        public ActionResult Search(string term)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var response = this.InvokeService<IInfrastructureQueryService, SearchResponse>(service => service.Search(term));

                    const string projectSearchResultView = "ProjectSearchResultView";
                    const string candidateSearchResultView = "CandidateSearchResultView";

                    var json = response.Results.Select(u =>
                        {
                            switch (u.GetType().Name)
                            {
                                case projectSearchResultView:
                                    return new { label = u.Name, value = Url.Action("Edit", "ProjectGeneral", new { id = u.Id, area = "Project" }) };

                                case candidateSearchResultView:
                                    return new { label = u.Name, value = Url.Action("Edit", "Candidate", new { id = u.Id, area = "Candidate" }) };

                                default:
                                    return new { label = u.Name, value = u.Id.ToString() };
                            }
                        }).ToList();

                    var result = Json(json.ToArray(), JsonRequestBehavior.AllowGet);
                    return result;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new EmptyResult();
                }
            }
        }

        public ActionResult Help(string location)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var helpEntry = this.InvokeService<IDocumentManagementQueryService, HelpEntryView>(service => service.RetrieveHelpEntry(location));

                    return PartialView(helpEntry != null 
                        ? Mapper.DynamicMap<HelpActionModel>(helpEntry) 
                        : new HelpActionModel{Title = location, Body = string.Format("<p>No help documentation found for {0}.</p>", location)});
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new EmptyResult();
                }
            }
        }
    }
}

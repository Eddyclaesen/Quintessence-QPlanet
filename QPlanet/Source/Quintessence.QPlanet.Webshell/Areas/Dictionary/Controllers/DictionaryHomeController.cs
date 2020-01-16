using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Models.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QPlanet.Webshell.Areas.Dictionary.Controllers
{
    //[QPlanetAuthenticateController("DIM")]
    public class DictionaryHomeController : DictionaryController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuintessenceDictionaries(DataTableParameterModel parameters)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //var request = new ListDictionariesRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                    //var response = this.InvokeService<IDictionaryManagementQueryService, ListDictionariesResponse>(service => service.ListQuintessenceDictionaries(request));
                    //var dictionaries = response.Dictionaries;

                    //var model = new
                    //{
                    //    sEcho = parameters.sEcho,
                    //    iTotalRecords = response.DataTablePaging.TotalRecords,
                    //    iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                    //    aaData = dictionaries.Select(d => new[] { d.Name, d.Contact != null ? d.Contact.Name : string.Empty, d.Current.ToString(), d.Id.ToString() })
                    //};

                    //return Json(model, JsonRequestBehavior.AllowGet);

                    //TODO: Remove this controller
                    throw new NotImplementedException();
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CustomerDictionaries(DataTableParameterModel parameters)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //var request = new ListDictionariesRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                    //var response = this.InvokeService<IDictionaryManagementQueryService, ListDictionariesResponse>(service => service.ListCustomerDictionaries(request));
                    //var dictionaries = response.Dictionaries;

                    //var model = new
                    //{
                    //    sEcho = parameters.sEcho,
                    //    iTotalRecords = response.DataTablePaging.TotalRecords,
                    //    iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                    //    aaData = dictionaries.Select(d => new[] { d.Name, d.Contact != null ? d.Contact.Name : string.Empty, d.Current.ToString(), d.Id.ToString() })
                    //};

                    //return Json(model, JsonRequestBehavior.AllowGet);

                    //TODO: Remove this controller
                    throw new NotImplementedException();
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Scm;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminActivity;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminActivityController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActivityTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var activityTypes = this.InvokeService<ISupplyChainManagementQueryService, List<ActivityTypeView>>(service => service.ListActivityTypeDetails());
                    return PartialView(activityTypes.OrderByDescending(at => at.IsSystem).ThenBy(at => at.Name).ToList());
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult Profiles()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var profiles = this.InvokeService<ISupplyChainManagementQueryService, List<ProfileView>>(service => service.ListProfiles());
                    return PartialView(profiles.OrderBy(p => p.Name).ToList());
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ProductTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var productTypes = this.InvokeService<ISupplyChainManagementQueryService, List<ProductTypeView>>(service => service.ListProductTypes());
                    return PartialView(productTypes.OrderBy(p => p.Name).ToList());
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ApplyPriceIndex()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new ApplyPriceIndexActionModel { Index = 100 };
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult ApplyPriceIndex(ApplyPriceIndexActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityTypeProfileRatesByIndex(model.Index));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult AddActivityType()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new AddActivityTypeModel();
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddActivityType(AddActivityTypeModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewActivityTypeRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.CreateNewActivityType(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditActivityType(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var activityType = this.InvokeService<ISupplyChainManagementQueryService, ActivityTypeView>(service => service.RetrieveActivityType(id));
                    var model = Mapper.DynamicMap<EditActivityTypeModel>(activityType);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityType(EditActivityTypeModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateActivtyTypeRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityType(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteActivityType(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.DeleteActivityType(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult AddActivityTypeProfile(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var profiles = this.InvokeService<ISupplyChainManagementQueryService, List<ProfileView>>(service => service.ListProfiles());

                    var model = new AddActivityTypeProfileModel();
                    model.ActivityTypeId = id;
                    model.Profiles = profiles.Select(p => new ProfileSelectListItemModel(p));
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddActivityTypeProfile(AddActivityTypeProfileModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewActivityTypeProfileRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.CreateNewActivityTypeProfile(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditActivityTypeProfile(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var activityTypeProfile = this.InvokeService<ISupplyChainManagementQueryService, ActivityTypeProfileView>(service => service.RetrieveActivityTypeProfile(id));
                    var profiles = this.InvokeService<ISupplyChainManagementQueryService, List<ProfileView>>(service => service.ListProfiles());
                    var model = Mapper.DynamicMap<EditActivityTypeProfileModel>(activityTypeProfile);
                    model.Profiles = profiles.Select(p => new ProfileSelectListItemModel(p));
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityTypeProfile(EditActivityTypeProfileModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateActivtyTypeProfileRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityTypeProfile(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteActivityTypeProfile(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.DeleteActivityTypeProfile(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CreateProfile()
        {
            return PartialView("CreateProfile");
        }

        [HttpPost]
        public ActionResult CreateProfile(EditProfileModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewProfileRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.CreateNewProfile(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditProfile(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var profile = this.InvokeService<ISupplyChainManagementQueryService, ProfileView>(service => service.RetrieveProfile(id));
                    var model = Mapper.DynamicMap<EditProfileModel>(profile);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProfileRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateProfile(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CreateProductType()
        {
            return View("CreateProductType");
        }

        [HttpPost]
        public ActionResult CreateProductType(EditProductTypeModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateProductTypeRequest>(model);
                    var id = this.InvokeService<ISupplyChainManagementCommandService, Guid>(service => service.CreateProductType(request));
                    return RedirectToAction("EditProductType", new {id = id});
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditProductType(Guid id)
        {
            using(DurationLog.Create())
            {
                try
                {
                    var model = Mapper.DynamicMap<EditProductTypeModel>(this.InvokeService<ISupplyChainManagementQueryService, ProductTypeView>(service => service.RetrieveProductType(id)));
                    return View("EditProductType", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditProductType(EditProductTypeModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProductTypeRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateProductType(request));
                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception); 
                    return HandleError(exception);
                }
            }
        }
    }
}

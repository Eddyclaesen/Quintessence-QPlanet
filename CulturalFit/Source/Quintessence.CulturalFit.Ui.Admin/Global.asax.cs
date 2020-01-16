using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.UI.Admin.Models.Admin;
using Quintessence.CulturalFit.UI.Admin.Models.Admin.Entities;
using Quintessence.CulturalFit.UI.Core.Service;
using Quintessence.CulturalFit.UI.Core.Unity;

namespace Quintessence.CulturalFit.UI.Admin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer _container;

        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdminGenerateReport",
                url: "CustomersAdmin/GenerateReport/{requestId}/{languageId}",
                defaults: new { controller = "CustomersAdmin", action = "GenerateReport" }
                );

            routes.MapRoute(
                name: "AdminListParticipants",
                url: "ParticipantsAdmin/Index/{projectId}",
                defaults: new { controller = "ParticipantsAdmin", action = "Index" }
                );

            routes.MapRoute(
                name: "AdminListParticipants_Save",
                url: "ParticipantsAdmin/Save/{contactId}",
                defaults: new { controller = "ParticipantsAdmin", action = "Save", contactId = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "AdminEditCustomerRequest",
                url: "CustomersAdmin/EditCustomerRequest/{projectId}/{requestId}",
                defaults: new { controller = "CustomersAdmin", action = "EditCustomerRequest" }
                );

            routes.MapRoute(
                name: "AdminEditCustomerRequestAndSend",
                url: "CustomersAdmin/EditCustomerRequestAndSend/{projectId}/{requestId}",
                defaults: new { controller = "CustomersAdmin", action = "EditCustomerRequestAndSend" }
                );

            routes.MapRoute(
                name: "AdminCreateCustomerRequest",
                url: "CustomersAdmin/CreateCustomerRequest/{projectId}",
                defaults: new { controller = "CustomersAdmin", action = "CreateCustomerRequest", projectId = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "AdminCreateCustomerRequestAndSend",
                url: "CustomersAdmin/CreateCustomerRequestAndSend/{projectId}",
                defaults: new { controller = "CustomersAdmin", action = "CreateCustomerRequestAndSend" }
                );

            routes.MapRoute(
                name: "AdminContactOverview",
                url: "CustomersAdmin/Index/{projectId}",
                defaults: new { controller = "CustomersAdmin", action = "Index", projectId = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "CustomersAdmin", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            InitializeAutoMapper();
            _container = new UnityContainer();
            _container.RegisterInstance<IUnityContainer>(_container);
            _container.RegisterInstance(new ServiceFactory());
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();
        }

        /// <summary>
        /// Initializes the auto mapper.
        /// </summary>
        protected void InitializeAutoMapper()
        {
            Mapper.CreateMap<Contact, CustomerModel>();
            Mapper.CreateMap<Participant, CandidateEntity>();
            //Mapper.CreateMap<List<Participant>, List<CandidateEntity>>();
        }

        /// <summary>
        /// Gets the Unity container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        private IUnityContainer Container
        {
            get { return _container; }
        }

        protected void Session_Start()
        {
            //var username = Environment.UserName;

            //var serviceFactory = Container.Resolve<ServiceFactory>();

            //var service = serviceFactory.CreateChannel<ICrmService>();

            //var associate = service.RetrieveAssociate(username);

            //Session["CurrentAssociate"] = associate;
        }
    }
}
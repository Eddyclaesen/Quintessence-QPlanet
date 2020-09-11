using System.Collections.Generic;
using System.Web.Mvc;
using Quintessence.QPlanet.Webshell.Areas.Admin.Controllers;
using Quintessence.QPlanet.Webshell.Areas.Candidate.Controllers;
using Quintessence.QPlanet.Webshell.Areas.Dictionary.Controllers;
using Quintessence.QPlanet.Webshell.Areas.Finance.Controllers;
using Quintessence.QPlanet.Webshell.Areas.Information.Controllers;
using Quintessence.QPlanet.Webshell.Areas.Project.Controllers;
using Quintessence.QPlanet.Webshell.Areas.SimulationSet.Controllers;
using Quintessence.QPlanet.Webshell.Areas.Workspace.Controllers;
using Quintessence.QPlanet.Webshell.Models.AreaInfo;

namespace Quintessence.QPlanet.Webshell.Controllers
{
    public class AreaInfoController : Controller
    {
        private const string MainArea = "Main";
        private const string ProjectArea = "Project";
        private const string WorkspaceArea = "Workspace";
        private const string DictionaryArea = "Dictionary";
        private const string SimulationSetArea = "SimulationSet";
        private const string CandidateArea = "Candidate";
        private const string FinanceArea = "Finance";
        private const string InformationArea = "Information";
        private const string AdminArea = "Admin";

        public ActionResult AreaNavigation()
        {
            var areaLinks = new List<AreaLink>
                                {
                                    new AreaLink
                                        {
                                            Area = string.Empty, //empty area is Home area
                                            Action = "Index",
                                            Controller = "Home",
                                            LinkText = MainArea,
                                            Current = CurrentArea == MainArea
                                        },
                                    new AreaLink
                                        {
                                            Area = ProjectArea,
                                            Action = "Index",
                                            Controller = "ProjectHome",
                                            LinkText = ProjectArea,
                                            Current = CurrentArea == ProjectArea
                                        },
                                    //new AreaLink
                                    //    {
                                    //        Area = InformationArea,
                                    //        Action = "Index",
                                    //        Controller = "InformationHome",
                                    //        LinkText = InformationArea,
                                    //        Current = CurrentArea == InformationArea
                                    //    },
                                    new AreaLink
                                        {
                                            Area = WorkspaceArea,
                                            Action = "Index",
                                            Controller = "WorkspaceDayProgram",
                                            LinkText = WorkspaceArea,
                                            Current = CurrentArea == WorkspaceArea
                                        },
                                    new AreaLink
                                        {
                                            Area = CandidateArea,
                                            Action = "Index",
                                            Controller = "Candidate",
                                            LinkText = CandidateArea,
                                            Current = CurrentArea == CandidateArea
                                        },
                                    new AreaLink
                                        {
                                            Area = FinanceArea,
                                            Action = "Index",
                                            Controller = "FinanceHome",
                                            LinkText = FinanceArea,
                                            Current = CurrentArea == FinanceArea
                                        },
                                    new AreaLink
                                        {
                                            Area = AdminArea,
                                            Action = "Index",
                                            Controller = "AdminHome",
                                            LinkText = AdminArea,
                                            Current = CurrentArea == AdminArea
                                        },
                                };

            return PartialView(areaLinks);
        }

        public ActionResult AreaActionNavigation()
        {
            switch (CurrentArea)
            {
                case MainArea:
                    return PartialView(CreateHomeAreaActionLinks());

                case ProjectArea:
                    return PartialView(CreateProjectAreaActionLinks());

                case InformationArea:
                    return PartialView(CreateInformationAreaActionLinks());

                case WorkspaceArea:
                    return PartialView(CreateWorkspaceAreaActionLinks());

                case DictionaryArea:
                    return PartialView(CreateDictionaryAreaActionLinks());

                case SimulationSetArea:
                    return PartialView(CreateSimulationSetAreaActionLinks());

                case CandidateArea:
                    return PartialView(CreateCandidateAreaActionLinks());

                case FinanceArea:
                    return PartialView(CreateFinanceAreaActionLinks());

                case AdminArea:
                    return PartialView(CreateAdminAreaActionLinks());
            }
            return HttpNotFound();
        }

        private IEnumerable<AreaActionLink> CreateHomeAreaActionLinks()
        {
            yield return new AreaActionLink { Area = string.Empty, Action = "Index", Controller = "Home", Current = IsCurrentControllerOfType<HomeController>(), LinkText = "Home", LinkTitle = "Home" };
        }

        private IEnumerable<AreaActionLink> CreateProjectAreaActionLinks()
        {
            yield return new AreaActionLink { Area = ProjectArea, Action = "Index", Controller = "ProjectHome", Current = IsCurrentControllerOfType<ProjectHomeController>(), LinkText = "Home", LinkTitle = "Home" };
            yield return new AreaActionLink { Area = ProjectArea, Action = "Index", Controller = "ProjectOverview", Current = IsCurrentControllerOfType<ProjectOverviewController>(), LinkText = "Overview", LinkTitle = "Overview" };
            //yield return new AreaActionLink { Area = ProjectArea, Action = "Index", Controller = "ProjectProposal", Current = IsCurrentControllerOfType<ProjectProposalController>(), LinkText = "Proposal", LinkTitle = "Proposal" };
            //yield return new AreaActionLink { Area = ProjectArea, Action = "Index", Controller = "ProjectFrameworkAgreement", Current = IsCurrentControllerOfType<ProjectFrameworkAgreementController>(), LinkText = "Agreements", LinkTitle = "Framework agreements"};
            //yield return new AreaActionLink { Area = ProjectArea, Action = "Index", Controller = "ProjectCandidateOverview", Current = IsCurrentControllerOfType<ProjectCandidateOverviewController>(), LinkText = "Candidates", LinkTitle = "Candidates"};
            //yield return new AreaActionLink { Area = ProjectArea, Action = "Index", Controller = "ProjectCandidateReportingOverview", Current = IsCurrentControllerOfType<ProjectCandidateReportingOverviewController>(), LinkText = "Reporting", LinkTitle = "Reporting"};
        }

        private IEnumerable<AreaActionLink> CreateInformationAreaActionLinks()
        {
            yield return new AreaActionLink { Area = InformationArea, Action = "Index", Controller = "InformationHome", Current = IsCurrentControllerOfType<InformationHomeController>(), LinkText = "Home", LinkTitle = "Home"};
            yield return new AreaActionLink { Area = InformationArea, Action = "Index", Controller = "InformationBusinessIntelligence", Current = IsCurrentControllerOfType<InformationBusinessIntelligenceController>(), LinkText = "Intelligence", LinkTitle = "Business Intelligence" };
        }

        private IEnumerable<AreaActionLink> CreateWorkspaceAreaActionLinks()
        {
            yield return new AreaActionLink { Area = WorkspaceArea, Action = "Index", Controller = "WorkspaceDayProgram", Current = IsCurrentControllerOfType<WorkspaceDayProgramController>(), LinkText = "Calendar", LinkTitle = "Calendar"};
            yield return new AreaActionLink { Area = WorkspaceArea, Action = "Index", Controller = "WorkspaceHome", Current = IsCurrentControllerOfType<WorkspaceHomeController>(), LinkText = "Home", LinkTitle = "Home"};
            yield return new AreaActionLink { Area = WorkspaceArea, Action = "Index", Controller = "WorkspaceTimesheet", Current = IsCurrentControllerOfType<WorkspaceTimesheetController>(), LinkText = "Timesheet", LinkTitle = "Timesheet"};
        }

        private IEnumerable<AreaActionLink> CreateDictionaryAreaActionLinks()
        {
            yield return new AreaActionLink { Area = DictionaryArea, Action = "Index", Controller = "DictionaryHome", Current = IsCurrentControllerOfType<DictionaryHomeController>(), LinkText = "Home", LinkTitle = "Home" };
        }

        private IEnumerable<AreaActionLink> CreateSimulationSetAreaActionLinks()
        {
            yield return new AreaActionLink { Area = SimulationSetArea, Action = "Index", Controller = "SimulationSetHome", Current = IsCurrentControllerOfType<SimulationSetHomeController>(), LinkText = "Home", LinkTitle = "Home" };
        }

        private IEnumerable<AreaActionLink> CreateCandidateAreaActionLinks()
        {
            yield return new AreaActionLink { Area = CandidateArea, Action = "Index", Controller = "Candidate", Current = IsCurrentControllerOfType<CandidateController>(), LinkText = "Home", LinkTitle = "Home" };
            yield return new AreaActionLink { Area = CandidateArea, Action = "IndexQa", Controller = "ProgramHomeQa", Current = IsCurrentControllerOfType<ProgramHomeQaController>(), LinkText = "Program QA", LinkTitle = "Program QA" };
            yield return new AreaActionLink { Area = CandidateArea, Action = "IndexQb", Controller = "ProgramHomeQb", Current = IsCurrentControllerOfType<ProgramHomeQbController>(), LinkText = "Program QB", LinkTitle = "Program QB" };
            yield return new AreaActionLink { Area = CandidateArea, Action = "IndexQg", Controller = "ProgramHomeQg", Current = IsCurrentControllerOfType<ProgramHomeQgController>(), LinkText = "Program QG", LinkTitle = "Program QG" };
            yield return new AreaActionLink { Area = CandidateArea, Action = "IndexEx", Controller = "ProgramHomeEx", Current = IsCurrentControllerOfType<ProgramHomeExController>(), LinkText = "Program EX", LinkTitle = "Program EX" };
            yield return new AreaActionLink { Area = CandidateArea, Action = "IndexOn", Controller = "ProgramHomeOn", Current = IsCurrentControllerOfType<ProgramHomeOnController>(), LinkText = "Program ON", LinkTitle = "Program ON" };
        }

        private IEnumerable<AreaActionLink> CreateFinanceAreaActionLinks()
        {
            yield return new AreaActionLink { Area = FinanceArea, Action = "Index", Controller = "FinanceHome", Current = IsCurrentControllerOfType<FinanceHomeController>(), LinkText = "Home", LinkTitle = "Home" };
        }

        private IEnumerable<AreaActionLink> CreateAdminAreaActionLinks()
        {
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminHome", Current = IsCurrentControllerOfType<AdminHomeController>(), LinkText = "Home", LinkTitle = "Home" };
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminProject", Current = IsCurrentControllerOfType<AdminProjectController>(), LinkText = "Project", LinkTitle = "Project" };
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminReport", Current = IsCurrentControllerOfType<AdminReportController>(), LinkText = "Reporting", LinkTitle = "Reporting" };
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminMailing", Current = IsCurrentControllerOfType<AdminMailingController>(), LinkText = "Mailing", LinkTitle = "Mailing" };
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminActivity", Current = IsCurrentControllerOfType<AdminActivityController>(), LinkText = "Act.", LinkTitle = "Activities" };
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminDictionary", Current = IsCurrentControllerOfType<AdminDictionaryController>(), LinkText = "Dic.", LinkTitle = "Dictionaries" };
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminSimulation", Current = IsCurrentControllerOfType<AdminSimulationController>(), LinkText = "Sim.", LinkTitle = "Simulations" };
            yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminUser", Current = IsCurrentControllerOfType<AdminUserController>(), LinkText = "Users", LinkTitle = "Users" };
            //yield return new AreaActionLink { Area = AdminArea, Action = "Index", Controller = "AdminJob", Current = IsCurrentControllerOfType<AdminJobController>(), LinkText = "Jobs", LinkTitle = "Jobs" };
        }

        private string CurrentArea
        {
            get { return (string)(ControllerContext.ParentActionViewContext.RouteData.DataTokens["area"] ?? MainArea); }
        }

        private bool IsCurrentControllerOfType<TController>()
            where TController : Controller
        {
            return ControllerContext.ParentActionViewContext.Controller.GetType() == typeof(TController);
        }
    }
}

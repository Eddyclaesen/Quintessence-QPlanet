using System;
using System.Linq;
using System.Collections.Generic;

using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess
{
    public class ReplicationDataAccess : IReplicationDataAccess
    {
        public string RetrieveCrmReplicationSettingByKey(string key, string defaultValue)
        {
            try
            {
                using (var dbCtx = new QuintessenceEntities())
                {
                    CrmReplicationSetting setting = dbCtx.CrmReplicationSettings.FirstOrDefault(s => s.Key == key);
                    return (setting != null && !String.IsNullOrEmpty(setting.Value)) ? setting.Value : defaultValue;                 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing QuintEssence database", ex);
            }         
        }

        public IDictionary<string, string> RetrieveCrmReplicationSettingsByKeys(IEnumerable<string> keys)
        {
            try
            {
                using (var dbCtx = new QuintessenceEntities())
                {
                    return dbCtx.CrmReplicationSettings.Where(s => keys.Contains(s.Key)).ToDictionary(s => s.Key, s => s.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing QuintEssence database", ex);
            }
        }

        public CrmReplicationJob RetrieveCrmReplicationJobByName(string jobName)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                return dbCtx.CrmReplicationJobs.FirstOrDefault(j => j.Name == jobName);
            }
        }

        public Guid? RetrieveCrmReplicationJobIdByName(string jobName)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationJob job = dbCtx.CrmReplicationJobs.FirstOrDefault(j => j.Name == jobName);
                return (job != null) ? job.Id : (Guid?)null;  // TODO: return job?.Id;
            }
        }

        public IEnumerable<CrmReplicationJob> RetrieveCrmReplicationJobs()
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                return dbCtx.CrmReplicationJobs.ToList();
            }
        }

        public void RegisterTeamLeaderEventError(int teamLeaderEventId, DateTime logDateUtc, string info)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationTeamLeaderEventErrorLog errorLog = new CrmReplicationTeamLeaderEventErrorLog
                {
                    CrmReplicationTeamLeaderEventId = teamLeaderEventId,
                    LogDateUtc = logDateUtc,
                    Info = info
                };

                dbCtx.CrmReplicationTeamLeaderEventErrorLogs.Add(errorLog);
                dbCtx.SaveChanges();
            } 
        }

        public Guid RegisterCrmReplicationJobHistory(Guid crmReplicationJobId, DateTime startDate, DateTime? endDate, bool succeeded, string info)
        {
            CrmReplicationJobHistory crmReplicationJobHistory = new CrmReplicationJobHistory
            {
                Id = Guid.NewGuid(),
                CrmReplicationJobId = crmReplicationJobId,
                StartDate = startDate,
                EndDate = endDate,
                Succeeded = succeeded
            };

            using (var dbCtx = new QuintessenceEntities())
            {
                dbCtx.CrmReplicationJobHistories.Add(crmReplicationJobHistory);
                dbCtx.SaveChanges();
            }
            return crmReplicationJobHistory.Id;
        }

        public Guid RegisterCrmReplicationJobHistoryStart(Guid crmReplicationJobId)
        {
            return RegisterCrmReplicationJobHistory(crmReplicationJobId, DateTime.Now, null, false, null);
        }

        public void RegisterCrmReplicationJobHistoryEnd(Guid crmReplicationJobHistoryId, bool succeeded, string info)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationJobHistory jobHistory = dbCtx.CrmReplicationJobHistories.FirstOrDefault(j => j.Id == crmReplicationJobHistoryId);
                if (jobHistory != null)
                {
                    jobHistory.EndDate = DateTime.Now;
                    jobHistory.Succeeded = succeeded;
                    jobHistory.Info = info;
                    dbCtx.SaveChanges();
                }
            }
        }

        public IEnumerable<CrmReplicationTeamLeaderEvent> RetrieveCrmReplicationTeamLeaderEvents(int maxEventsToReturn, int maxProcessCount)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                return dbCtx.CrmReplicationTeamLeaderEvents.Where(e => e.ProcessCount < maxProcessCount).OrderBy(e => e.ReceivedUtc).Take(maxEventsToReturn).ToList();
            }
        }

        public void DeleteCrmReplicationreamLeaderEventById(int id)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                var teamLeaderEvent = new CrmReplicationTeamLeaderEvent { Id = id };
                dbCtx.CrmReplicationTeamLeaderEvents.Attach(teamLeaderEvent);
                dbCtx.CrmReplicationTeamLeaderEvents.Remove(teamLeaderEvent);
                dbCtx.SaveChanges();
            }
        }

        public void UpdateCrmReplicationreamLeaderEventProcessCountById(int id)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationTeamLeaderEvent teamLeaderEvent = dbCtx.CrmReplicationTeamLeaderEvents.FirstOrDefault(e => e.Id == id);
                if (teamLeaderEvent != null)
                {
                    teamLeaderEvent.ProcessCount++;
                    dbCtx.SaveChanges();
                }
            }
        }

        public int? RetrieveCrmUserGroupByName(string userGroupName)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationUserGroup userGroup = dbCtx.CrmReplicationUserGroups.FirstOrDefault(ug => ug.Name == userGroupName);
                return (userGroup != null) ? userGroup.Id : (int?)null; // TODO: return userGroup?.Id;
            }
        }


        public void EnrichTeamLeaderUsersWithAssociateId(IEnumerable<User> users)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (var user in users)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderId == user.Id);
                    if (associate != null)
                        user.AssociateId = associate.Id;                    
                }
            }
        }

        public void EnrichTeamLeaderProjectsWithStatusId(IEnumerable<Project> projects)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (var project in projects)
                {
                    CrmReplicationProjectStatusMapping projectStatusMapping = dbCtx.CrmReplicationProjectStatusMappings.FirstOrDefault(psm => psm.SourceValue == project.Status);
                    if (projectStatusMapping != null)
                        project.StatusId = projectStatusMapping.TargetId;
                }
            }
        }

        public void EnrichTeamLeaderProjectWithAssociateId(Project project, IEnumerable<ProjectUser> projectUsers)
        {
            ProjectUser associateProjectUser = projectUsers.FirstOrDefault(pu => pu.Role == TeamLeader.Constants.ProjectUserRoles.DecisionMaker);
            if (associateProjectUser != null)
            {
                using (var dbCtx = new QuintessenceEntities())
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderId == associateProjectUser.UserId);
                    if (associate != null)
                    {
                        project.AssociateId = associate.Id;
                    }
                }       

            }
        }

        public void EnrichTeamLeaderProjectsWithCompanyId(IEnumerable<Project> projects)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (var project in projects)
                {
                    if (project.ContactOrCompany == TeamLeader.Constants.ContactOrCompany.Company && !String.IsNullOrEmpty(project.ContactOrCompanyId))
                    {
                        int contactOrCompanyId;
                        if (int.TryParse(project.ContactOrCompanyId, out contactOrCompanyId))
                        {
                            CrmReplicationContact contact = dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.TeamLeaderId == contactOrCompanyId);
                            if (contact != null)
                            {
                                project.ContactId = contact.Id;
                            }  
                        }
                    }                  
                }
            }
        }

        public int SyncFullCrmProject(Project project)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationProject projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.TeamLeaderId == project.Id);
                if (projectDb == null && project.LegacyId.HasValue)
                    projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.Id == project.LegacyId.Value);

                if (projectDb == null)
                {
                    int maxId = dbCtx.CrmReplicationProjects.Max(ps => ps.Id) + 1;

                    CrmReplicationProject newProject = new CrmReplicationProject
                    {
                        Id = maxId,
                        Name = project.Title,
                        AssociateId = project.AssociateId,
                        ContactId = project.ContactId,
                        ProjectStatusId = project.StatusId,
                        StartDate = project.BoekJaarStartDate,
                        BookyearFrom = project.BoekJaarStartDate,
                        BookyearTo = project.BoekJaarEndDate,
                        TeamLeaderId = project.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationProjects.Add(newProject);
                }
                else
                {
                    projectDb.Name = project.Title;
                    projectDb.AssociateId = project.AssociateId;
                    projectDb.ContactId = project.ContactId;
                    projectDb.ProjectStatusId = project.StatusId;
                    projectDb.BookyearFrom = project.BoekJaarStartDate;
                    projectDb.BookyearTo = project.BoekJaarEndDate;
                    projectDb.TeamLeaderId = project.Id;
                    projectDb.LastSyncedUtc = DateTime.UtcNow;
                    projectDb.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public int SyncCrmProjectIds(IEnumerable<Project> projects)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (Project project in projects)
                {
                    CrmReplicationProject projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.Id == project.LegacyId);
                    if (projectDb != null && projectDb.TeamLeaderId != project.Id)
                    {
                        projectDb.TeamLeaderId = project.Id;
                        projectDb.LastSyncedUtc = DateTime.UtcNow;
                        projectDb.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationProjectByTeamLeaderId(int projectTeamLeaderId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationProject projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.TeamLeaderId == projectTeamLeaderId);
                if (projectDb != null)
                {
                    dbCtx.CrmReplicationProjects.Attach(projectDb);
                    dbCtx.CrmReplicationProjects.Remove(projectDb);
                    dbCtx.SaveChanges();
                }
            }
        }

        public bool IsQPlanetProject(int? projectId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                return dbCtx.Project2CrmProject.Any(p => p.CrmProjectId == projectId);
            }
        }

        public int SyncCrmAssociates(IEnumerable<User> users)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                int maxId = dbCtx.CrmReplicationAssociates.Max(ps => ps.Id) + 1;
                int? userGroupId = null;
                foreach (var user in users)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderName.Equals(user.Name, StringComparison.OrdinalIgnoreCase));
                    if (associate == null)
                    {
                        if (!userGroupId.HasValue)
                        {
                            CrmReplicationUserGroup fromTeamLeaderUserGroup = dbCtx.CrmReplicationUserGroups.FirstOrDefault(ug => ug.Name == Constants.UserGroupNames.FromTeamLeader);
                            if (fromTeamLeaderUserGroup != null)
                                userGroupId = fromTeamLeaderUserGroup.Id;
                        }

                        CrmReplicationAssociate newAssociate = new CrmReplicationAssociate
                        {
                            Id = maxId++,
                            UserName = user.Name,
                            UserGroupId = userGroupId,
                            TeamLeaderId = user.Id,
                            TeamLeaderName = user.Name,
                            LastSyncedUtc = DateTime.UtcNow,
                            SyncVersion = 1
                        };
                        if (!String.IsNullOrEmpty(user.Name) && user.Name.Contains(" "))
                        {
                            string[] parts = user.Name.Split(' ');
                            if (parts.Length >= 1)
                                newAssociate.FirstName = parts[0];
                            if (parts.Length >= 2)
                                newAssociate.LastName = user.Name.Substring(parts[0].Length + 1);
                        }
                        dbCtx.CrmReplicationAssociates.Add(newAssociate);
                    }
                    else if (associate.TeamLeaderId != user.Id)
                    {
                        associate.TeamLeaderId = user.Id;
                        associate.LastSyncedUtc = DateTime.UtcNow;
                        associate.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public int SyncCrmAssociateEmails(IEnumerable<User> users)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                int maxId = dbCtx.CrmReplicationEmailAssociates.Max(ea => ea.Id) + 1;
                foreach (var user in users)
                {
                    CrmReplicationEmailAssociate emailAssociate = dbCtx.CrmReplicationEmailAssociates.FirstOrDefault(ea => ea.AssociateId == user.AssociateId);
                    if (emailAssociate == null)
                    {
                        CrmReplicationEmailAssociate newEmailAssociate = new CrmReplicationEmailAssociate
                        {
                            Id = maxId++,
                            AssociateId = user.AssociateId,
                            Email = user.Email,
                            Rank = 1,
                            LastSyncedUtc = DateTime.UtcNow,
                            SyncVersion = 1
                        };
                        dbCtx.CrmReplicationEmailAssociates.Add(newEmailAssociate);                        
                    }
                    else if (emailAssociate.Email != user.Email)
                    {
                        emailAssociate.Email = user.Email;
                        emailAssociate.Rank = 1;
                        emailAssociate.LastSyncedUtc = DateTime.UtcNow;
                        emailAssociate.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public CrmReplicationContact RetrieveCrmReplicationContactByTeamLeaderId(int companyTeamleaderId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                return dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.TeamLeaderId == companyTeamleaderId);
            }
        }

        public void EnrichTeamLeaderCompanies(IEnumerable<Company> companies)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                Dictionary<string, int> cache = new Dictionary<string, int>();

                foreach (var company in companies)
                {
                    // AccountManager
                    if (cache.ContainsKey(company.AccountManager))
                        company.AccountManagerId = cache[company.AccountManager];
                    else
                    {
                        CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderName == company.AccountManager);
                        if (associate != null)
                        {
                            company.AccountManagerId = associate.Id;
                            cache.Add(company.AccountManager, associate.Id);
                        }
                    }
                    // Assistent
                    if (cache.ContainsKey(company.Assistent))
                        company.AssistentId = cache[company.Assistent];
                    else
                    {
                        CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderName == company.Assistent);
                        if (associate != null)
                        {
                            company.AssistentId = associate.Id;
                            cache.Add(company.Assistent, associate.Id);
                        }
                    }
                }
            }
        }

        public void EnrichTeamLeaderCompanyWithUserIds(Company company)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                int associateId;
                if (int.TryParse(company.Associate, out associateId))
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderId == associateId);
                    if (associate != null)
                        company.AssociateId = associate.Id;
                }
                if (int.TryParse(company.AccountManager, out associateId))
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderId == associateId);
                    if (associate != null)
                        company.AccountManagerId = associate.Id;
                }
                if (int.TryParse(company.Assistent, out associateId))
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderId == associateId);
                    if (associate != null)
                        company.AssistentId = associate.Id;
                }
            }  
        }

        public int SyncFullCrmContact(Company company)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationContact contact = dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.TeamLeaderId == company.Id);
                if (contact == null)
                {
                    int maxId = dbCtx.CrmReplicationContacts.Max(ea => ea.Id) + 1;

                    CrmReplicationContact newContact = new CrmReplicationContact
                    {
                        Id = maxId,
                        Name = company.Name,
                        Department = company.Afdeling,

                        AssociateId = company.AssociateId,
                        AccountManagerId = company.AccountManagerId,
                        CustomerAssistantId = company.AssistentId,
                        TeamLeaderId = company.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationContacts.Add(newContact);
                }
                else if (company.HasUpDates(contact))
                {
                    contact.Name = company.Name;
                    contact.Department = company.Afdeling;

                    contact.AssociateId = company.AssociateId;
                    contact.AccountManagerId = company.AccountManagerId;
                    contact.CustomerAssistantId = company.AssistentId;
                    contact.LastSyncedUtc = DateTime.UtcNow;
                    contact.SyncVersion++;
                }            
                return dbCtx.SaveChanges();
            }
        }

        public int SyncCrmContactIds(IEnumerable<Company> companies)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (Company company in companies)
                {
                    CrmReplicationContact contactDb = dbCtx.CrmReplicationContacts.FirstOrDefault(p => p.Id == company.LegacyId);
                    if (contactDb != null && contactDb.TeamLeaderId != company.Id)
                    {
                        contactDb.TeamLeaderId = company.Id;
                        contactDb.LastSyncedUtc = DateTime.UtcNow;
                        contactDb.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationContactAndEmailByTeamLeaderId(int companyTeamleaderId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationContact contact = dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.TeamLeaderId == companyTeamleaderId);
                if (contact != null)
                {
                    dbCtx.CrmReplicationEmails.RemoveRange(dbCtx.CrmReplicationEmails.Where(e => e.ContactId == contact.Id));
                    dbCtx.SaveChanges();

                    dbCtx.CrmReplicationContacts.Attach(contact);
                    dbCtx.CrmReplicationContacts.Remove(contact);
                    dbCtx.SaveChanges();
                }
            }
        }

        public int SyncCrmPersonIds(IEnumerable<Contact> contacts)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (Contact contact in contacts)
                {
                    CrmReplicationPerson personDb = dbCtx.CrmReplicationPersons.FirstOrDefault(p => p.Id == contact.LegacyId);
                    if (personDb != null && personDb.TeamLeaderId != contact.Id)
                    {
                        personDb.TeamLeaderId = contact.Id;
                        personDb.LastSyncedUtc = DateTime.UtcNow;
                        personDb.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public void EnrichTeamLeaderContactWithCompanyInfo(IEnumerable<Contact> contacts)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (Contact contact in contacts)
                {
                    int companyId;
                    if (int.TryParse(contact.LinkedCompanyIds, out companyId))
                    {
                        CrmReplicationContact company = dbCtx.CrmReplicationContacts.FirstOrDefault(a => a.TeamLeaderId == companyId);
                        if (company != null)
                        {
                            contact.LinkedCompanyId = company.Id;
                            contact.LinkedCompanyName = company.Name;
                        }
                    }
                }
            }  
        }

        public void EnrichTeamLeaderContactWithLegacyId(IEnumerable<Contact> contacts)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (Contact contact in contacts)
                {
                    contact.LegacyId = null;
                    CrmReplicationPerson person = dbCtx.CrmReplicationPersons.FirstOrDefault(p => p.TeamLeaderId == contact.Id);
                    if (person != null)
                        contact.LegacyId = person.Id;
                }
            }   
        }

        public int SyncFullCrmPerson(Contact contact)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationPerson person = dbCtx.CrmReplicationPersons.FirstOrDefault(c => c.TeamLeaderId == contact.Id);
                if (person == null)
                {
                    int maxId = dbCtx.CrmReplicationPersons.Max(ea => ea.Id) + 1;

                    CrmReplicationPerson newPerson = new CrmReplicationPerson
                    {
                        Id = maxId,
                        ContactId = contact.LinkedCompanyId,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Title = contact.Positie,
                        IsRetired = contact.IsFormerEmployee,
                        TeamLeaderId = contact.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationPersons.Add(newPerson);
                }
                else
                {
                    person.ContactId = contact.LinkedCompanyId;
                    person.FirstName = contact.FirstName;
                    person.LastName = contact.LastName;
                    person.Title = contact.Positie;
                    person.IsRetired = contact.IsFormerEmployee;
                    person.LastSyncedUtc = DateTime.UtcNow;
                    person.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationPersonAndEmailByTeamLeaderId(int contactTeamleaderId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationPerson person = dbCtx.CrmReplicationPersons.FirstOrDefault(c => c.TeamLeaderId == contactTeamleaderId);
                if (person != null)
                {
                    dbCtx.CrmReplicationEmails.RemoveRange(dbCtx.CrmReplicationEmails.Where(e => e.PersonId == person.Id));
                    dbCtx.SaveChanges();

                    dbCtx.CrmReplicationPersons.Attach(person);
                    dbCtx.CrmReplicationPersons.Remove(person);
                    dbCtx.SaveChanges();
                }
            }
        }

        public void EnrichTeamLeaderCompanyContacts(IEnumerable<CompanyContact> companyContacts)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (CompanyContact companyContact in companyContacts)
                {
                    CrmReplicationPerson person = dbCtx.CrmReplicationPersons.FirstOrDefault(p => p.TeamLeaderId == companyContact.ContactTeamLeaderId);
                    if (person != null)
                    {
                        companyContact.ContactLegacyId = person.Id;
                        companyContact.FirstName = person.FirstName;
                        companyContact.LastName = person.LastName;                        
                    }
                }
            } 
        }

        public int SyncFullCrmCompanyContactRelation(CrmReplicationContact company, IEnumerable<CompanyContact> companyContacts)
        {
            int affected = 0;
            using (var dbCtx = new QuintessenceEntities())
            {
                List<CompanyContact> contacts = new List<CompanyContact>(companyContacts.Where(cc => cc.ContactLegacyId.HasValue));

                // Delete
                List<CrmReplicationEmail> existingContactsForCompany = dbCtx.CrmReplicationEmails.Where(e => e.ContactId == company.Id).ToList();
                foreach (CrmReplicationEmail email in existingContactsForCompany)
                {
                    if (!contacts.Any(cc => cc.ContactLegacyId == email.Id)) // If existing is no longer in the incoming list, delete it.
                    {
                        dbCtx.CrmReplicationEmails.Attach(email);
                        dbCtx.CrmReplicationEmails.Remove(email);
                        affected += dbCtx.SaveChanges();
                    }
                }

                int maxId = dbCtx.CrmReplicationEmails.Max(e => e.Id) + 1;

                // Update ors Add
                foreach (CompanyContact companyContact in contacts)
                {
                    CrmReplicationEmail email = dbCtx.CrmReplicationEmails.FirstOrDefault(e => e.ContactId == company.Id && e.PersonId == companyContact.ContactLegacyId);
                    if (email == null)
                    {
                        CrmReplicationEmail newEmail = new CrmReplicationEmail
                        {
                            Id = maxId++,
                            PersonId = companyContact.ContactLegacyId,
                            ContactId = company.Id,
                            ContactName = company.Name,
                            FirstName = companyContact.FirstName,
                            LastName =  companyContact.LastName,
                            Email = companyContact.Email,
                            DirectPhone = companyContact.DirectPhone,
                            MobilePhone = companyContact.MobilePhone,
                            LastSyncedUtc = DateTime.UtcNow,
                            SyncVersion = 1
                        };
                        dbCtx.CrmReplicationEmails.Add(newEmail);
                    }
                    else
                    {
                        email.FirstName = companyContact.FirstName;
                        email.LastName = companyContact.LastName;
                        email.Email = companyContact.Email;
                        email.DirectPhone = companyContact.DirectPhone;
                        email.MobilePhone = companyContact.MobilePhone;
                        email.LastSyncedUtc = DateTime.UtcNow;
                        email.SyncVersion++;
                    }
                }
                affected+= dbCtx.SaveChanges();
            }
            return affected; 
        }

        public int SyncFullCrmEmail(Contact contact)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationEmail email = dbCtx.CrmReplicationEmails.FirstOrDefault(e => e.ContactId == contact.LinkedCompanyId && e.PersonId == contact.LegacyId);
                if (email == null)
                {
                    int maxId = dbCtx.CrmReplicationEmails.Max(e => e.Id) + 1;

                    CrmReplicationEmail newEmail = new CrmReplicationEmail
                    {
                        Id = maxId,
                        PersonId = contact.Id,
                        ContactId = contact.LinkedCompanyId,
                        ContactName = contact.LinkedCompanyName,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        DirectPhone = contact.Telephone,
                        MobilePhone = contact.Gsm,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationEmails.Add(newEmail);
                }
                else
                {
                    email.FirstName = contact.FirstName;
                    email.LastName = contact.LastName;
                    email.Email = contact.Email;
                    email.DirectPhone = contact.Telephone;
                    email.MobilePhone = contact.Gsm;
                    email.LastSyncedUtc = DateTime.UtcNow;
                    email.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void EnrichTeamLeaderTask(TeamLeaderTask task)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationProject project = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.TeamLeaderId == task.ProjectId);
                if (project != null)
                    task.ProjectLegacyId = project.Id;

                if (!String.IsNullOrEmpty(task.Lokatie))
                {
                    Office office = dbCtx.Offices.FirstOrDefault(o => o.ShortName == task.Lokatie);
                    if(office != null)
                        task.OfficeId = office.Id;
                }

                if (task.ResponsibleUser.HasValue)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderId == task.ResponsibleUser);
                    if (associate != null)
                        task.AssociateId = associate.Id;
                }

                CrmReplicationAppointmentTaskMapping taskMapping = dbCtx.CrmReplicationAppointmentTaskMappings.FirstOrDefault(tm => tm.Description == task.TaskDescription);
                if (taskMapping != null)
                    task.TaskId = taskMapping.TaskId;              
            }  
        }

        public int SyncFullCrmAppointment(TeamLeaderTask task)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointment appointment = dbCtx.CrmReplicationAppointments.FirstOrDefault(a => a.TeamLeaderId == task.Id);
                if (appointment == null)
                {
                    int maxId = dbCtx.CrmReplicationAppointments.Max(a => a.Id) + 1;

                    CrmReplicationAppointment newAppointment = new CrmReplicationAppointment
                    {
                        Id = maxId,
                        AppointmentDate = task.AppointmentStartDateTime,
                        EndDate = task.AppointmentEndDateTime,
                        AssociateId = task.AssociateId,
                        IsReserved = task.IsReserved,
                        OfficeId = task.OfficeId,
                        LanguageId = task.LanguageId,
                        Gender = task.Gender,
                        Code = task.Code,
                        FirstName = task.FirstName,
                        LastName =  task.LastName,
                        CrmProjectId = task.ProjectLegacyId,
                        TaskId = task.TaskId,
                        Description = task.Description,
                        TeamLeaderId = task.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationAppointments.Add(newAppointment);
                }
                else
                {
                    appointment.AppointmentDate = task.AppointmentStartDateTime;
                    appointment.EndDate = task.AppointmentEndDateTime;
                    appointment.AssociateId = task.AssociateId;
                    appointment.IsReserved = task.IsReserved;
                    appointment.OfficeId = task.OfficeId;
                    appointment.LanguageId = task.LanguageId;
                    appointment.Gender = task.Gender;
                    appointment.Code = task.Code;
                    appointment.FirstName = task.FirstName;
                    appointment.LastName = task.LastName;
                    appointment.CrmProjectId = task.ProjectLegacyId;
                    appointment.TaskId = task.TaskId;
                    appointment.Description = task.Description;
                    appointment.LastSyncedUtc = DateTime.UtcNow;
                    appointment.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationAppointmentByTeamLeaderId(int teamLeaderTaskId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointment appointment = dbCtx.CrmReplicationAppointments.FirstOrDefault(c => c.TeamLeaderId == teamLeaderTaskId);
                if (appointment != null)
                {
                    dbCtx.CrmReplicationAppointments.Attach(appointment);
                    dbCtx.CrmReplicationAppointments.Remove(appointment);
                    dbCtx.SaveChanges();
                }
            }
        }

        public int SyncFullCrmAppointmentTimeSheet(TimeTracking timeTracking)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointmentTimesheet appointmentTimeSheet = dbCtx.CrmReplicationAppointmentTimesheets.FirstOrDefault(a => a.TeamLeaderId == timeTracking.Id);
                if (appointmentTimeSheet == null)
                {
                    int maxId = dbCtx.CrmReplicationAppointmentTimesheets.Max(a => a.Id) + 1;

                    CrmReplicationAppointmentTimesheet newAppointmentTimeSheet = new CrmReplicationAppointmentTimesheet
                    {
                        Id = maxId,
                        ProjectId = timeTracking.ProjectLegacyId,
                        AssociateId = timeTracking.AssociateId,
                        StartDate = timeTracking.StartDate,
                        EndDate = timeTracking.EndDate,
                        Description = timeTracking.Description,
                        TaskDescription = timeTracking.ProjectTitle,
                        TeamLeaderId = timeTracking.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationAppointmentTimesheets.Add(newAppointmentTimeSheet);
                }
                else
                {
                    appointmentTimeSheet.ProjectId = timeTracking.ProjectLegacyId;
                    appointmentTimeSheet.AssociateId = timeTracking.AssociateId;
                    appointmentTimeSheet.StartDate = timeTracking.StartDate;
                    appointmentTimeSheet.EndDate = timeTracking.EndDate;
                    appointmentTimeSheet.Description = timeTracking.Description;
                    appointmentTimeSheet.TaskDescription = timeTracking.ProjectTitle;

                    appointmentTimeSheet.LastSyncedUtc = DateTime.UtcNow;
                    appointmentTimeSheet.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationAppointmentTimeSheetByTeamLeaderId(int teamLeaderTimeTrackingId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointmentTimesheet appointmentTimeSheet = dbCtx.CrmReplicationAppointmentTimesheets.FirstOrDefault(c => c.TeamLeaderId == teamLeaderTimeTrackingId);
                if (appointmentTimeSheet != null)
                {
                    dbCtx.CrmReplicationAppointmentTimesheets.Attach(appointmentTimeSheet);
                    dbCtx.CrmReplicationAppointmentTimesheets.Remove(appointmentTimeSheet);
                    dbCtx.SaveChanges();
                }
            }   
        }

        public void EnrichTeamLeaderTimeTracking(TimeTracking timeTracking)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderId == timeTracking.UserId);
                if (associate != null)
                    timeTracking.AssociateId = associate.Id;
                
                CrmReplicationProject project = dbCtx.CrmReplicationProjects.FirstOrDefault(a => a.TeamLeaderId == timeTracking.ProjectId);
                if (project != null)
                    timeTracking.ProjectLegacyId = project.Id;

            } 
        }
    }
}
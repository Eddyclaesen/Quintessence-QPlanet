using System;
using System.Linq;
using System.Collections.Generic;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess
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

        public void RegisterSuperOfficeEventError(int superOfficeEventId, DateTime logDateUtc, string info)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationSuperOfficeEventErrorLog errorLog = new CrmReplicationSuperOfficeEventErrorLog
                {
                    CrmReplicationSuperOfficeEventId = superOfficeEventId,
                    LogDateUtc = logDateUtc,
                    Info = info
                };

                dbCtx.CrmReplicationSuperOfficeEventErrorLogs.Add(errorLog);
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

        public IEnumerable<CrmReplicationSuperOfficeEvent> RetrieveCrmReplicationSuperOfficeEvents(int maxEventsToReturn, int maxProcessCount)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                return dbCtx.CrmReplicationSuperOfficeEvents.Where(e => e.ProcessCount < maxProcessCount).OrderBy(e => e.ReceivedUtc).Take(maxEventsToReturn).ToList();
            }
        }

        public void DeleteCrmReplicationSuperOfficeEventById(int id)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                var superOfficeEvent = new CrmReplicationSuperOfficeEvent { Id = id };
                dbCtx.CrmReplicationSuperOfficeEvents.Attach(superOfficeEvent);
                dbCtx.CrmReplicationSuperOfficeEvents.Remove(superOfficeEvent);
                dbCtx.SaveChanges();
            }
        }

        public void UpdateCrmReplicationSuperOfficeProcessCountById(int id)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationSuperOfficeEvent superOfficeEvent = dbCtx.CrmReplicationSuperOfficeEvents.FirstOrDefault(e => e.Id == id);
                if (superOfficeEvent != null)
                {
                    superOfficeEvent.ProcessCount++;
                    dbCtx.SaveChanges();
                }
            }
        }

    //    public int? RetrieveCrmUserGroupByName(string userGroupName)
    //    {
    //        using (var dbCtx = new QuintessenceEntities())
    //        {
    //            CrmReplicationUserGroup userGroup = dbCtx.CrmReplicationUserGroups.FirstOrDefault(ug => ug.Name == userGroupName);
    //            return (userGroup != null) ? userGroup.Id : (int?)null; // TODO: return userGroup?.Id;
    //        }
    //    }


        public void EnrichSuperOfficeUsersWithAssociateId(IEnumerable<User> users)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (var user in users)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.SuperOfficeId == user.Id);
                    if (associate != null)
                        user.AssociateId = associate.Id;
                }
            }
        }

        public void EnrichSuperOfficeProjectsWithStatusId(IEnumerable<Project> projects)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (var project in projects)
                {
                    CrmReplicationProjectStatusMapping projectStatusMapping = dbCtx.CrmReplicationProjectStatusMappings.FirstOrDefault(psm => psm.SourceValue == project.ProjectStatusValue);
                    if (projectStatusMapping != null)
                        project.StatusId = projectStatusMapping.TargetId;
                }
            }
        }

        public void EnrichSuperOfficeProjectWithAssociateId(Project project)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.SuperOfficeId == project.AssociateId);
                if (associate != null)
                {
                    project.AssociateLegacyId = associate.Id;
                }
            }
        }

        public void EnrichSuperOfficeProjectsWithCompanyId(IEnumerable<Project> projects)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (var project in projects)
                {
                    if (project.ContactId.HasValue)
                    {
                        CrmReplicationContact contact = dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.SuperOfficeId == project.ContactId.Value);
                        if (contact != null)
                        {
                            project.ContactLegacyId = contact.Id;
                        }
                    }
                }
            }
        }

        public int SyncFullCrmProject(Project project)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationProject projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.SuperOfficeId.Value == project.Id);
                if (projectDb == null && project.LegacyId.HasValue)
                    projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.Id == project.LegacyId.Value);

                if (projectDb == null)
                {
                    int maxId = dbCtx.CrmReplicationProjects.Max(ps => ps.Id) + 1;

                    CrmReplicationProject newProject = new CrmReplicationProject
                    {
                        Id = maxId,
                        Name = project.Name,
                        AssociateId = project.AssociateLegacyId,
                        ContactId = project.ContactLegacyId,
                        ProjectStatusId = project.StatusId,
                        StartDate = project.BoekJaarStartDate,
                        BookyearFrom = project.BoekJaarStartDate,
                        BookyearTo = project.BoekJaarEndDate,
                        SuperOfficeId = project.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationProjects.Add(newProject);
                }
                else
                {
                    projectDb.Name = project.Name;
                    projectDb.AssociateId = project.AssociateLegacyId;
                    projectDb.ContactId = project.ContactLegacyId;
                    projectDb.ProjectStatusId = project.StatusId;
                    projectDb.BookyearFrom = project.BoekJaarStartDate;
                    projectDb.BookyearTo = project.BoekJaarEndDate;
                    projectDb.SuperOfficeId = project.Id;
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
                    CrmReplicationProject projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.TeamLeaderId == project.LegacyId);
                    if (projectDb != null && projectDb.SuperOfficeId != project.Id)
                    {
                        projectDb.SuperOfficeId = project.Id;
                        projectDb.LastSyncedUtc = DateTime.UtcNow;
                        projectDb.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationProjectBySuperOfficeId(int projectSuperOfficeId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationProject projectDb = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.SuperOfficeId.Value == projectSuperOfficeId);
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
                    if (String.IsNullOrEmpty(user.FullName))
                        continue;

                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderName.Equals(user.FullName, StringComparison.OrdinalIgnoreCase));
                    if (associate == null)
                    {
                        if (!userGroupId.HasValue)
                        {
                            CrmReplicationUserGroup fromTeamLeaderUserGroup = dbCtx.CrmReplicationUserGroups.FirstOrDefault(ug => ug.Name == Constants.UserGroupNames.FromSuperOffice);
                            if (fromTeamLeaderUserGroup != null)
                                userGroupId = fromTeamLeaderUserGroup.Id;
                        }

                        CrmReplicationAssociate newAssociate = new CrmReplicationAssociate
                        {
                            Id = maxId++,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            TeamLeaderName = user.FullName,   // This avoids double insertions,since we use this field to find the user
                            UserGroupId = userGroupId,
                            SuperOfficeId = user.Id,                            
                            LastSyncedUtc = DateTime.UtcNow,
                            SyncVersion = 1
                        };
                        dbCtx.CrmReplicationAssociates.Add(newAssociate);
                    }
                    else if (associate.SuperOfficeId != user.Id)
                    {
                        associate.SuperOfficeId = user.Id;
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
                    if (String.IsNullOrEmpty(user.Email))
                        continue;

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

    //    public CrmReplicationContact RetrieveCrmReplicationContactByTeamLeaderId(int companyTeamleaderId)
    //    {
    //        using (var dbCtx = new QuintessenceEntities())
    //        {
    //            return dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.TeamLeaderId == companyTeamleaderId);
    //        }
    //    }

    //    public void EnrichTeamLeaderCompanies(IEnumerable<Company> companies)
    //    {
    //        using (var dbCtx = new QuintessenceEntities())
    //        {
    //            Dictionary<string, int> cache = new Dictionary<string, int>();

    //            foreach (var company in companies)
    //            {
    //                // AccountManager
    //                if (cache.ContainsKey(company.AccountManager))
    //                    company.AccountManagerId = cache[company.AccountManager];
    //                else
    //                {
    //                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderName == company.AccountManager);
    //                    if (associate != null)
    //                    {
    //                        company.AccountManagerId = associate.Id;
    //                        cache.Add(company.AccountManager, associate.Id);
    //                    }
    //                }
    //                // Assistent
    //                if (cache.ContainsKey(company.Assistent))
    //                    company.AssistentId = cache[company.Assistent];
    //                else
    //                {
    //                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.TeamLeaderName == company.Assistent);
    //                    if (associate != null)
    //                    {
    //                        company.AssistentId = associate.Id;
    //                        cache.Add(company.Assistent, associate.Id);
    //                    }
    //                }
    //            }
    //        }
    //    }

        public void EnrichSuperOfficeCompanyWithCrmIds(Company company)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                if (company.AssociateId.HasValue)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.SuperOfficeId == company.AssociateId.Value);
                    if (associate != null)
                        company.AssociateCrmId = associate.Id;
                }
                if (company.AccountManagerId.HasValue)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.SuperOfficeId == company.AccountManagerId.Value);
                    if (associate != null)
                        company.AccountManagerCrmId = associate.Id;
                }
                if (company.AssistentId.HasValue)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.SuperOfficeId == company.AssistentId.Value);
                    if (associate != null)
                        company.AssistentCrmId = associate.Id;
                }
            }
        }

        public int SyncFullCrmContact(Company company)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationContact contact = dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.SuperOfficeId == company.Id);
                if (contact == null)
                {
                    int maxId = dbCtx.CrmReplicationContacts.Max(ea => ea.Id) + 1;

                    CrmReplicationContact newContact = new CrmReplicationContact
                    {
                        Id = maxId,
                        Name = company.Name,
                        Department = company.Department,

                        AssociateId = company.AssociateCrmId,
                        AccountManagerId = company.AccountManagerCrmId,
                        CustomerAssistantId = company.AssistentCrmId,
                        SuperOfficeId = company.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationContacts.Add(newContact);
                }
                else
                {
                    contact.Name = company.Name;
                    contact.Department = company.Department;

                    contact.AssociateId = company.AssociateCrmId;
                    contact.AccountManagerId = company.AccountManagerCrmId;
                    contact.CustomerAssistantId = company.AssistentCrmId;
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
                    CrmReplicationContact contactDb = dbCtx.CrmReplicationContacts.FirstOrDefault(p => p.TeamLeaderId == company.LegacyId);
                    if (contactDb != null && contactDb.SuperOfficeId != company.Id)
                    {
                        contactDb.SuperOfficeId = company.Id;
                        contactDb.LastSyncedUtc = DateTime.UtcNow;
                        contactDb.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationContactAndEmailBySuperOfficeId(int companySuperOfficeId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationContact contact = dbCtx.CrmReplicationContacts.FirstOrDefault(c => c.SuperOfficeId.Value == companySuperOfficeId);
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

        public int SyncCrmPersonIds(IEnumerable<PersonOverview> persons)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (PersonOverview person in persons)
                {
                    CrmReplicationPerson personDb = dbCtx.CrmReplicationPersons.FirstOrDefault(p => p.TeamLeaderId == person.LegacyId);
                    if (personDb != null && personDb.SuperOfficeId != person.Id)
                    {
                        personDb.SuperOfficeId = person.Id;
                        personDb.LastSyncedUtc = DateTime.UtcNow;
                        personDb.SyncVersion++;
                    }
                }
                return dbCtx.SaveChanges();
            }
        }

        public void EnrichSuperOfficePersonWithCompanyInfo(IEnumerable<Person> persons)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (Person person in persons)
                {
                    if (!person.ContactId.HasValue)
                        continue;

                    CrmReplicationContact company = dbCtx.CrmReplicationContacts.FirstOrDefault(a => a.SuperOfficeId == person.ContactId.Value);
                    if (company != null)
                    {
                        person.LinkedCompanyId = company.Id;
                        person.LinkedCompanyName = company.Name;
                    }
                }
            }
        }

        public void EnrichSuperOfficePersonWithCrmReplicationPersonId(IEnumerable<Person> persons)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                foreach (Person person in persons)
                {
                    person.CrmReplicationPersonId = null;
                    CrmReplicationPerson crmReplicationPerson = dbCtx.CrmReplicationPersons.FirstOrDefault(p => p.SuperOfficeId.Value == person.Id);
                    if (crmReplicationPerson != null)
                        person.CrmReplicationPersonId = crmReplicationPerson.Id;
                }
            }
        }

        public int SyncFullCrmPerson(Person person)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationPerson dbPerson = dbCtx.CrmReplicationPersons.FirstOrDefault(c => c.SuperOfficeId.Value == person.Id);
                if (dbPerson == null)
                {
                    int maxId = dbCtx.CrmReplicationPersons.Max(ea => ea.Id) + 1;

                    CrmReplicationPerson newPerson = new CrmReplicationPerson
                    {
                        Id = maxId,
                        ContactId = person.LinkedCompanyId,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Title = person.Title,
                        IsRetired = person.Retired,
                        SuperOfficeId = person.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationPersons.Add(newPerson);
                }
                else
                {
                    dbPerson.ContactId = person.LinkedCompanyId;
                    dbPerson.FirstName = person.FirstName;
                    dbPerson.LastName = person.LastName;
                    dbPerson.Title = person.Title;
                    dbPerson.IsRetired = person.Retired;
                    dbPerson.SuperOfficeId = person.Id;
                    dbPerson.LastSyncedUtc = DateTime.UtcNow;
                    dbPerson.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationPersonAndEmailBySuperOfficeId(int personSuperOfficeId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationPerson person = dbCtx.CrmReplicationPersons.FirstOrDefault(c => c.SuperOfficeId.Value == personSuperOfficeId);
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

        public int SyncFullCrmEmail(Person person)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationEmail email = dbCtx.CrmReplicationEmails.FirstOrDefault(e => e.ContactId == person.LinkedCompanyId && e.PersonId == person.CrmReplicationPersonId);
                if (email == null)
                {
                    int maxId = dbCtx.CrmReplicationEmails.Max(e => e.Id) + 1;

                    CrmReplicationEmail newEmail = new CrmReplicationEmail
                    {
                        Id = maxId,
                        PersonId = person.Id,
                        ContactId = person.LinkedCompanyId,
                        ContactName = person.LinkedCompanyName,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Email = person.Email,
                        DirectPhone = person.DirectPhone,
                        MobilePhone = person.MobilePhone,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationEmails.Add(newEmail);
                }
                else
                {
                    email.FirstName = person.FirstName;
                    email.LastName = person.LastName;
                    email.Email = person.Email;
                    email.DirectPhone = person.DirectPhone;
                    email.MobilePhone = person.MobilePhone;
                    email.LastSyncedUtc = DateTime.UtcNow;
                    email.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void EnrichSuperOfficeAppointment(Appointment appointment)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationProject project = dbCtx.CrmReplicationProjects.FirstOrDefault(p => p.SuperOfficeId == appointment.ProjectId);
                if (project != null)
                    appointment.ProjectCrmId = project.Id;

                if (!String.IsNullOrEmpty(appointment.Lokatie))
                {
                    Office office = dbCtx.Offices.FirstOrDefault(o => o.ShortName == appointment.Lokatie);
                    if (office != null)
                        appointment.OfficeId = office.Id;
                }

                if (appointment.AssociateId.HasValue)
                {
                    CrmReplicationAssociate associate = dbCtx.CrmReplicationAssociates.FirstOrDefault(a => a.SuperOfficeId == appointment.AssociateId);
                    if (associate != null)
                        appointment.AssociateCrmId = associate.Id;
                }

                CrmReplicationAppointmentTaskMapping taskMapping = dbCtx.CrmReplicationAppointmentTaskMappings.FirstOrDefault(tm => tm.Description == appointment.TaskValue);
                if (taskMapping != null)
                    appointment.TaskCrmId = taskMapping.TaskId;
            }
        }

        public int SyncFullCrmAppointment(Appointment appointment)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointment dbAppointment = dbCtx.CrmReplicationAppointments.FirstOrDefault(a => a.SuperOfficeId == appointment.Id);
                if (dbAppointment == null)
                {
                    int maxId = dbCtx.CrmReplicationAppointments.Max(a => a.Id) + 1;

                    CrmReplicationAppointment newAppointment = new CrmReplicationAppointment
                    {
                        Id = maxId,
                        AppointmentDate = appointment.AppointmentStartDateTime,
                        EndDate = appointment.AppointmentEndDateTime,
                        AssociateId = appointment.AssociateCrmId,
                        IsReserved = appointment.IsReserved,
                        OfficeId = appointment.OfficeId,
                        LanguageId = appointment.LanguageId,
                        Gender = appointment.Gender,
                        Code = appointment.Code,
                        FirstName = appointment.FirstName,
                        LastName = appointment.LastName,
                        CrmProjectId = appointment.ProjectCrmId,
                        TaskId = appointment.TaskCrmId,
                        Description = appointment.Description,
                        SuperOfficeId = appointment.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationAppointments.Add(newAppointment);
                }
                else
                {
                    dbAppointment.AppointmentDate = appointment.AppointmentStartDateTime;
                    dbAppointment.EndDate = appointment.AppointmentEndDateTime;
                    dbAppointment.AssociateId = appointment.AssociateCrmId;
                    dbAppointment.IsReserved = appointment.IsReserved;
                    dbAppointment.OfficeId = appointment.OfficeId;
                    dbAppointment.LanguageId = appointment.LanguageId;
                    dbAppointment.Gender = appointment.Gender;
                    dbAppointment.Code = appointment.Code;
                    dbAppointment.FirstName = appointment.FirstName;
                    dbAppointment.LastName = appointment.LastName;
                    dbAppointment.CrmProjectId = appointment.ProjectCrmId;
                    dbAppointment.TaskId = appointment.TaskCrmId;
                    dbAppointment.Description = appointment.Description;
                    dbAppointment.LastSyncedUtc = DateTime.UtcNow;
                    dbAppointment.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationAppointmentBySuperOfficeId(int superOfficeAppointmentId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointment appointment = dbCtx.CrmReplicationAppointments.FirstOrDefault(c => c.SuperOfficeId.Value == superOfficeAppointmentId);
                if (appointment != null)
                {
                    dbCtx.CrmReplicationAppointments.Attach(appointment);
                    dbCtx.CrmReplicationAppointments.Remove(appointment);
                    dbCtx.SaveChanges();
                }
            }
        }

        public int SyncFullCrmAppointmentTimeSheet(Appointment appointment)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointmentTimesheet appointmentTimeSheet = dbCtx.CrmReplicationAppointmentTimesheets.FirstOrDefault(a => a.SuperOfficeId == appointment.Id);
                if (appointmentTimeSheet == null)
                {
                    int maxId = dbCtx.CrmReplicationAppointmentTimesheets.Max(a => a.Id) + 1;

                    CrmReplicationAppointmentTimesheet newAppointmentTimeSheet = new CrmReplicationAppointmentTimesheet
                    {
                        Id = maxId,
                        ProjectId = appointment.ProjectCrmId,
                        AssociateId = appointment.AssociateCrmId,
                        StartDate = appointment.AppointmentStartDateTime,
                        EndDate = appointment.AppointmentEndDateTime,
                        Description = appointment.Description,
                        TaskDescription = appointment.ProjectName,
                        SuperOfficeId = appointment.Id,
                        LastSyncedUtc = DateTime.UtcNow,
                        SyncVersion = 1
                    };
                    dbCtx.CrmReplicationAppointmentTimesheets.Add(newAppointmentTimeSheet);
                }
                else
                {
                    appointmentTimeSheet.ProjectId = appointment.ProjectCrmId;
                    appointmentTimeSheet.AssociateId = appointment.AssociateCrmId;
                    appointmentTimeSheet.StartDate = appointment.AppointmentStartDateTime;
                    appointmentTimeSheet.EndDate = appointment.AppointmentEndDateTime;
                    appointmentTimeSheet.Description = appointment.Description;
                    appointmentTimeSheet.TaskDescription = appointment.ProjectName;

                    appointmentTimeSheet.LastSyncedUtc = DateTime.UtcNow;
                    appointmentTimeSheet.SyncVersion++;
                }
                return dbCtx.SaveChanges();
            }
        }

        public void DeleteCrmReplicationAppointmentTimeSheetBySuperOfficeId(int superOfficeTimeTrackingId)
        {
            using (var dbCtx = new QuintessenceEntities())
            {
                CrmReplicationAppointmentTimesheet appointmentTimeSheet = dbCtx.CrmReplicationAppointmentTimesheets.FirstOrDefault(c => c.SuperOfficeId.Value == superOfficeTimeTrackingId);
                if (appointmentTimeSheet != null)
                {
                    dbCtx.CrmReplicationAppointmentTimesheets.Attach(appointmentTimeSheet);
                    dbCtx.CrmReplicationAppointmentTimesheets.Remove(appointmentTimeSheet);
                    dbCtx.SaveChanges();
                }
            }
        }
    }
}
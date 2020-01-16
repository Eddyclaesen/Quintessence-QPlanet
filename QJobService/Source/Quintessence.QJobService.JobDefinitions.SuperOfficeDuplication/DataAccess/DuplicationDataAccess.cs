using System;
using System.Collections.Generic;
using System.Linq;

using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess
{
    public class DuplicationDataAccess : IDuplicationDataAccess
    {
        public string RetrieveDuplicationSettingsByKey(string key, string defaultValue)
        {
            try
            {
                using (var dbCtx = new SuperOfficeEntities())
                {
                    DuplicationSetting setting = dbCtx.DuplicationSettings.FirstOrDefault(s => s.Key == key);
                    return (setting != null && !String.IsNullOrEmpty(setting.Value)) ? setting.Value : defaultValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing SuperOffice database", ex);
            }
        }

        public IDictionary<string, string> RetrieveDuplicationSettingsByKeys(IEnumerable<string> keys)
        {
            try
            {
                using (var dbCtx = new SuperOfficeEntities())
                {
                    return dbCtx.DuplicationSettings.Where(s => keys.Contains(s.Key)).ToDictionary(s => s.Key, s => s.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing SuperOffice database", ex);
            }
        }

        public void RegisterDuplicationError(DateTime logDateUtc, string info)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.DuplicationErrorLogs.Max(c => (int?)c.Id) ?? 0;
                maxId++;
                DuplicationErrorLog newDuplicationErrorLog = new DuplicationErrorLog
                {
                    Id = maxId,
                    LogDateUtc = logDateUtc,
                    Info = info
                };
                dbCtx.DuplicationErrorLogs.Add(newDuplicationErrorLog);
                dbCtx.SaveChanges();
            }
        }

        public Guid RegisterDuplicationJobHistory(string jobName, DateTime startDate, DateTime? endDate, bool succeeded, string info)
        {
            DuplicationJobHistory duplicationJobHistory = new DuplicationJobHistory
            {
                Id = Guid.NewGuid(),
                JobName = jobName,
                StartDate = startDate,
                EndDate = endDate,
                Succeeded = succeeded
            };

            using (var dbCtx = new SuperOfficeEntities())
            {
                dbCtx.DuplicationJobHistories.Add(duplicationJobHistory);
                dbCtx.SaveChanges();
            }
            return duplicationJobHistory.Id;
        }

        public Guid RegisterDuplicationJobHistoryStart(string jobName)
        {
            return RegisterDuplicationJobHistory(jobName, DateTime.Now, null, false, null);

        }

        public void RegisterDuplicationJobHistoryEnd(Guid duplicationJobHistoryId, bool succeeded, string info)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                DuplicationJobHistory jobHistory = dbCtx.DuplicationJobHistories.FirstOrDefault(j => j.Id == duplicationJobHistoryId);
                if (jobHistory != null)
                {
                    jobHistory.EndDate = DateTime.Now;
                    jobHistory.Succeeded = succeeded;
                    jobHistory.Info = info;
                    dbCtx.SaveChanges();
                }
            }
        }

        public void TruncateDuplicationTables()
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                dbCtx.TruncateDuplicationTables();
            }
        }

        public int RegisterUsers(IEnumerable<SuperOfficeUser> users)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.Users.Max(c => (int?)c.Id) ?? 0;
                foreach (SuperOfficeUser u in users)
                {
                    maxId++;
                    User newUser = new User
                    {
                        Id = maxId,
                        UserId = u.UserId,
                        ContactId = u.ContactId,
                        PersonId = u.PersonId,                        
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Title = u.Title,
                        UserGroup = u.UserGroupParsed,
                        OtherGroups = u.OtherGroupsParsed,
                        IsActive = u.IsActive,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Users.Add(newUser);
                }
                return dbCtx.SaveChanges();
            } 
        }


        public int RegisterPersons(IEnumerable<SuperOfficePerson> persons)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.People.Max(c => (int?)c.Id) ?? 0;
                foreach (SuperOfficePerson c in persons)
                {
                    maxId++;
                    Person newPerson = new Person
                    {
                        Id = maxId,
                        PersonId = c.Id.Value,
                        ContactId = c.ContactId == 0 ? 4870 : c.ContactId.GetValueOrDefault(),
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        FullName = c.FullName,
                        MrMrs = c.MrMrs,
                        Title = c.Title,
                        EmailAddress = c.Email,
                        Phone = c.Phone,
                        Retired = c.Retired,
                        Inside = c.Inside,
                        Nps = c.Nps,
                        CommercialEmail = c.CommercialEmails,
                        
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.People.Add(newPerson);
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterContacts(IEnumerable<SuperOfficeContact> contacts)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.Contacts.Max(c => (int?)c.Id) ?? 0;
                foreach (SuperOfficeContact c in contacts)
                {
                    maxId++;
                    if (c.Id != 0)
                    {
                        Contact newContact = new Contact
                        {
                            Id = maxId,
                            ContactId = c.Id.GetValueOrDefault(),
                            Name = c.Name,
                            StreetAddressLine1 = c.StreetAddressLine1,
                            PostAddressLine1 = c.PostAddressLine1,
                            PostAddressZip = c.PostAddressZip,
                            PostAddressCity = c.PostAddressCity,
                            Country = c.Country,

                            ContactPhone = c.ContactPhone,
                            ContactFax = c.ContactFax,
                            UrlAddress = c.UrlAddress,
                            EmailAddress = c.EmailAddress,
                            ContactAssociateFullName = c.ContactAssociateFullName,
                            ContactAssociatePersonId = c.ContactAssociatePersonId,

                            Category = c.CategoryParsed,
                            Business = c.BusinessParsed,
                            OrgNr = c.OrgNr,
                            Number = c.Number,
                            Stop = c.Stop,
                            ContactNoMail = c.ContactNoMail,

                            Assistent = c.Assistent,
                            FidelisationNorm = c.FidelisatieNormParsed,
                            FocusList = c.FocusList,
                            UpdatedDate = c.UpdatedDate,
                            UpdatedBy = c.UpdatedBy,

                            LastSyncedUtc = DateTime.UtcNow
                        };
                        dbCtx.Contacts.Add(newContact);
                    }
                    
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterProjects(IEnumerable<SuperOfficeProject> projects)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.Projects.Max(c => (int?)c.Id) ?? 0;
                foreach (SuperOfficeProject p in projects)
                {
                    maxId++;
                    Project newProject = new Project
                    {
                        Id = maxId,
                        ProjectId = p.Id.Value,
                        Name = p.Name,
                        ProjectAssociateFullName = p.ProjectAssociateFullName,
                        ProjectAssociatePersonId = p.ProjectAssociatePersonId,
                        Type = p.TypeParsed,
                        Status = p.StatusParsed,
                        RegisteredDate = p.RegisteredDate,
                        EndDate = p.EndDate,
                        BookYear = p.BookYearParsed,
                        NextMileStone = p.NextMileStone,
                        Completed = p.Completed,
                        UpdatedDate = p.UpdatedDate,
                        UpdatedBy = p.UpdatedBy,

                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Projects.Add(newProject);
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterAppointments(IEnumerable<SuperOfficeAppointment> appointments)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.Appointments.Max(c => (int?)c.Id) ?? 0;
                foreach (SuperOfficeAppointment a in appointments)
                {
                    maxId++;
                    Appointment newAppointment = new Appointment
                    {
                        Id = maxId,
                        AppointmentId = a.Id.GetValueOrDefault(),
                        Type = a.Type,
                        Associate = a.Associate,
                        AssociateFullName = a.AssociateFullName,
                        AssociatePersonId = a.AssociatePersonId > 0 ? a.AssociatePersonId : null,
                        ContactId = a.ContactId,
                        PersonId = a.PersonId,
                        ProjectId = a.ProjectId,
                        SaleId = a.SaleId,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        DurationMinutes = a.DurationMinutes,
                        Text = a.Text,
                        Completed = a.Completed,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Gender = a.GenderParsed,
                        Language = a.LanguageParsed,
                        Email = a.Email,
                        Code = a.Code,
                        Reservation = a.Reservation,
                        Location = a.LocationParsed,

                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Appointments.Add(newAppointment);
                }
                return dbCtx.SaveChanges();
            }  
        }

        public int RegisterSales(IEnumerable<SuperOfficeSale> sales)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.Sales.Max(c => (int?)c.Id) ?? 0;
                foreach (SuperOfficeSale s in sales)
                {
                    maxId++;
                    Sale newSale = new Sale
                    {
                        Id = maxId,
                        SaleId = s.Id.Value,
                        ContactId = s.ContactId,
                        ProjectId = s.ProjectId,
                        Text  = s.Text,
                        PersonId = s.PersonId,
                        AssociateId = s.AssociateId,
                        AssociateFullName = s.AssociateFullName,
                        Type = s.Type,
                        Stage = s.Stage,
                        SaleStatus = s.SaleStatus,
                        SaleDate = s.SaleDate,
                        Source = s.SourceParsed,
                        Amount = s.Amount,
                        TotalCost = s.TotalCost,
                        Earning  = s.Earning,
                        SaleText = s.SaleText,
                        Completed = s.Completed,
                        UpdatedDate = s.UpdatedDate,
                        UpdatedBy = s.UpdatedBy,

                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Sales.Add(newSale);
                }
                return dbCtx.SaveChanges();
            }  

        }

        public int RegisterProjectMembers(IEnumerable<SuperOfficeProjectMember> projectMembers)
        {
            using (var dbCtx = new SuperOfficeEntities())
            {
                int maxId = dbCtx.ProjectMembers.Max(c => (int?) c.Id) ?? 0;
                foreach (SuperOfficeProjectMember pm in projectMembers)
                {
                    maxId++;
                    ProjectMember newProjectMember = new ProjectMember
                    {
                        Id = maxId,
                        ProjectMemberId = pm.Id.Value,
                        ProjectId = pm.ProjectId.GetValueOrDefault(),
                        PersonId = pm.PersonId.GetValueOrDefault(),
                        ProjectMemberTypeId = pm.ProjectMemberTypeId,
                        ProjectMemberTypeName = pm.ProjectMemberTypeName,

                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.ProjectMembers.Add(newProjectMember);
                }

                return dbCtx.SaveChanges();
            }
        }

        public IEnumerable<Appointment> RetrieveAppointmentIds(int fromAppointmentId, int batchSize)
        {
            try
            {
                using (var dbCtx = new SuperOfficeEntities())
                {
                    return dbCtx.Appointments.Where(a => a.AppointmentId > fromAppointmentId).OrderBy(a => a.AppointmentId).Take(batchSize).ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing SuperOffice database", ex);
            }
        }

        public int EnrichAppointments(IEnumerable<Appointment> appointments, IEnumerable<SuperOfficeAppointmentDetail> superOfficeAppointmentDetails)
        {
            int affected = 0;
            using (var dbCtx = new SuperOfficeEntities())
            {
                foreach (SuperOfficeAppointmentDetail superOfficeAppointmentDetail in superOfficeAppointmentDetails.Where(soad => soad.MotherId != 0))
                {
                    Appointment appointment = appointments.FirstOrDefault(a => a.AppointmentId == superOfficeAppointmentDetail.AppointmentId);
                    if (appointment != null)
                    {
                        dbCtx.Appointments.Attach(appointment);
                        appointment.MotherId = superOfficeAppointmentDetail.MotherId;
                    }
                }
                affected = dbCtx.SaveChanges();
            }

            return affected;
        }

        public IEnumerable<int> RetrieveProjectIds(int fromProjectId, int batchSize)
        {
            try
            {
                using (var dbCtx = new SuperOfficeEntities())
                {
                    return dbCtx.Projects.Where(a => a.ProjectId > fromProjectId).OrderBy(p => p.ProjectId).Select(p => p.ProjectId).Take(batchSize).ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing SuperOffice database", ex);
            }
        }

    }
}

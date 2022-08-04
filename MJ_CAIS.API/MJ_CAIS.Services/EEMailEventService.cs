using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    public class EEMailEventService : IEEMailEventService
    {
        private readonly CaisDbContext _dbContext;
        public EEMailEventService(CaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddEmailEvent(string to, string subject, string body)
        {
            EEmailEvent emailEvent = new EEmailEvent();
            emailEvent.Id = BaseEntity.GenerateNewId();
            emailEvent.EmailAddress = to;
            emailEvent.Subject = subject;
            emailEvent.Body = body;
            emailEvent.EmailStatus = EmailStatusConstants.Pending;
            _dbContext.Set<EEmailEvent>().Add(emailEvent);
            await _dbContext.SaveChangesAsync();
        }
    }
}

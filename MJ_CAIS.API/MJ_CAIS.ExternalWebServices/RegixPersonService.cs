using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.ExternalWebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoLogica.RegiX.GraoNBDAdapter;

namespace MJ_CAIS.ExternalWebServices
{
    public class RegixPersonService: IRegixPersonService
    {
        private CaisDbContext _dbContext;
        RegixPersonService(CaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string CreateRegixPersonResponse(string egn)
        {
            PersonDataRequestType r = new PersonDataRequestType();
            r.EGN = egn;
            return Common.XmlData.XmlUtils.SerializeToXml(r);

        }

        private void CallPersonDataRegix(string egn,string applicationID, string serviceURI)
        {
            WWebRequest req = new WWebRequest();
            req.Id = BaseEntity.GenerateNewId();
            req.RequestXml = CreateRegixPersonResponse(egn);
            req.ApplicationId = applicationID;
            req.RemoteAddress = serviceURI;
            //todo: add enum
            req.Status = "New";

            //може би save?

            //call webservice client CallRegixExecuteSynchronous

            //map objects, save in columns

            //return object?!
        }
    }
}

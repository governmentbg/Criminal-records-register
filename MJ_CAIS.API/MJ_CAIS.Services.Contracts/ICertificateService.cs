using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    //todo: add dto when they are ready
    public interface ICertificateService: IBaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string>
    {
    }
}

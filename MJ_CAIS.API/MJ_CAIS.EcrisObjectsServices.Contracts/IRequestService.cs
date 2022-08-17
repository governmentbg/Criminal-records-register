using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.EcrisObjectsServices.Contracts
{
    public interface IRequestService
    {
        Task RecreateResponseToRequest(string responseId);
        Task GenerateUnsuccessfulResponce(string requestId, string reasonCode, string joinSeparator = " ");
    }
}

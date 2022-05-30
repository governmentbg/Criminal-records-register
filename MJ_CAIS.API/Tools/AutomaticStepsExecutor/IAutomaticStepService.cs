using MJ_CAIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public interface IAutomaticStepService
    {
        public  Task PreSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config);      
        public  Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, Microsoft.Extensions.Configuration.IConfiguration config);
        public  Task PostSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config);
        public  Task PreProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config);
        public  Task<AutomaticStepResult> ProcessEntitiesAsync(List<IBaseIdEntity> entities, Microsoft.Extensions.Configuration.IConfiguration config);
        public  Task PostProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config);


    }
}

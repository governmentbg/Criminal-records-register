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
        public  Task PreSelectAsync();      
        public  Task<List<BaseEntity>> SelectEntitiesAsync();
        public  Task PostSelectAsync();
        public  Task PreProcessAsync();
        public  Task<AutomaticStepResult> ProcessEntitiesAsync(List<BaseEntity> entities);
        public  Task PostProcessAsync();


    }
}

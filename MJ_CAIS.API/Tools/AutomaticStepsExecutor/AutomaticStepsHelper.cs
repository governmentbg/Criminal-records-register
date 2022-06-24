using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public class AutomaticStepsHelper
    {
        
        public static string GetTextFromFile(string fileName)
        {
            string htmlCode = File.ReadAllText(fileName, Encoding.Default);
            return htmlCode;
        }

       


    }
}

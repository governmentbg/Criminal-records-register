using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNBFileProcessor
{
    public interface IEventProcessor
    {
        Task Process(string fullPath);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEEMailEventService
    {
        Task AddEmailEvent(string to, string subject, string body);
    }
}

﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.core.Models.Db;

namespace ygo.core.Services
{
    public interface ILimitService
    {
        Task<List<Limit>> AllLimits();
    }
}
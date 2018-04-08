﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.infrastructure.Models;

namespace ygo.domain.Repository
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> AllSubCategories();
    }
}
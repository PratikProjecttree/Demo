﻿using Demo.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAll();
        Task<CategoryModel> GetById(int id);
    }
}

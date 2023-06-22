﻿using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface ICategoryService : IBaseService<Category>
    {
        public Task<List<Category>> GetCategoryInfoAsync(Expression<Func<Category, bool>> filter = null);
    }
}

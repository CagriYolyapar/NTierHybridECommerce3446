﻿using Project.BLL.Managers.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.DAL.Repositories.Concretes;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Managers.Concretes
{
    public class ProductManager(IProductRepository repository) : BaseManager<Product>(repository),IProductManager
    {
        IProductRepository _repository = repository;
    }
}

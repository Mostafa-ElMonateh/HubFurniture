﻿using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications;
using HubFurniture.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Contracts.Contracts.repositories;

namespace HubFurniture.Repository
{
    public class GenericRepository <T>: IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), specifications);
        }
    }
}
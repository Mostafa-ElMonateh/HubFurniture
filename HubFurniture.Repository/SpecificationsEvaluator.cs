﻿using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Repository
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> startQuery,
            ISpecifications<TEntity> specifications)
        {
            var query = startQuery;

            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }

            query = specifications.Includes.Aggregate(query, (currentQuery, includeExpression)
                => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
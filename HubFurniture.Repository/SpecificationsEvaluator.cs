﻿using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications;
using Microsoft.EntityFrameworkCore;


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

            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }

            if (specifications.IsPaginationEnabled)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            query = specifications.Includes.Aggregate(query, (currentQuery, includeExpression)
                => currentQuery.Include(includeExpression));

            return query;
        }
    }
}

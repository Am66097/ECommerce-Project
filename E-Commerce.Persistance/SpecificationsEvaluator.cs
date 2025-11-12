using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistance
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = EntryPoint;
            if (specifications is not null)
            {

                if (specifications.Criteria is not null)
                {
                    Query = Query.Where(specifications.Criteria);
                } //for criteria 

                if (specifications.IncludeExpression is not null && specifications.IncludeExpression.Any())
                {
                    Query = specifications.IncludeExpression.Aggregate(Query, (CurrentQuery, IncludeExp) =>
                    CurrentQuery.Include(IncludeExp));
                }  //for includes

                if (specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                } // for order by

                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderByDescending(specifications.OrderByDescending);
                } // for OrderByDescending

                if(specifications.IsPaginated)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                } // for pagination
            }
            return Query;
        }

    }
}

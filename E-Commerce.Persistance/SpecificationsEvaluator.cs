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
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> EntryPoint ,
            ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = EntryPoint;
            if(specifications is not null)
            {
                if(specifications.IncludeExpression is not null && specifications.IncludeExpression.Any())
                {
                    Query = specifications.IncludeExpression.Aggregate(Query,(CurrentQuery,IncludeExp)=>
                    CurrentQuery.Include(IncludeExp));
                }
            }
            return Query;
        }

    }
}

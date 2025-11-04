using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {

        public ICollection<Expression<Func<TEntity , object>>> IncludeExpression { get; } // For Includes Expressions 

        public Expression<Func<TEntity,bool>> Criteria {  get; } // For Criteria 

        public Expression<Func<TEntity,object>> OrderBy { get; } // For OrderBy 
        public Expression<Func<TEntity,object>> OrderByDescending { get; } // For OrderByDescending 

        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; }

    }
}

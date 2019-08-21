using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Abp.Data;
using Abp.EntityFrameworkCore;
using LeesStore.Products;
using Microsoft.EntityFrameworkCore;

namespace LeesStore.EntityFrameworkCore.Repositories
{
    public class ProductRepository : LeesStoreRepositoryBase<Product, int>, IProductRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        public ProductRepository(IDbContextProvider<LeesStoreDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider) : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
        }

        public int GetIdealQuantity(int productId)
        {
            // 1. If it returns a set of Product you can do Context.Set<Product>().FromSql("SPROC @Param1", param1)
            // 2. To execute sprocs that don't return anything: Context.Database.ExecuteSqlCommandAsync("SPROC @Param1", param1)
            // 3. The ADO.Net Approach, for custom column or anything more complicated
            var dbCommand = this.Context.Database.GetDbConnection().CreateCommand();
            dbCommand.Transaction = GetActiveTransaction();
            dbCommand.CommandText = "GetIdealProductQuantity";
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) {Value = productId});
            using (var dbDataReader = dbCommand.ExecuteReader())
            {
                while (dbDataReader.Read())
                {
                    var idealQuantity = dbDataReader.GetInt32(0);
                    return idealQuantity;
                }
            }
            throw new ArgumentException("No ideal quantity exists for product: " + productId);
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                {"ContextType", typeof(LeesStoreDbContext) },
                {"MultiTenancySide", MultiTenancySide }
            });
        }
    }
}

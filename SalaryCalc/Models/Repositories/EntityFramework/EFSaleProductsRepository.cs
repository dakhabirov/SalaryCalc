using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFSaleProductsRepository : ISaleProductsRepository
    {
        private readonly AppDbContext context;

        public EFSaleProductsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<SaleProduct> GetSaleProducts(Guid saleId)
        {
            return context.Sales.Where(s => s.Id == saleId).FirstOrDefault().SaleProducts;
        }

        public void SaveSaleProducts(Guid saleId, Guid productId, int amount)
        {
            SaleProduct saleProduct = new SaleProduct
            {
                SaleId = saleId,
                ProductId = productId,
                Amount = amount
            };

            context.SaleProducts.Add(saleProduct);
            context.SaveChanges();
        }
    }
}

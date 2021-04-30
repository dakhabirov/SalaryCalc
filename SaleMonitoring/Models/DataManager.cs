using SalaryCalc.Models.Repositories.Interfaces;

namespace SalaryCalc.Models
{
    public class DataManager
    {
        public IUsersRepository Users { get; set; }

        public IPositionsRepository Positions { get; set; }

        public ISalariesRepository Salaries { get; set; }

        public IProductsRepository Products { get; set; }

        public ICategoriesRepository Categories { get; set; }

        public ISalesRepository Sales { get; set; }

        public ISaleProductsRepository SaleProducts { get; set; }

        public DataManager(IUsersRepository users, IPositionsRepository positions, ISalariesRepository salaries, IProductsRepository products, ICategoriesRepository categories, ISalesRepository sales, ISaleProductsRepository saleProducts)
        {
            Users = users;
            Positions = positions;
            Salaries = salaries;
            Products = products;
            Categories = categories;
            Sales = sales;
            SaleProducts = saleProducts;
        }
    }
}
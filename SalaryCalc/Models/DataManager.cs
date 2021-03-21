using SalaryCalc.Models.Repositories.Interfaces;

namespace SalaryCalc.Models
{
    public class DataManager
    {
        public IUsersRepository Users { get; set; }

        public ISalesRepository Sales { get; set; }

        public IProductsRepository Products { get; set; }

        public DataManager(IUsersRepository users, ISalesRepository sales, IProductsRepository products)
        {
            Users = users;
            Sales = sales;
            Products = products;
        }
    }
}

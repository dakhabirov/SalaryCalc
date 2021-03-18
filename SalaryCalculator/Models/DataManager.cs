using SalaryCalculator.Models.Repositories.Interfaces;

namespace SalaryCalculator.Models
{
    public class DataManager
    {
        public IUsersRepository Users { get; set; }

        public IProductsRepository Products { get; set; }

        public DataManager(IUsersRepository users, IProductsRepository products)
        {
            Users = users;
            Products = products;
        }
    }
}

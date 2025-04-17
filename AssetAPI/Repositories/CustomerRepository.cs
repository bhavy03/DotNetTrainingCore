using AssetAPI.Entity;
using AssetAPI.Enums;

namespace AssetAPI.Repositories
{
    public class CustomerRepository : IAssetRepository<Customer>
    {
        private readonly AssetContext _context;

        public CustomerRepository(AssetContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.Where(c => c.Role != Role.Admin).ToList();
        }

        public Customer? Search(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer Add(Customer customer)
        {
            var hashedPassword = PasswordHandler.HashPassword(customer.Password);
            customer.Password = hashedPassword;
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public bool Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return true;
        }

        public Customer? Update(int id, Customer customer)
        {
            var existing = _context.Customers.Find(id);
            if (existing == null) return null;

            if (existing.Role == Role.Admin) return existing;
            existing.Name = customer.Name;
            existing.Number = customer.Number;
            existing.Address = customer.Address;
            var hashedPassword = PasswordHandler.HashPassword(customer.Password);
            customer.Password = hashedPassword;
            if (existing.Role == Role.Customer)
            {
                existing.Role = Role.Customer;
            }

            _context.SaveChanges();
            return existing;
        }
    }
}


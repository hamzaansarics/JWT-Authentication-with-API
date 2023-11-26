using JWT_API.Model;

namespace JWT_API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly JWT_APIContext _context;
        public CustomerService(JWT_APIContext context)
        {
            _context = context;
        }
        public int PostData(customermodel customermodel)
        {
            _context.customers.Add(customermodel);
            int result = _context.SaveChanges();
            return result;
        }
        public List<customermodel> GetData()
        {
            var customerdata = new List<customermodel>();
            customerdata.AddRange(_context.customers);
            return customerdata;
        }
        public int CreateUser(UserModel user)
        {
            _context.users.Add(user);
            int result = _context.SaveChanges();
            return result;
        } //GetUser
        public List<UserModel> GetUser()
        {
            var userModels = new List<UserModel>();
            userModels.AddRange(_context.users);
            return userModels;
        }
    }
}

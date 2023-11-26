using JWT_API.Model;

namespace JWT_API.Services
{
    public interface ICustomerService
    {
        int PostData(customermodel customermodel);
        List<customermodel> GetData();
        int CreateUser(UserModel user);
        List<UserModel> GetUser();
    }
}
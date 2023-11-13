using BasicAuthExample.IRepositories;
using BasicAuthExample.WrapperModels;
using BasicAuthExample.WrapperModels.User;

namespace BasicAuthExample.DataRepositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<ApiWrapper> GetUserInfo()
        {

              UserResponseModel user = new UserResponseModel();
            user.Contact = "+923110215547";
            user.Email = "ahmeduzair12123@gmail.com";
            user.Name= "Ahmed Uzair";
            user.Id= 1; 

            return ApiWrapper.SetResponse(success: true, data: user, error: null);
        }
    }
}

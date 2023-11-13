using BasicAuthExample.WrapperModels;

namespace BasicAuthExample.IRepositories
{
    public interface IUserRepository
    {
        Task<ApiWrapper> GetUserInfo();
    }
}
 
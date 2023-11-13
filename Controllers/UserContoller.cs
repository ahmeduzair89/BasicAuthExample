using BasicAuthExample.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthExample.Controllers
{
    public class UserContoller : Controller
    {
        readonly IUserRepository userRepository;
        public UserContoller(IUserRepository userRepository)
        {
            this.userRepository = userRepository;   
        }


        [Authorize]
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
                var res = await userRepository.GetUserInfo();
                return  res.Success? Ok(res): BadRequest(res);
        }
    }
}

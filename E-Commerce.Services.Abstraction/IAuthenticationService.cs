using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Abstraction
{
    public interface IAuthenticationService
    {
        //Login
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);

        //Register
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    }
}

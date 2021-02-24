using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using sudokuBackEnd.APIModel;

namespace sudokuBackEnd.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> AuthenticateGoogle();
        Task<AuthenticateResponse> CreateUser(CreateUserRequest model);
        Task<AuthenticateResponse> CreateGoogleUser(CreateGoogleUserModel model);
        Task<IEnumerable<User>> GetLeaderBoard();
        Task<UserExistsResponse> UserExists(string name);
        Task ChangePassword(string newPassword, int loggedInUser);
    }
}

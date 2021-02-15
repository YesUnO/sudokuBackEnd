using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sudokuBackEnd.APIModel;
using sudokuBackEnd.DB.Data;
using sudokuBackEnd.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sudokuBackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly SudokuContext _context;
        private readonly IConfiguration _configuration;
        public UserService(SudokuContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _context.User.FirstOrDefaultAsync(x=>x.Name == model.Username && x.Password == model.Password);
            if (user==null)
            {
                return null;
            }
            var apiUser = DBToApiModel.ToApiModel(user);
            var token = generateJwtToken(user);
            return new AuthenticateResponse(apiUser,token);
        }

        public async Task<IEnumerable<User>> GetLeaderBoard()
        {
            var users =  _context.User.Where(x => x.ResolvedGamesCount > 0).OrderBy(x => x.Elo).ToList();
            return DBToApiModel.ToApiModel(users);
        }

        public Task<AuthenticateResponse> AuthenticateGoogle()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticateResponse> CreateGoogleUser(CreateGoogleUserRequest model)
        {
            var user = _context.User.FirstOrDefaultAsync(x => x.AuthId == model.Sub).Result;
            if (user == null)
            {
                user = _context.User.Add(new DB.Entity.User { AuthId = model.Sub, Name = model.Name }).Entity;
            }
            var apiUser = DBToApiModel.ToApiModel(user);
            var token = generateJwtToken(user);
            return new AuthenticateResponse(apiUser, token);
        }

        public async Task<AuthenticateResponse> CreateUser(CreateUserRequest model)
        {

            var user = _context.User.Add(new DB.Entity.User { Name = model.Username, Password = model.Password });
            await _context.SaveChangesAsync();
            var apiUser = DBToApiModel.ToApiModel(user.Entity);
            var token = generateJwtToken(user.Entity);
            return new AuthenticateResponse(apiUser, token);
        }

        public async Task<UserExistsResponse> UserExists(string name)
        {
            bool exists =  _context.User.Any(e => e.Name == name);
            return new UserExistsResponse { Exist = exists };
        }

        private string generateJwtToken(DB.Entity.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes($"{_configuration["Auth:Secret"]}");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

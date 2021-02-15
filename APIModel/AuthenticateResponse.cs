using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.APIModel
{
    public class AuthenticateResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Elo { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Elo = user.Elo;
            Username = user.Username;
            Token = token;
        }
    }
}

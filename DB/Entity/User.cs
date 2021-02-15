using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.DB.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string AuthId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int ResolvedGamesCount { get; set; }
        public float Elo { get; set; }
    }
}

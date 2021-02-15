using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sudokuBackEnd.APIModel
{
    public class User
    {
        public string Username { get; set; }
        public string Elo { get; set; }
        public int ResolvedGamesCount { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}

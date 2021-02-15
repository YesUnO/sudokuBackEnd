using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.DB.Entity
{
    public class UserGameSolution
    {
        public int Id { get; set; }
        public GameEntering ResolvedGameEntering { get; set; }
        public User User { get; set; }
        public float Time { get; set; }
    }
}

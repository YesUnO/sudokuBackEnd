using sudokuBackEnd.APIModel;
using sudokuBackEnd.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.Services.ServiceInterfaces
{
    public interface IGameEnteringService
    {
        public GameEntering SaveGameEntering(Game game,float time);
        public Task<List<GameEntering>> GetUsersUnsolvedGameEnterings(int id);
    }
}

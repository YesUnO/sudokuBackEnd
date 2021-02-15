using Microsoft.EntityFrameworkCore;
using sudokuBackEnd.APIModel;
using sudokuBackEnd.DB.Data;
using sudokuBackEnd.DB.Entity;
using sudokuBackEnd.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.Services
{
    public class GameEnteringService : IGameEnteringService
    {
        private SudokuContext _sudokuContext;
        public GameEnteringService(SudokuContext sudokuContext)
        {
            _sudokuContext = sudokuContext;
        }

        public async Task<List<GameEntering>> GetUsersUnsolvedGameEnterings(int id)
        {
            
            return await _sudokuContext.UserGameSolution.Where(x => x.User.Id != id).Select(x=>x.ResolvedGameEntering).Take(5).Distinct().ToListAsync();
            
        }

        public GameEntering SaveGameEntering(Game gameRecieved,float time)
        {
            GameEntering game;
            if (gameRecieved.Id ==0)
            {
                game = new GameEntering
                {
                    Id = gameRecieved.Id,
                    Name = gameRecieved.Name,
                    Entering = gameRecieved.Entering,
                    Solution = gameRecieved.Solution,
                    DifficulitySettings = gameRecieved.Difficulity,
                    AvarageTime = time,
                    NumberOfSuccessfullSolutions = 1
                };

                _sudokuContext.GameEntering.Add(game);

            }
            else
            {
                game = _sudokuContext.GameEntering.Find(gameRecieved.Id);
                game.AvarageTime = (game.AvarageTime * game.NumberOfSuccessfullSolutions + time) / (game.NumberOfSuccessfullSolutions + 1);
                game.NumberOfSuccessfullSolutions += 1;

                _sudokuContext.GameEntering.Update(game);
            }
            _sudokuContext.SaveChanges();
            return game;
        }
    }
}

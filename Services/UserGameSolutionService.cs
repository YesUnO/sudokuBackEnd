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
    public class UserGameSolutionService : IUserGameSolutionService
    {
        private readonly IGameEnteringService _gameEnteringService;
        private readonly SudokuContext _sudokuContext;
        public UserGameSolutionService(SudokuContext sudokuContext, IGameEnteringService gameEnteringService)
        {
            _sudokuContext = sudokuContext;
            _gameEnteringService = gameEnteringService;
        }

        public void CountUsersElo()
        {
            var allSolutions = _sudokuContext.UserGameSolution
                .Select(x => new { x.ResolvedGameEntering, x.User, x.Time })
                .OrderBy(x => x.Time)
                .ToList();
            var allSolutionsGrouped = allSolutions.GroupBy(x => x.ResolvedGameEntering);

            var partialResults = new List<Tuple<DB.Entity.User, float,float>>();
            foreach (var group in allSolutionsGrouped)
            {
                var i = 1;
                foreach (var item in group)
                {
                    float points = i / (float)group.Count();
                    if (partialResults.Any(m=>m.Item1 == item.User))
                    {
                        var index = partialResults.FindIndex(x => x.Item1 == item.User);
                        var usersPartialScore = partialResults.Find(x => x.Item1 == item.User);
                        partialResults[index] = Tuple.Create(item.User, usersPartialScore.Item2 + points, usersPartialScore.Item3+1);
                    }
                    else
                    {
                        partialResults.Add(new Tuple<DB.Entity.User, float, float>(item.User, points, 1));
                    }
                }
            }
            foreach (var partialResult in partialResults)
            {
                partialResult.Item1.Elo = (partialResult.Item2 / partialResult.Item3)*100;
                _sudokuContext.SaveChanges();
            }
            
        }

        public void SaveUserGameSolution(SaveUserGameSolutionRequest model, int id)
        {
            var game = _gameEnteringService.SaveGameEntering(model.Game,model.ElapsedTime);

            var user = _sudokuContext.User.Find(id);
            user.ResolvedGamesCount += 1;
            var userGameSolution = new UserGameSolution
            {
                ResolvedGameEntering = game,
                User = user,
                Time = model.ElapsedTime
            };


            _sudokuContext.UserGameSolution.Add(userGameSolution);
            _sudokuContext.SaveChanges();
        }
    }
}

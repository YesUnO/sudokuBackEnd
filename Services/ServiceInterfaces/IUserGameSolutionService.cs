using sudokuBackEnd.APIModel;
using sudokuBackEnd.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.Services.ServiceInterfaces
{
    public interface IUserGameSolutionService
    {
        void SaveUserGameSolution(SaveUserGameSolutionRequest model, int id);
        void CountUsersElo();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sudokuBackEnd.APICore;
using sudokuBackEnd.DB.Data;
using sudokuBackEnd.DB.Entity;
using sudokuBackEnd.Services.ServiceInterfaces;

namespace sudokuBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameEnteringsController : RestApiController
    {
        private readonly IGameEnteringService _gameEnteringService;

        public GameEnteringsController(IGameEnteringService gameEnteringService)
        {
            _gameEnteringService = gameEnteringService;
        }

        // GET: api/GameEnterings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameEntering>>> GetGameEntering()
        {
            return await _gameEnteringService.GetUsersUnsolvedGameEnterings(LoggedInUser);
        }

        
    }
}

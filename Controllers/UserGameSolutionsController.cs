using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sudokuBackEnd.APICore;
using sudokuBackEnd.APIModel;
using sudokuBackEnd.DB.Data;
using sudokuBackEnd.DB.Entity;
using sudokuBackEnd.Services.ServiceInterfaces;

namespace sudokuBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameSolutionsController : RestApiController
    {
        private readonly IUserGameSolutionService _userGameSolutionService;

        public UserGameSolutionsController(IUserGameSolutionService userGameSolutionService)
        {
            _userGameSolutionService = userGameSolutionService;
        }

        // GET: api/UserGameSolutions
        [HttpGet]
        public async Task<ActionResult> GetUserGameSolution()
        {
            _userGameSolutionService.CountUsersElo();
            return Ok();
        }

        //// GET: api/UserGameSolutions/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserGameSolution>> GetUserGameSolution(int id)
        //{
        //    var userGameSolution = await _context.UserGameSolution.FindAsync(id);

        //    if (userGameSolution == null)
        //    {
        //        return NotFound();
        //    }

        //    return userGameSolution;
        //}

        //// PUT: api/UserGameSolutions/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUserGameSolution(int id, UserGameSolution userGameSolution)
        //{
        //    if (id != userGameSolution.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(userGameSolution).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserGameSolutionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/UserGameSolutions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostUserGameSolution(SaveUserGameSolutionRequest model)
        {
            _userGameSolutionService.SaveUserGameSolution(model, LoggedInUser);
            return Ok(); 
        }

        //// DELETE: api/UserGameSolutions/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<UserGameSolution>> DeleteUserGameSolution(int id)
        //{
        //    var userGameSolution = await _context.UserGameSolution.FindAsync(id);
        //    if (userGameSolution == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UserGameSolution.Remove(userGameSolution);
        //    await _context.SaveChangesAsync();

        //    return userGameSolution;
        //}

        //private bool UserGameSolutionExists(int id)
        //{
        //    return _context.UserGameSolution.Any(e => e.Id == id);
        //}
    }
}

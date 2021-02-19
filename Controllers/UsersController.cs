using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sudokuBackEnd.APIModel;
using sudokuBackEnd.DB.Data;
using sudokuBackEnd.DB.Entity;
using sudokuBackEnd.Services.ServiceInterfaces;

namespace sudokuBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SudokuContext _context;
        private IUserService _userService;

        public UsersController(SudokuContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        // GET: api/Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUser()
        //{
        //    return await _context.User.ToListAsync();
        //}

        // GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.User.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        
        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> PostUser(CreateUserRequest model)
        {
            var authenticateResponse = await _userService.CreateUser(model);

            return authenticateResponse;
        }

        [Route("CreateGoogle")]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> GetGoogleUser(CreateGoogleUserRequest requestModel)
        {
            //var recievedToken = Request.Headers["Authorization"].ToString().Split(" ");
            var googleToken = GoogleJsonWebSignature.ValidateAsync(requestModel.Token, new GoogleJsonWebSignature.ValidationSettings()).Result;

            var model = new CreateGoogleUserModel { Name = googleToken.Name, Sub = googleToken.Subject };
            var authenticateResponse = await _userService.CreateGoogleUser(model);

            return authenticateResponse;
        }

        [Route("Authenticate")]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var authenticateResponse = await _userService.Authenticate(model);
            if (authenticateResponse==null)
            {
                return Unauthorized();
            }
            return authenticateResponse;
        }

        [Route("LeaderBoard")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIModel.User>>> GetLeaderBoard()
        {
            var leaderboard = await _userService.GetLeaderBoard();
            return Ok(leaderboard);
        }

        
        [Route("GoogleAuth")]
        [HttpGet]
        public async Task<ActionResult<AuthenticateResponse>> AuthenticateGoogle() 
        {
            var authenticateResponse = await _userService.AuthenticateGoogle();
            return authenticateResponse;
        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> DeleteUser(int id)
        //{
        //    var user = await _context.User.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.User.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return user;
        //}

        [HttpGet]
        public async Task<ActionResult<UserExistsResponse>> Exists(string name)
        {
            return await _userService.UserExists(name);
        }
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}

using sudokuBackEnd.DB.Data;
using sudokuBackEnd.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.APICore
{
    public class LoggedInUserHelper
    {
        private readonly SudokuContext _sudokuContext;
        
        public LoggedInUserHelper(SudokuContext sudokuContext)
        {
            _sudokuContext = sudokuContext;
        }

        public User FetchUser(int id)
        {
            return _sudokuContext.User.Find(id);
        }
    }
}

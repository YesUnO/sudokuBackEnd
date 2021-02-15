using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.APIModel
{
    public class SaveUserGameSolutionRequest
    {
        public Game Game { get; set; }
        public float ElapsedTime { get; set; }
    }
}

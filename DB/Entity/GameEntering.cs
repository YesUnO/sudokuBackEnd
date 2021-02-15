using sudokuBackEnd.APIModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.DB.Entity
{
    public class GameEntering
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Entering { get; set; }
        public string Solution { get; set; }
        public DifficulitySettingsEnum DifficulitySettings { get; set; }
        public float AvarageTime { get; set; }
        public int NumberOfSuccessfullSolutions { get; set; }
    }
}

using sudokuBackEnd.APIModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.APIModel
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Entering { get; set; }
        public string Solution { get; set; }
        public DifficulitySettingsEnum Difficulity { get; set; }
    }
}

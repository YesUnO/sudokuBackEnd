using sudokuBackEnd.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.APIModel
{
    public static class DBToApiModel
    {
        public static User ToApiModel(DB.Entity.User user)
        {
            return new User()
            {
                Username = user.Name,
                Elo = user.Elo.ToString(),
                ResolvedGamesCount = user.ResolvedGamesCount
            };
        }

        public static IEnumerable<User> ToApiModel(List<DB.Entity.User> users) {

            var usersApiModel = new List<User>();

            foreach (var user in users)
            {
                var userApiModel = new User
                {
                    Username = user.Name,
                    Elo = user.Elo.ToString(),
                    ResolvedGamesCount = user.ResolvedGamesCount
                };
                usersApiModel.Add(userApiModel);
            }
            return usersApiModel;
        }
    }
}

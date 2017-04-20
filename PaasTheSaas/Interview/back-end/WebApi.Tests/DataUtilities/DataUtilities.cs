
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Tests.DataUtilities
{
    public static class UserData
    {
        public static string KnownUserId
        {
            get
            {
                return "629fbcda-3289-4c12-bd6d-1481196ad8dc";
            }
        }

        public static string UnknownUserId
        {
            get
            {
                return "6254bcda-3289-4c12-bd6d-1481196ad8dc";
            }
        }

        public static User GetUnknownUser()
        {
            return new User()
            {
                Id = UnknownUserId,
                Name = "Juan Jaspe"
            };
        }

        public static User GetKnownUser()
        {
            return GetUsers().FirstOrDefault(user => user.Id == KnownUserId);
        }

        public static IEnumerable<User> GetKnownUserAsList()
        {
            return new List<User>() { GetUsers().FirstOrDefault(user => user.Id == KnownUserId) };
        }

        public static IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = KnownUserId,
                    Name = "Brian Sobus"
                },
                new User()
                {
                    Id = "6254bcda-3289-4c12-bd6d-1481196ad8ab",
                    Name = "Juan Jaspe"
                }
            };
        }
    }
}

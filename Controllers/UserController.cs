using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo_NET_CORE_API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private static string[] Descriptions = new[]
        {
            "Not working", "A funny guy", "That guy", "Someone new", "Manager", "Not a funny guy", "Harpers brothers", "Zulu", "Tester", "Tester2"
        };

        [HttpGet("[action]")]
        public IEnumerable<Users> GetUsers()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Users
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                // Generate a random ID
                Id = rng.Next(0, 1000),
                // Get a random description by using Descriptions.Length
                Description = Descriptions[rng.Next(Descriptions.Length)]
            });
        }

        public class Users
        {
            public string DateFormatted { get; set; }
            public int Id { get; set; }
            public string Description { get; set; }

            public int HashedId
            {
                get
                {
                    return 874098 + Id;
                }
            }
        }
    }
}

using System.Collections.Generic;

namespace Log4Grid.Management.Web.Models
{
    public class HomeUsers
    {
        public HomeUsers()
        {
            Users = new List<Log4Grid.Models.User>();
        }

        public IList<Log4Grid.Models.User> Users { get; set; }
    }
}
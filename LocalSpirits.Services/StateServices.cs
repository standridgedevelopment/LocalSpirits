using LocalSpirits.Data;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class StateServices
    {
        public State GetStateByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                State state = ctx.States.Single(e => e.Name.Contains(name));
                return state;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Binariks_task.Models
{
    public class LeagueReiting
    {
        public string Name { get; set; }
        public ICollection<Team> Teams { get; set; }

        public LeagueReiting()
        {
            Teams = new List<Team>();
        }
    }
}

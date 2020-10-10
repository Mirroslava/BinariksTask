using System;
using System.Collections.Generic;

namespace Binariks_task.Models
{
    public class LigaReiting
    {
        public string Name { get; set; }
        public ICollection<Team> Teams { get; set; }

        public LigaReiting()
        {
            Teams = new List<Team>();
        }
    }
}

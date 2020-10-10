using System;
using System.Collections.Generic;

namespace Binariks_task.Models
{
    public class League
    {
        public string name { get; set; }
        public ICollection<Matches> matches { get; set; }
        public League()
        {
            matches = new List<Matches>();
        }
    }
}

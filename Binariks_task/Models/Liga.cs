﻿using System;
using System.Collections.Generic;

namespace Binariks_task.Models
{
    public class Liga
    {
        public string name { get; set; }
        public ICollection<Matches> matches { get; set; }
        public Liga()
        {
            matches = new List<Matches>();
        }
    }
}

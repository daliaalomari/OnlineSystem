using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystemStore.Domain.Tables
{
    public class ManyToMany
    {
        public int ProductRef { get; set; }
        public int CategoryRef { get; set; }
    }
}

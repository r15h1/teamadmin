using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core
{
    public class Club 
    {
        public Club()
        {
            Address = new Address();
        }

        public int? Id { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
    }
}

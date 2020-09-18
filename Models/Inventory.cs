using System;
using System.Collections.Generic;

namespace capestone_CreateAPI.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string YearMade { get; set; }
        public string Color { get; set; }
    }
}

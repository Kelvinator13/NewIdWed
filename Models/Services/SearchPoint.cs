using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Models.Services
{
    public class SearchPoint
    {
        public SearchPoint()
        {

        }
        [Key]
        public int JunctionPrimaryKey { get; set; }

        public string RestaurantModelPrimaryKey { get; set; }

        public int ApiPrimaryKey { get; set; }
    }
}

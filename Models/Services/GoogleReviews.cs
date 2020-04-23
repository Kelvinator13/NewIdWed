using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Models.Services
{
    public class GoogleReviews
    {
        [Key]
        public int ReviewsPrimaryKey { get; set; }
        public string RestaurantGuid { get; set; }
        public string Author_name { get; set; }
        public string Author_url { get; set; }
        public string Language { get; set; }
        public string Profile_photo_url { get; set; }
        public int Rating { get; set; }
        public string Relative_time_description { get; set; }
        public string Text { get; set; }
        public int Time { get; set; }
    }
}

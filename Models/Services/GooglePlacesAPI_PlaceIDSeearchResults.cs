using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Models.Services
{
    public class GooglePlacesAPI_PlaceIDSeearchResults
    {
        public object[] html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
    }

    public class Results
    {
        public string formatted_phone_number { get; set; }
        public string name { get; set; }
        public int price_level { get; set; }
        public float rating { get; set; }
        public Review[] reviews { get; set; }
        public string vicinity { get; set; }
        public string website { get; set; }
    }


    public class Review
    {
        public string author_name { get; set; }
        public string author_url { get; set; }
        public string language { get; set; }
        public string profile_photo_url { get; set; }
        public int rating { get; set; }
        public string relative_time_description { get; set; }
        public string text { get; set; }
        public int time { get; set; }
    }
}

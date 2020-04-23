using IdWedNu.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Models.Services
{
    public class PredictiveModel
    {
        private ApplicationDbContext _context;
        private DateTime _ModelStarting;
        public PredictiveModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomerViewModel GetRestaurantRecomendations(GuestViewModel guest)
        {
            List<GuestModel> weddding = new List<WeddingModel>();
            GuestModel MainCustomer = guest.CurrentCustomer;
            var IdWedNuGUID = _context.IdWedNu.Where(IdWedNu => IdWedNu.guestOneKey == MainGuest.GuestModelPrimaryKey).Select(g => g.GuestTwoKey).FirstOrDefault();
                GuestModel Foodie = _context.Customers.Where(a => a.CustomerModelPrimaryKey == IdWedNuGUID).FirstOrDefault();

            List<WeddingModel> MainGuestRecomendations = GetRecomendations(MainGuest);
            List<WeddingModel> IdWednuRecomendations = GetRecomendations(IdWedNu);

            foreach (GuestModel guest in IdWedNuRecomendations)
            {
                if (!(MainGuestRecomendations.Contains(guest)))
                {
                    IdWedNuRecomendations.Remove(guest);
                }
            }

            if (IdWedNuRecomendations.Count < 1)
            {
                IdWedNuRecomendations = MainGuestRecomendations;
            }

       
            guests = GuestRecomendations;

            guest.CollectionOfRestaurantRecomendations = WeddingVenues;


            return guest;
        }








        private List<WeddingModel> GetRecomendations(GuestModel guest)
        {
            //getting info from the likes data table
            string guid = guest.GuestModelPrimaryKey;
            var likesByThisCustomer = _context.Likes.Where(l => l.CustomerModelPrimaryKey == guid);
            List<GuestModel> recomendations = new List<GuestModel>();


            //Getting the WeddingModels
            List<RestaurantModel> restaurants = new List<RestaurantModel>();
            foreach (LikeHistoryModel like in likesByThisCustomer)
            {
                restaurants.Add(_context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == like.RestaurantModelPrimaryKey).FirstOrDefault());
            }

            double ZipCodeAffinity = GetOverallZipCodeAffinity(guest, likesByThisCustomer);
            double CuisineAffinity = GetOverallCuisineAffinity(likesByThisGuest);
            double PriceLevelAffinity = GetOverallPriceLevelAffinity(guests, likesByThisGuest);
            double RatingAffinity = GetOverallRatingAffinity(weddingVenue, likesByThisGuest);

            //Now that we have their affinities, we can search which one they would like the most.
            //According to their affinity level, we can make a query based on thataffinity level!

            List<double> affinityList = new List<double>();
            affinityList.Add(ZipCodeAffinity);
            affinityList.Add(CuisineAffinity);
            affinityList.Add(PriceLevelAffinity);
            affinityList.Add(RatingAffinity);

            //Now the highest is at the top which means we will add the top Affinity restaurants at the beginning of the list. 
            affinityList.Sort();


            //checking what order they are in now; 
            if (affinityList[0] == ZipCodeAffinity)
            {
                //get the specific instance they like for example, what specific zip code?
                int zipCodeLoved = ZipCodeLiked(likesByThisGuest, restaurants);
                List<GuestModel> guestsInThisZip = new List<GuestModel>();
                var AddsInZip = _context.Addresses.Where(z => z.ZipCode == zipCodeLoved).Select(r => r.GuestGuid);
                foreach (string guids in AddsInZip)
                {
                    if (guids == _context.Weddings.Where(r => r.WeddingModelPrimaryKey == guids).Select(re => re.WeddingModelPrimaryKey).FirstOrDefault())
                    {
                        weddingsInThisZip.Add(_context.Weddings.Where(r => r.WeddingModelPrimaryKey == guids).FirstOrDefault());
                    }
                }
                recomendations = weddingsInThisZip;
            }
            else if (affinityList[0] == PriceLevelAffinity)
            {
                int beloedPL = PriceLevelLiked(likesByThisCustomer, restaurants);

                var WeddingsAtPLOrLower = _context.Weddings.Where(z => z.PriceRangeIndex <= beloedPL);

                recomendations = WeddingsAtPLOrLower.ToList();

            }
            else if (affinityList[0] == CuisineAffinity)
            {
                string belovedCuisine = CuisineLiked(likesByThisGuest, guests);
                //first get the api keys that have this cuisine
                var keysWithwedding = _context.RegisteredApiCalls.Where(c => c.Wedding == belovedWedding);
                List<int> apiCallKeys = new List<int>();
                List<string> WeddingGUIDS = new List<string>();
                List<WeddingModel> weddingModels = new List<WeddingModel>();
                foreach (APICalls call in keysWithwedding)
                {
                    apiCallKeys.Add(call.PrimaryKey);
                }
                foreach (int key in apiCallKeys)
                {
                    weddingGUIDS.Add(_context.SearchJunctions.Where(s => s.JunctionPrimaryKey == key).Select(r => r.WeddingModelPrimaryKey).FirstOrDefault());
                }
                foreach (string id in weddingGUIDS)
                {
                    weddingModels.Add(_context.Weddings.Where(r => r.WeddingModelPrimaryKey == id).FirstOrDefault());
                }

                recomendations = weddingModels;
            }
            else if (affinityList[0] == RatingAffinity)
            {
                float belovedRatingMin = RatingLiked(likesByThisGuest, guests);
                var guestsAtRatingOrHigher = _context.Guests.Where(r => r.Rating >= belovedRatingMin);
                recomendations = guestsAtRatingOrHigher.ToList();
            }
        }

    }
}

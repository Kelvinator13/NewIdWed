using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Contracts
{
    interface IPlaceResultsRequest
    {
        Task<GooglePlacesAPI_PlaceIDSearchResult> GetPlaceIDResults(string APIKEY,string PLACE_ID);
    }
}

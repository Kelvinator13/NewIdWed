using IdWedNu.Contracts;
using IdWedNu.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Facebook;
using RestSharp;
using System.Net;

namespace IdWedNu.Models.Services
{
    public class FacebookDataRequest : IFacebookDataRequest
    {
        private string _FacebookAppSecret = Api_Keys.FacebookAppSecret;
        private string _FacebokAppId = Api_Keys.FacebookAppID;
        private dynamic _token;
        private ApplicationDbContext _context;
        private IFacebookDataRequest _FacebookDataRequest;
        private readonly object GuestGUID;

        public object GuestData { get; private set; }
        public static object Api_Keys { get; private set; }
        public string FacebookAppSecret { get => _FacebookAppSecret; set => _FacebookAppSecret = value; }
        public string FacebookAppSecret1 { get => _FacebookAppSecret; set => _FacebookAppSecret = value; }

        public FacebookDataRequest(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<FacebookData> GetFacebookData(string FacebookUserToken)
        {


            string _RequestUrl = $"https://graph.facebook.com/me?fields=id,name,age_range,gender,payment_pricepoints,likes.summary(true)&access_token=";
            HttpClient client = new HttpClient();
            System.Net.Http.HttpResponseMessage response = await client.GetAsync(_RequestUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                FacebookData data = JsonConvert.DeserializeObject<FacebookData>(json);
                return data;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Will return T/F if a users Facebook Data has been pulled.
        /// </summary>
        /// <param name="GuestGUID"></param>
        /// <returns></returns>
        public bool CheckTableInformation(string CustomerGUID)
        {
            //this is where we check
            var CustomerData = _context.GuestFacebookLink.Where(a => a.GuestGUID == GuestGUID);

            if (GuestData != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<FacebookData> RetrieveGuestFacebookData(string GuestGUID)
        {
            //this method will only be called when we know that there is a profile.
            var userToken = _context.GuestFacebookLink.Where(a => a.GUID == GuestGUID).Select(b => b.UserAccessToken).FirstOrDefault();
            FacebookData dataForSpecifiedUser = await GetFacebookData(userToken);

            return dataForSpecifiedUser;
        }




        /* THE POSTMAN METHOD */
        public void postman()
        {
            var client = new RestClient("https://www.facebook.com/v6.0/dialog/oauth?client_id=2844860895551149&redirect_uri=https://localhost:44355&state=test123&response_type=token");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "sb=e4WOXkUkul4op-qjZkt-itfM; fr=1piSrlzsGHyJ6Wubn..BejoV7.ya.AAA.0.0.BekJx2.AWU7XW7n");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }







        //The Model for the response
        public class AccessUser
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
        }

        public void CheckAuthorization()
        {
            string app_Id = Api_Keys.FacebookAppID.ToString();
            string app_secret = Api_Keys.FacebookAppSecret.ToString();
            string sample = "https://www.facebook.com/v6.0/dialog/oauth?client_id=2844860895551149&redirect_uri=https://localhost:44355&state=test123&response_type=token";

            FacebookClient facebook = new FacebookClient();
            dynamic userToken = facebook.Get("/oauth",
                new
                {
                    client_id = app_Id,
                    redirect_uri = "https://localhost:44355",
                    state = "test123",
                    response_type = "token"
                });

            //get the user token.




            dynamic user = facebook.Get("/me",
                new
                {
                    fields = "id, name",
                    access_token = userToken
                });
        }
    }
}

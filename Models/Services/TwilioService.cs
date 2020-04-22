using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace IdWedNu.Models.Services
{
    public class TwilioService
    {
        private ApplicationDbContext _context;
        public TwilioService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void sendMessage(string toPhoneNumber, WeddingModel matchingWeddingVenue)
        {
            string theAddress = getWeddingAddress(matchingWeddingVenue);
            string theBody = $"You have been matched\nWeddingVenue details\n{matchingWeddingVenue.WeddingVenuName}\n{theAddress}\n{matchingWeddingVenue.WeddingVenuePhone}";
            TwilioClient.Init(Api_Keys.TwilioSID, Api_Keys.TwilioAuthToken);
            var message = MessageResource.Create(
            body: theBody,
            from: new Twilio.Types.PhoneNumber(Api_Keys.TwilioFromNumber),
            to: new Twilio.Types.PhoneNumber(Api_Keys.TwilioToNumber)
                );
        }
        public void notifyUsers(List<GuestModel> guests, WeddingVenueModel matchingWeddingVenue)
        {
            foreach (GuestModel guest in guests)
            {
                sendMessage(guest.PhoneNumber.ToString(), matchingWeddingVenue);
            }
        }
        private string getWeddingVenueAddress(WeddingModel wedding)
        {
            string address = "";
            var streetName = _context.Addresses.Where(a => a.AddressKey == wedding.AddressKey)
                .Select(b => b.StreetName)
                .FirstOrDefault();
            var buildingNumber = _context.Addresses.Where(a => a.AddressKey == wedding.AddressKey).
                Select(b => b.BuildingNumber)
                .FirstOrDefault();
            var city = _context.Addresses.Where(a => a.AddressKey == wedding.AddressKey).
                Select(b => b.City)
                .FirstOrDefault();
            var state = _context.Addresses.Where(a => a.AddressKey == wedding.AddressKey).
                Select(b => b.StateCode)
                .FirstOrDefault();
            address += buildingNumber;
            address += streetName;
            address += city;
            address += state;
            return address;
        }
    }
}

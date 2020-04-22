using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Models.Services
{
    public class FacebookAuthorizationResponse
    {
        public bool IsAuthorized { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool Name { get; set; }
    }
}

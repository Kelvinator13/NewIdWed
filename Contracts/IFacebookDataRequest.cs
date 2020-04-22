using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Contracts
{
    public class IFacebookDataRequest
    {
        public abstract Task<FacebookData> GetfacebookData(string FacebookToken);

    }
}

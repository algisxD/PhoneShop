using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.Services
{
    public class SessionService
    {
        Dictionary<string, dynamic> Items { get; set; } = new Dictionary<string, dynamic>();

        public dynamic this[string Index]
        {
            get
            {
                return Items.ContainsKey(Index) ? Items[Index] : null;
            }
            set
            {
                Items[Index] = value;
            }
        }
    }
}

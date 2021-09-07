using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.Services
{
    public class WindowStateService
    {
        private int countedComponents = 0;
        public int OpenNewComponent()
        {
            return countedComponents++;
        }

        public int CloseComponent()
        {
            return countedComponents--;
        }
    }
}

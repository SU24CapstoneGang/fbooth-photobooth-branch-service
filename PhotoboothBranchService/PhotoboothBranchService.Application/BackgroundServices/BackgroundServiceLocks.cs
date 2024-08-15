using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.BackgroundServices
{
    public static class BackgroundServiceLocks
    {
        public static SemaphoreSlim ServiceLock = new SemaphoreSlim(1, 1); // Allows only one thread to enter
    }
}

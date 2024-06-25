using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Enum
{
    public enum PhotoSessionStatus
    {
        Waiting = 1, //session just created, need customer enter code
        Ongoing = 2, // customer entered code, in take photo session  
        Canceled = 3, //for some issue, this photo is delete and not count on Session Order
        Ended = 4 // after the session ended
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.TransactionHistory
{
    public class TransactionHistoryFilter
    {
        public int? FinalPictureNumber { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? AccountID { get; set; }
    }
}

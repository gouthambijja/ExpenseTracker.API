using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.BLL.RequestModels
{
    public   class LoginModel
    {
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string Password { get; set; }
    }
}

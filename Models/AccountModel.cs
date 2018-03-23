using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealStateWebApi.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
            resultModel=new ResultModel();

        }
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string Role { get; set; }
        public string MobileNo { get; set; }
        public ResultModel resultModel { get; set; }
    }
}
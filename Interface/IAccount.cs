using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealStateWebApi.Models;

namespace RealStateWebApi.Interface
{
    interface IAccount
    {
        ResultModel Adduser(AccountModel Models);
        ResultModel DeleteUser(long UserID);
        ResultModel UpdateUser(AccountModel model);
        AccountModel GetUserLogin(AccountModel model);
        List<AccountModel> AllUser();
        AccountModel GetUserdetailbyUserId(long UserID);
    }
}

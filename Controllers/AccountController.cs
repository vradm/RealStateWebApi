using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RealStateWebApi.DBManager;
using RealStateWebApi.Interface;
using RealStateWebApi.Respository;
using RealStateWebApi.Models;
namespace RealStateWebApi.Controllers
{
    public class AccountController : ApiController
    {
        static readonly IAccount _Repository = new AccountRepository(new RealStateEntities());


        [HttpPost]
        public IHttpActionResult Adduser(AccountModel inputModel)
        {

            return Json(_Repository.Adduser(inputModel));

        }
        [HttpGet]
        public IHttpActionResult DeleteUser(long Id)
        {
            return Json(_Repository.DeleteUser(Id));

        }
        [HttpGet]
        public IHttpActionResult EditUser(long Id)
        {
            return Json(_Repository.GetUserdetailbyUserId(Id));
        }

        [HttpPost]
        public IHttpActionResult UpdateUser(AccountModel model)
        {
            return Json(_Repository.UpdateUser(model));


        }
        [HttpPost]
        public IHttpActionResult GetUserLogin(AccountModel model)
        {
            return Json(_Repository.GetUserLogin(model));


        }
        [HttpGet]
        public IHttpActionResult AllUser()
        {
            return Json(_Repository.AllUser());

        }
    }



}



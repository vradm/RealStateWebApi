using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealStateWebApi.Interface;
using RealStateWebApi.Models;
using RealStateWebApi.DBManager;
namespace RealStateWebApi.Respository
{
    public class AccountRepository : IAccount
    {
        private RealStateEntities _Context;
        public AccountRepository()
        {
            this._Context = new RealStateEntities();
        }
        public AccountRepository(RealStateEntities context)
        {
            this._Context = context;
        }
        public ResultModel Adduser(AccountModel Model)
        {
            ResultModel resultModel = new ResultModel();
            using (var context = new RealStateEntities())
            {
                try
                {
                    var result = context.UserMasters.Where(x => x.Email == Model.Email).FirstOrDefault();
                    if (result != null)
                    {
                        resultModel.Message = "This Email Id is Already Exist";

                    }
                    else
                    {
                        var query = context.UserMasters.Create();
                        query.CreatedBy = 1;
                        query.CreatedDate = DateTime.Now;
                        query.Email = Model.Email;
                        query.FirstName = Model.FirstName;
                        query.LastName = Model.LastName;
                        query.MobileNo = Model.MobileNo;
                        query.password = Model.password;
                        query.Role = Model.Role;
                        query.Status = true;

                        context.UserMasters.Add(query);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            resultModel.Message = "Your Record is succuessfully inserted";

                        }
                        else
                        {
                            resultModel.Message = "Your Record is not  inserted";
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultModel.Message = ex.Message;
                    return resultModel;

                }
                //return Ok(false);
            }
            return resultModel;


        }
        public ResultModel DeleteUser(long UserID)
        {
            ResultModel resultModel = new ResultModel();
            using (var context = new RealStateEntities())
            {
                try
                {
                    var Result = (from e in context.UserMasters where e.UserID == UserID select e).FirstOrDefault();
                    if (Result != null)
                    {
                        context.UserMasters.Attach(Result);
                        context.UserMasters.Remove(Result);
                        context.SaveChanges();
                        resultModel.Message = "Record Has been Deleted";
                    }
                    else
                    {
                        resultModel.Message = "Record Not Found";
                    }


                }
                catch (Exception ex)
                {
                    resultModel.Message = ex.Message;
                    return resultModel;

                }
            }
            return resultModel;
        }
        public ResultModel UpdateUser(AccountModel model)
        {
            ResultModel resultModel = new ResultModel();
            using (var context = new RealStateEntities())
            {
                try
                {

                    var result = (from e in context.UserMasters where e.UserID == model.UserID select e).FirstOrDefault();
                    if (result != null)
                    {
                        result.FirstName = model.FirstName;
                        result.LastName = model.LastName;
                        result.MobileNo = model.MobileNo;
                        result.ModifiedDate = DateTime.Now;
                        result.Email = model.Email;
                        result.Status = true;
                        result.Role = model.Role;
                        result.ModifiedBy = Convert.ToInt32(model.UserID);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            resultModel.Message = "Your Record  has been succuessfully Updated";
                        }
                        else
                        {
                            resultModel.Message = "Your Record  Not Updated";

                        }
                    }

                }
                catch (Exception ex)
                {
                    resultModel.Message = ex.Message;


                }
            }
            return resultModel;
        }
        public AccountModel GetUserLogin(AccountModel model)
        {
            //AccountModel AccountModel = new AccountModel();
            try
            {

                using (var context = new DBManager.RealStateEntities())
                {
                    var loginDtl = context.UserMasters.Where(x => x.Email == model.Email && x.password == model.password).FirstOrDefault();
                    if (loginDtl != null)
                    {
                        model.FirstName = loginDtl.FirstName;
                        model.LastName = loginDtl.LastName;
                        model.Email = loginDtl.Email;
                        model.UserID = loginDtl.UserID;
                        model.Role = loginDtl.Role;
                        model.resultModel.Status = true;
                    }
                    else
                    {
                        model.resultModel.Message = "InvalUserID Credentials";
                        model.resultModel.Status = false;
                    }

                }
            }
            catch (Exception ex)
            {
                model.resultModel.Message = ex.Message;
                model.resultModel.Status = false;
            }
            return model;

        }
        public List<AccountModel> AllUser()
        {
            List<AccountModel> models = new List<AccountModel>();
            try
            {
                using (var context = new DBManager.RealStateEntities())
                {
                    var result = (from e in context.UserMasters
                                  select new AccountModel
                                  {
                                      UserID = e.UserID,
                                      Email = e.Email,
                                      CreatedBy = e.CreatedBy,
                                      Role = e.Role,
                                      Status = e.Status,
                                      CreatedDate = e.CreatedDate,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      MobileNo = e.MobileNo

                                  }).ToList();
                    return models = result;
                }

            }
            catch (Exception ex)
            {

            }
            return models;
        }
        public AccountModel GetUserdetailbyUserId(long UserID)
        {
            AccountModel model = new AccountModel();
            try
            {
                using (var context = new RealStateEntities())
                {
                    var result = (from u in context.UserMasters.Where(x => x.UserID == UserID)
                                  select new AccountModel
                                  {
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Email = u.Email,
                                      MobileNo = u.MobileNo,
                                      UserID = u.UserID,
                                      Role = u.Role

                                  }).FirstOrDefault();
                    model = result;
                }

            }
            catch (Exception ex)
            {


            }
            return model;
        }


    }
}
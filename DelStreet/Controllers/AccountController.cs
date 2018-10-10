
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;

using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using BussinessManager.IService;
using ModelMapper;


namespace Fairwater.UI.Controllers
{

    public class AccountController : Controller
    {
        #region Fields

        // Object declaration. 
        private readonly IAccountService _IAccountService;


        #endregion

        #region DI

        public AccountController(IAccountService _IAccountService)
        {
            this._IAccountService = _IAccountService;

        }
        #endregion
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetOTP(string phoneNumber, string otp)
        {
            //Generate RandomNo
            UserModel model = new UserModel();
            bool isLogin = false;
            if (string.IsNullOrEmpty(otp))
            {
                var user = _IAccountService.GetUserDetailByPhoneNumber(phoneNumber).FirstOrDefault();
                if (user == null)
                {
                    int _min = 1000;
                    int _max = 9999;
                    Random _rdm = new Random();
                    var uniqueNumber = _rdm.Next(_min, _max);


                    model.OTP = Convert.ToString(uniqueNumber);
                    model.PhoneNumber = phoneNumber;
                    _IAccountService.InsertUserDetail(model);
                }
                else
                {
                    int _min = 1000;
                    int _max = 9999;
                    Random _rdm = new Random();
                    var uniqueNumber = _rdm.Next(_min, _max);


                    model.OTP = Convert.ToString(uniqueNumber);
                    model.UserId = user.UserId;
                    _IAccountService.UpdateUserDetail(model);
                }
            }
            else
            {
                var user = _IAccountService.GetUserDetailByPhoneNumber(phoneNumber).FirstOrDefault();
                if (user.OTP == otp)
                {
                    isLogin = true;

                }
                else
                {

                }
            }

            return Json(new { isLogin = isLogin, phoneNumber = phoneNumber }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SinUp(string phoneNumber)
        {
            UserModel userModel = new UserModel();
            userModel.PhoneNumber = phoneNumber;
            return View(userModel);
        }
        [HttpPost]
        public ActionResult SinUp(UserModel userModel)
        {

            var user = _IAccountService.GetUserDetailByPhoneNumber(userModel.PhoneNumber).FirstOrDefault();
            user.OTP = user.OTP;
            user.UserName = userModel.UserName;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Password = userModel.Password;
            user.Email = userModel.Email;
            _IAccountService.UpdateUserDetail(user);
            return Json(new { isStatus=true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DashBoard()
        {
            return View();
        }
    }



}

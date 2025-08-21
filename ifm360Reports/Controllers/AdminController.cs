using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ifm360Reports.Controllers
{
    public class AdminController : Controller
    {
        db_Utility db = new db_Utility();
        public IActionResult UserMapToCustomer()
        {
            ViewBag.Company = db.PopulateDropDown("exec Usp_GroupLNewAppDLL 'Company'", db.strElect);


            ViewBag.User = db.PopulateDropDown("exec Usp_GroupLNewAppDLL 'User'", db.strElect);


            return View();
        }
        [HttpPost]
        public JsonResult BindCustomer(string Company)
        {
            var ds = db.Fill("exec Usp_GroupLNewAppDLL 'Customer', @Id='" + Company + "'", db.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        [HttpPost]
        public JsonResult SaveUserMapping(string UserId, string Customer,string CustomerName)
        {

            var ds = db.Fill("exec Usp_GroupLNewApp_UserMapping @UserId='" + UserId + "', @Customer='" + Customer + "',@CustomerName='"+CustomerName+"'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }
        [HttpGet]
        public JsonResult ShowUserMapping()
        {

            var ds = db.Fill("exec Usp_GroupLNewAppDLL 'UserMapList'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }
    }
  
}



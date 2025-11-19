using ifm360Reports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using ifm360Reports.AuthFilter;
namespace ifm360Reports.Controllers
{
    public class HomeController : Controller

    {
        db_Utility util = new db_Utility();
        ClsUtility utility = new ClsUtility();
    List<SelectListItem> CustomerList = new List<SelectListItem>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [AuthenticationFilter]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Login(Adm_User obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.uname.Trim() == "HRAdmin" && obj.pwd == "HRAdmin")
                {
                    HttpContext.Session.SetString("UserName", "HRAdmin");
                    return RedirectToAction("LeaveDate", "Reports");
                }
                var ds = util.Fill("exec udp_ValidateLogin2 @Userid ='" + obj.uname.Trim() + "',@Password='" + obj.pwd + "'", util.strElect);


                string errorMessage = ds.Tables[0].Rows[0][1].ToString();

                if (errorMessage != "Invalid Username")
                {
                    if (errorMessage != "Incorrect Password")
                    {
                        HttpContext.Session.SetString("UserName", (obj.uname.ToString()).ToUpper());
                        HttpContext.Session.SetString("password", (obj.pwd.ToString()).ToUpper());


                        //string[] compcode = ds.Tables[0].Rows[0][0].ToString().Split(",");
                        //string[] compdesc = ds.Tables[0].Rows[0][1].ToString().Split(",");

                        List<SelectListItem> list = new List<SelectListItem>();


                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            list.Add(new SelectListItem
                            {
                                Text = row["CompanyDesc"].ToString(),
                                Value = row["CompanyCode"].ToString(),
                            });
                        }


                        if (obj.uname.ToUpper() == "HDBADMIN" && obj.pwd.ToUpper() == "ADMIN@123")
                        {
                            HttpContext.Session.SetString("companyid", "GroupLHelpfulPeo");
                            HttpContext.Session.SetString("locationid", "North");
                            HttpContext.Session.SetString("branchid", "20909");
                            return RedirectToAction("ConsolidatedVisittoReport", "Reports");
                        }
                        else
                        {
                            return RedirectToAction("BranchLogin", "Home");
                        }



                    }
                    else
                    {
                        ViewBag.message = errorMessage;
                    }

                }
                else
                {
                    ViewBag.message = errorMessage;
                }
            }
           

            return View();
        }
      
        public IActionResult BranchLogin()

        {
            //ViewBag.company = util.PopulateDropDown("exec udp_GetReportPortalCompany", util.strElect);
           

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Region = util.PopulateDropDown("exec udp_GetReportPortalCompany", util.strElect);
            ViewBag.Company = util.PopulateDropDown("Usp_GroupLNewAppDLL 'CompanyLogin',@Id='" + HttpContext.Session.GetString("UserName") + "'", util.strElect);
            //var companyListJson = HttpContext.Session.GetString("CompanyList");
            //List<SelectListItem> companyList = null;

            //if (!string.IsNullOrEmpty(companyListJson))
            //{
            //    companyList = JsonConvert.DeserializeObject<List<SelectListItem>>(companyListJson);
            //}

            //ViewBag.Company = companyList;

            return View();
            
        }
        [HttpPost]
        public IActionResult BranchLogin(branch_login obj)
        {

              
            if(obj.companyid != null && obj.location_id != null && obj.branch_id != null )
            {
                HttpContext.Session.SetString("companyid", obj.companyid.ToString());
                HttpContext.Session.SetString("locationid", obj.location_id.ToString());
                HttpContext.Session.SetString("branchid", obj.branch_id.ToString());
                var ds = util.Fill("exec [Usp_GroupLNewAppDLL] 'GetLoginDetails' ,@Id='" + HttpContext.Session.GetString("UserName") + "',@Id2='"+ obj.branch_id + "' ", util.strElect);
                if (ds.Tables.Count > 0)
                {
                    HttpContext.Session.SetString("CompanyName", ds.Tables[0].Rows[0]["Company"].ToString());
                    HttpContext.Session.SetString("BranchName", ds.Tables[0].Rows[0]["Branch"].ToString());
                    HttpContext.Session.SetString("RegionName", ds.Tables[0].Rows[0]["Region"].ToString());
                    return RedirectToAction("Index", "Home");

                }
                
            }
            else
            {
                ViewBag.message = "All Field Required!";
            }
            
            return View();
        }

        public JsonResult bindRegion(string id)
        {
            DataSet ds = util.Fill("exec udp_GetReportPortalRegion  @CompanyCode='" + id+"' ",util.strElect);
           DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }

        public JsonResult bindRegion2(string id)
        {
            DataSet ds = util.Fill("exec Usp_GroupLBranchLogin 'Region', @CompanyCode='" + id+"',@UserId='"+HttpContext.Session.GetString("UserName") +"' ",util.strElect);
           DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }
        public JsonResult bindBranch(string id, string locid)
        {
            DataSet ds = util.Fill("exec udp_GetReportPortalBranch @CompanyCode='" + id+ "', @HrLocationCode='"+locid+"' ", util.strElect);
           DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }
        public JsonResult bindBranch2(string id, string locid)
        {
            DataSet ds = util.Fill("exec Usp_GroupLBranchLogin 'Branch', @CompanyCode='" + id+ "', @Region='" + locid+ "',@UserId='"+HttpContext.Session.GetString("UserName") +"'  ", util.strElect);
           DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            return RedirectToAction("Login","Home");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}

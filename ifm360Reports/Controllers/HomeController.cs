using ifm360Reports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace ifm360Reports.Controllers
{
    public class HomeController : Controller

    {
        db_Utility util = new db_Utility();
        ClsUtility utility = new ClsUtility();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
            var ds = util.Fill("exec udp_ValidateLogin @Userid ='" + obj.uname + "',@Password='" + obj.pwd + "'", util.strElect);
         
            
                string errorMessage = ds.Tables[0].Rows[0][1].ToString();

                if (errorMessage != "Invalid Username")
                {
                if (errorMessage != "Incorrect Password")
                {
                    HttpContext.Session.SetString("UserName", (obj.uname.ToString()).ToUpper());
                    HttpContext.Session.SetString("password", (obj.pwd.ToString()).ToUpper());

                    string[] compcode = ds.Tables[0].Rows[0][0].ToString().Split(",");
                    string[] compdesc = ds.Tables[0].Rows[0][1].ToString().Split(",");

                    List<SelectListItem> list = new List<SelectListItem>();
                    

                    for(var i = 0; i < compcode.Length; i++)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = compdesc[i],
                            Value = compcode[i],
                        });
                    }
                    
                    HttpContext.Session.SetString("CompanyList", JsonConvert.SerializeObject(list));

                    if(obj.uname.ToUpper() == "HDBADMIN" && obj.pwd.ToUpper() == "ADMIN@123" )
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
            
           

            return View();
        }
        
        public IActionResult BranchLogin(DataSet data)

        {
            //ViewBag.company = util.PopulateDropDown("exec udp_GetReportPortalCompany", util.strElect);
           

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Region = util.PopulateDropDown("exec udp_GetReportPortalCompany", util.strElect);
            var companyListJson = HttpContext.Session.GetString("CompanyList");
            List<SelectListItem> companyList = null;

            if (!string.IsNullOrEmpty(companyListJson))
            {
                companyList = JsonConvert.DeserializeObject<List<SelectListItem>>(companyListJson);
            }

            ViewBag.Company = companyList;

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
                return RedirectToAction("PhotoAttendance", "Reports");
            }
            else
            {
                ViewBag.message = "All Field Required!";
            }
            
            return View();
        }

        public JsonResult bindRegion(string id)
        {
            DataSet ds = util.Fill("exec udp_GetReportPortalRegion @CompanyCode='"+id+"' ",util.strElect);
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}

using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ifm360Reports.AuthFilter;
using ifm360Reports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace ifm360Reports.Controllers
{
    [AuthenticationFilter]
    public class AdminController : Controller
    {

        ResponseMessage _Response = new ResponseMessage();

        db_Utility db = new db_Utility();
        private readonly string CompanyId;
        private readonly string locationid;
        private readonly string branchid;
        private readonly string UserId;
        public AdminController(IHttpContextAccessor context)
        {
            CompanyId = context.HttpContext.Session.GetString("companyid");
            locationid = context.HttpContext.Session.GetString("locationid");
            branchid = context.HttpContext.Session.GetString("branchid");
            UserId = context.HttpContext.Session.GetString("UserName");
        }
        public IActionResult UserMapToBranch()
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
        public JsonResult bindCustomertoMap(string Id)
        {
            var ds = db.Fill("exec Usp_GroupLNewAppDLL 'Customer', @Id='" + Id + "'", db.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        
        [HttpPost]
        public JsonResult bindRegion(string[] Id)
        {
            string result = string.Join(",", Id.Select(x => $"{x}"));
            DataSet ds = db.Fill($"exec udp_GetReportPortalMappingDDL 'Region', @CompanyCode='{result}' ", db.strElect);
            DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }

        [HttpPost]
        public JsonResult SaveUserMapping(string UserId, string Company, string Region, string[] Branch)
        {
            string Branchs = string.Join(",", Branch.Select(x => $"{x}"));
            var ds = db.Fill($"exec [Usp_GroupLNewApp_UserMapping] @UserId='{UserId}', @Company='{Company}',@Region='{Region}',@Branch='{Branchs}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }

        [HttpPost]
        public JsonResult SaveCustomerMapping(string UserId,  string[] Customer)
        {
            string Customers = string.Join(",", Customer.Select(x => $"{x}"));
            var ds = db.Fill($"exec [Usp_GroupLNewApp_UserCustomerMapping] @UserId='{UserId}', @Customer='{Customers}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }

        [HttpPost]
        public JsonResult DeleteCustomerMap(string UserId)
        {
          
            var ds = db.Fill($"exec [Usp_GroupLNewAppDLL] 'DeleteCustomerMap' ,@Id='{UserId}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }

        [HttpPost]
        public JsonResult DeleteBranchMap(string Id)
        {
          
            var ds = db.Fill($"exec [Usp_GroupLNewAppDLL] 'DeleteBranchMap' ,@Id='{Id}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }
        [HttpGet]
        public JsonResult ShowUserMapping(string UserId)
        {

            var ds = db.Fill($"exec Usp_GroupLNewAppDLL 'UserMapList',@Id='{UserId}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }
        [HttpGet]
        public JsonResult GetTour(string Id)
        {

            var ds = db.Fill($"exec  Udp_GetClientTourGroupL @ClientCode=N'{Id}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }
        [HttpGet]
        public JsonResult UserBranchList(string UserId,string Company,string Region)
        {

            var ds = db.Fill($"exec Usp_GroupLNewAppDLL 'UserBranchList',@Id='{UserId}',@Id2='{Company}',@Id3='{Region}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }
        [HttpGet]
        public JsonResult ShowCustomerMapping(string UserId)
        {

            var ds = db.Fill($"exec Usp_GroupLNewAppDLL 'CustomerMapList' ,@Id='{UserId}'", db.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }


        public IActionResult UserMapToCustomer()
        {
            ViewBag.User = db.PopulateDropDown("exec Usp_GroupLNewAppDLL 'User'", db.strElect);
            ViewBag.Customer = db.PopulateDropDown("exec Usp_GroupLNewAppDLL 'Customer'", db.strElect);
            return View();
        }


        public IActionResult MenuRight()
        {
            ViewBag.User = db.PopulateDropDown("exec Usp_GroupLNewAppDLL 'User'", db.strElect);
            ViewBag.MenuParent = db.PopulateDropDown("exec Usp_GroupLNewAppDLL 'MenuParent'", db.strElect);
            return View();
        }
        public JsonResult showMenuRight(string parentMenu, string user)
        {
            string query = "exec Usp_GroupLNewAppDLL 'MenuRight',@ParentMenuName='" + parentMenu + "',@Id='" + user + "'";
            DataSet ds = db.Fill(query, db.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        [HttpPost]
        public JsonResult saveMenuRight()
        {
            var data = Request.Form["data"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(data);
            using (SqlConnection conn = new SqlConnection(db.strElect))
            using (SqlCommand cmd = new SqlCommand("Usp_GroupLNewApp_MenuRightsMapping", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@data", dt);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "GroupLMenuRights";

                DataTable result = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(result);

                }
                return Json(JsonConvert.SerializeObject(result));
            }
        }

        public IActionResult StatusManage(string Id)
        {
           
            var companyid = HttpContext.Session.GetString("companyid");
            ViewBag.Customer= db.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap',@Id2='" + companyid + "', @Id = '" + HttpContext.Session.GetString("UserName") + "'", db.strElect);

            ViewBag.Region = db.PopulateDropDown("exec  udp_GetReportPortalRegion  @CompanyCode='" + companyid + "'", db.strElect);

            string type= "";
            if (Id == "Site")
            {
                type = "Site Active and Inactive";
            }
          if( Id == "Employee")
            {
                type = "Employee Active and Inactive";
            }
          ViewBag.Type = type;
            ViewBag.Id = Id;
            return View();
        }

        public ResponseMessage snowStatusDetails(string Type,string CleintCode,string BranchId,string isactive)
        {
            DataSet ds = new DataSet();
            ds = db.Fill(@$"exec Usp_GroupLNewApp_EmpandSiteActiveInavtive @Action='{Type}',@CompanyCode='{CompanyId}',@CleintCode='{CleintCode}',@BranchId='{BranchId}',@status='{isactive}'",db.strElect);
            if(ds.Tables.Count>0)
            {
                _Response.StatusCode = System.Net.HttpStatusCode.OK;
                _Response.Message = "Success";
                _Response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _Response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _Response.Message = "No Data Found";
                _Response.Data = null;
            }
            return _Response;

        }

        public JsonResult saveManageStatus(string Clientcode,string type,string BranchId)
        {
            var data = Request.Form["data"];
            var dt = BulkInsert.BulkSave( data,  Clientcode,BranchId , type,db.strElect);
            return Json(JsonConvert.SerializeObject(dt));
        }
      



    }
  
}



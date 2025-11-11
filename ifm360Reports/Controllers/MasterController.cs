using ifm360Reports.AuthFilter;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace ifm360Reports.Controllers
{
    [AuthenticationFilter]
    public class MasterController : Controller
    {
       private readonly db_Utility util = new db_Utility();
       private readonly ClsUtility utility = new ClsUtility();
        protected string Customer = string.Empty;
        protected int Customerlevel;
        protected string UserId;

        #region Constructor 
        public MasterController(IHttpContextAccessor httpContextAccessor)
        {
            Customer = httpContextAccessor.HttpContext.Session.GetString("Customer");
            UserId = httpContextAccessor.HttpContext.Session.GetString("UserName");
            Customerlevel = Convert.ToInt16(httpContextAccessor.HttpContext.Session.GetString("CustomerLevel"));
        }
        #endregion

        #region Client Budget
        public IActionResult ClientBudget()
        {
            ViewBag.Client= util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'ClientForMaster', @Id = '" + HttpContext.Session.GetString("branchid") + "'", util.strElect);
            return View();
        }
        public JsonResult SaveClient(int Id, string ClientCode, decimal Amount,string SiteID)
        {
            var ds = util.Fill($"exec Usp_GroupLNewAppBranchAmountMaster 'Insert',@Id='{Id}',@ClientCode='{ClientCode}',@Amount='{Amount}',@SiteId='{SiteID}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult GetClient(string ClientCode)
        {
            var ds = util.Fill($"exec Usp_GroupLNewAppBranchAmountMaster 'ShowList',@ClientCode='{ClientCode}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult DeleteClientBugdet(int Id)
        {
            var ds = util.Fill($"exec Usp_GroupLNewAppBranchAmountMaster 'Delete',@Id='{Id}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        #endregion

        public IActionResult CheckList()
        {
            ViewBag.Client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'ClientForMaster', @Id = '" + HttpContext.Session.GetString("branchid") + "'", util.strElect);
            return View();
        }

        #region Frequency Master
        public IActionResult Frequency()
        {
            ViewBag.Client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'ClientForMaster', @Id = '" + HttpContext.Session.GetString("branchid") + "'", util.strElect);
            return View();
        }

        public JsonResult SaveFrequency(int Id, string ClientCode, string Frequency,string StartTime,string EndTime)
        {
            var ds = util.Fill($"exec Usp_GrouplNewAppChecklistFrequency 'Insert',@Id='{Id}',@ClientCode='{ClientCode}',@Frequency='{Frequency}',@FromTime='{StartTime}',@ToTime='{EndTime}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult GetFrequency(string ClientCode)
        {
            var ds = util.Fill($"exec Usp_GrouplNewAppChecklistFrequency 'ShowList',@ClientCode='{ClientCode}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult DeleteFrequency(int Id)
        {
            var ds = util.Fill($"exec Usp_GrouplNewAppChecklistFrequency 'Delete',@Id='{Id}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }


        #endregion

        #region CheckList Master
        public IActionResult CheckListMaster()
        {
            ViewBag.Client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'ClientForMaster', @Id = '" + HttpContext.Session.GetString("branchid") + "'", util.strElect);

            ViewBag.Datatype = util.PopulateDropDown("exec Usp_GrouplNewAppChecklistMaster 'Datatypedropdown'", util.strElect);
            return View();
        }

        public JsonResult SaveCheckListMaster(int Id, string ClientCode, string HeaderId,string CheckListName,string[] DataType, string[] DataTypeNames)
        {
            string DataTypes = string.Join(",", DataType);
            string DataTypeName = string.Join(",", DataTypeNames);
            var ds = util.Fill($"exec Usp_GrouplNewAppChecklistMaster 'Insert',@Id='{Id}',@ClientCode='{ClientCode}',@HeaderId='{HeaderId}',@ChecklistName='{CheckListName}',@Datatype='{DataTypes}',@DatatypeName='{DataTypeName}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult GetCheckListMaster(string ClientCode)
        {
            var ds = util.Fill($"exec Usp_GrouplNewAppChecklistMaster 'ShowList',@ClientCode='{ClientCode}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult DeleteCheckListMaster(int Id)
        {
            var ds = util.Fill($"exec Usp_GrouplNewAppChecklistMaster 'Delete',@Id='{Id}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult CheckListHeader(string ClientCode)
        {
            var ds = util.Fill($"exec Usp_GrouplNewAppChecklistMaster 'Headerdropdown',@ClientCode='{ClientCode}'", util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        #endregion

        #region CheckListHeaderMaster
        public IActionResult CheckListHeaderMaster()
        {
            ViewBag.Client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'ClientForMaster', @Id = '" + HttpContext.Session.GetString("branchid") + "'", util.strElect);
            return View();
        }

        public JsonResult GetCheckListHeader(string val)
        {
            string query = "exec [Usp_GrouplNewAppChecklistHeaderMaster] 'select',@ClientCode='" + val + "'";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult addCheckListHeader(string clientCode, string checkListHeader, string AutoId)
        {
            string query = "exec [Usp_GrouplNewAppChecklistHeaderMaster] 'insert',@ClientCode='" + clientCode + "', @checkListHeader='" + checkListHeader + "',@Id='" + AutoId + "'";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult deleteCheckListHeader(string AutoId)
        {
            string query = "exec [Usp_GrouplNewAppChecklistHeaderMaster] 'delete', @Id='" + AutoId + "'";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        #endregion


        #region Email Master
        public IActionResult EmailMaster()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertEmailMaster(string EmpId, string EmpName, string EmailId, int Id)
        {
            if (Id == 0)
            {
                string query = "[Usp_GrouplNewAppEmpEmailMaster] 'insert', @EmpID='" + EmpId.Trim() + "',@EmpName='" + EmpName + "',@EmailId='" + EmailId + "'";
                DataSet ds = util.Fill(query, util.strElect);
                return Json(JsonConvert.SerializeObject(ds.Tables[0]));
            }
            else
            {
                string query = "[Usp_GrouplNewAppEmpEmailMaster] 'Update', @EmpID='" + EmpId.Trim() + "',@EmpName='" + EmpName + "',@EmailId='" + EmailId + "'";
                DataSet ds = util.Fill(query, util.strElect);
                return Json(JsonConvert.SerializeObject(ds.Tables[0]));
            }
        }

        public JsonResult GetEmailMaster()
        {
            string query = "Usp_GrouplNewAppEmpEmailMaster 'ShowList'";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        [HttpPost]
        public JsonResult DeleteEmailMaster(string EmpId)
        {
            string query = "Usp_GrouplNewAppEmpEmailMaster 'Delete',@EmpID='" + EmpId + "'";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        #endregion


    }
}

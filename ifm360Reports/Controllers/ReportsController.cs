using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Security.Policy;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ifm360Reports.Controllers
{
    public class ReportsController : Controller
	{
		db_Utility util = new db_Utility();
		ClsUtility utility = new ClsUtility();

		public IActionResult PhotoAttendance()

		{

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			//var uname = HttpContext.Session.GetString("UserName").ToUpper();

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
			{
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });

                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.client = ddl;
                ViewBag.Region = ddl2;
               // ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
			else
			{
                ViewBag.client = util.PopulateDropDown("exec udp_GetClientFromBranch @CompanyCode='" + companyid + "',@LocationAutoId ='All'", util.strElect);
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }
			
			

			

			return View();
		}

		public IActionResult AttendanceSuper()
		{

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                ViewBag.client = ddl;
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
            else
            {
                ViewBag.client = util.PopulateDropDown("exec udp_GetClientFromBranch @CompanyCode='" + companyid + "',@LocationAutoId ='All'", util.strElect);
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }
            

			return View();
		}

		public IActionResult VisitReport(string date, string client, string AsmtId, string vtype, string empid,string LocautoId)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataTable dt = new DataTable();
			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();

			var ds = util.Fill("exec udp_GetVisitReportUpdated @LocationAutoID ='" + LocautoId + "',@FromDate='" + date + "',@AsmtID='" + AsmtId + "',@ClientCode='" + client + "',@VisitType='" + vtype + "',@Empid='" + empid + "'", util.strElect);
			ViewBag.dt = ds.Tables[0];
			ViewBag.dt1 = ds.Tables[1];
			ViewBag.dt2 = ds.Tables[2];

			return View();
		}


        public IActionResult VisittopReport(string date, string client, string AsmtId, string vtype, string empid, string LocautoId)
        {
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            var ds = util.Fill("exec udp_GetVisitReportUpdated @LocationAutoID ='" + LocautoId + "',@FromDate='" + date + "',@AsmtID='" + AsmtId + "',@ClientCode='" + client + "',@VisitType='" + vtype + "',@Empid='" + empid + "'", util.strElect);
            ViewBag.dt = ds.Tables[0];
            ViewBag.dt1 = ds.Tables[1];
            ViewBag.dt2 = ds.Tables[2];

            return View();
        }

        public IActionResult ConsolidatedVisitReport()
		{
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.customer = ddl;
                ViewBag.Region = ddl2;

            }
			else
			{
                ViewBag.customer = util.PopulateDropDown("exec udpMstSale_Client_Get @LocationAutoID ='" + branchid + "'", util.strElect);

                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
            }

           
           
			return View();
		}
        public IActionResult ConsolidatedVisittoReport()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.customer = ddl;
                ViewBag.Region = ddl2;

            }
            else
            {
                ViewBag.customer = util.PopulateDropDown("exec udpMstSale_Client_Get @LocationAutoID ='" + branchid + "'", util.strElect);

                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
            }



            return View();
        }


        public JsonResult GetConsolidatedVisitReport(string todate, string fromdate, string clientcode ,string Regid)
		{

			DataTable dt = new DataTable();
			var branchid = HttpContext.Session.GetString("branchid");
			var ds = util.Fill("exec udp_GetCustomerFeedback @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@ToDate='" + todate + "',@ClientCode='" + clientcode + "', @Region='"+Regid+"' ", util.strElect);

			dt = ds.Tables[0];

			return Json(JsonConvert.SerializeObject(dt));
		}



		[HttpPost]
		public JsonResult GetSearchPhotoAttendance(string todate, string Region, string Empno, string client, string shift,string fromdate)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			string div = "";
			DataTable dt = new DataTable();

			var ds = util.Fill("exec udp_GetEmployeePhotoAttendanceGroupL @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@CompanyCode='" + companyid + "',@Region='" + Region + "',@ClientCode='" + client + "',@Shift='" + shift + "',@ToDate='" + todate + "' ", util.strElect);
			dt = ds.Tables[0];

			return Json(JsonConvert.SerializeObject(dt));
		}

		[HttpPost]

		public JsonResult GetSearchAttendanceSuper(string date, string site, string Empno, string client, string shift,string fromdate)
		{


			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

			string div = "";
			DataTable dt = new DataTable();


			var ds = util.Fill("exec udp_GetEmployeeSuperAttendanceGroupL @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@AsmtCode='" + site + "',@ClientCode='" + client + "',@ShiftCode='" + shift + "',@ToDate='" + date + "' ", util.strElect);
			dt = ds.Tables[0];
			
			return Json(JsonConvert.SerializeObject(dt));
		}


		[HttpPost]

		public JsonResult bindSite(string siteid)
		{
            
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @LocationAutoID ='" + branchid + "',@BaseCompanyCode ='" + companyid + "',@ClientCode='" + siteid + "' ", util.strElect);
			DataTable dt = ds.Tables[0];
			var data = JsonConvert.SerializeObject(dt);
			return Json(data);
		}


		[HttpPost]

		public JsonResult bindclient(string custid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");


            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("ClientCode", typeof(string));
                dt.Columns.Add("ClientName", typeof(string));

                dt.Rows.Add("GL0072", "Hdb Financial Services Limited");

                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }
            else
			{
                DataSet ds = util.Fill("exec udpMstSale_Client_Asmt_Get @LocationAutoID ='" + branchid + "',@ClientCode ='" + custid + "'", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);

                return Json(data);
            }
          
		}
		[HttpPost]
		public JsonResult bindvisittype(string custid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataSet ds = util.Fill("exec udpGetVisitType @LocationAutoID ='" + branchid + "',@ClientCode ='" + custid + "'", util.strElect);
			DataTable dt = ds.Tables[0];
			var data = JsonConvert.SerializeObject(dt);
			return Json(data);
		}


		[HttpPost]
		public JsonResult GetVisitReport(string date, string custid, string client, string visittype, string EmpId)
		{


			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

			string div = "";
			DataTable dt = new DataTable();
			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();


			var ds = util.Fill("exec udp_GetVisitReportUpdated @LocationAutoID ='" + branchid + "',@FromDate='" + date + "',@AsmtID='" + client + "',@ClientCode='" + custid + "',@VisitType='" + visittype + "',@Empid='" + EmpId + "' ", util.strElect);
			dt = ds.Tables[0];
			dt1 = ds.Tables[1];
			dt2 = ds.Tables[2];
			var data = new { dt = JsonConvert.SerializeObject(dt), dt1 = JsonConvert.SerializeObject(dt1), dt2 = JsonConvert.SerializeObject(dt2) };

			return Json(data);
		}

		#region Shortage Report 
		public IActionResult ShortageReport()
		{
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                ViewBag.client = ddl;


			}
			else
            {
                ViewBag.client = util.PopulateDropDown("exec udp_GetClientFromBranch @CompanyCode='" + companyid + "',@LocationAutoId ='" + branchid + "'", util.strElect);

            }
        

			return View();
		}
		public JsonResult BindBranchtoReg(string regid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("LocationAutoID", typeof(string));
                dt.Columns.Add("LocationCode", typeof(string));

                dt.Rows.Add("20909", "Lucknow");

                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }
			else {

                DataSet ds = util.Fill("exec udp_GetBranchFromRegion @baseCompanyCode ='" + companyid + "',@region ='" + regid + "'", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);
                return Json(data);
            }
		}
		public JsonResult BindSitetoClient(string clietnid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @BaseCompanyCode ='" + companyid + "',@ClientCode ='" + clietnid + "', @LocationAutoID='" + branchid + "'", util.strElect);
			DataTable dt = ds.Tables[0];
			var data = JsonConvert.SerializeObject(dt);
			return Json(data);
		}
		[HttpPost]
		public JsonResult GetSearchShortageReport(string date, string Regionid, string branchid, string emptype, string clientcode, string sitecode)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			//var locationid = HttpContext.Session.GetString("locationid");

			DataSet ds = util.Fill("exec udp_GetShortageReportGrouplNewApp @ComanyCode ='" + companyid + "',@DDate ='" + date + "', @Region='" + Regionid + "', @Branch='" + branchid + "',@employeetype='" + emptype + "',@ClientCode='" + clientcode + "',@SiteCode='" + sitecode + "'", util.strElect);
			DataTable dt = ds.Tables[0];
			DataTable dt1 = ds.Tables[1];
			DataTable dt2 = ds.Tables[2];
			DataTable dt3 = ds.Tables[3];
			DataTable dt4 = ds.Tables[4];
			DataTable dt5 = ds.Tables[5];
			DataTable dt6 = ds.Tables[6];
			DataTable dt7 = ds.Tables[7];
			var data = new
			{
				dt = JsonConvert.SerializeObject(dt),
				dt1 = JsonConvert.SerializeObject(dt1),
				dt2 = JsonConvert.SerializeObject(dt2),
				dt3 = JsonConvert.SerializeObject(dt3),
				dt4 = JsonConvert.SerializeObject(dt4),
				dt5 = JsonConvert.SerializeObject(dt5),
				dt6 = JsonConvert.SerializeObject(dt6),
				dt7 = JsonConvert.SerializeObject(dt7)

			};

			return Json(data);
		}

		#endregion

		#region Shortage Report Client Wise

		public IActionResult ShortageReportClientWise()
		{
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                ViewBag.client = ddl;
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
            else
            {
                ViewBag.client = util.PopulateDropDown("exec udp_GetClientFromBranch @CompanyCode='" + companyid + "',@LocationAutoId ='All'", util.strElect);
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }
            return View();
		}

		public JsonResult GetSearchShortageReportclientWise(string date, string Regionid, string Clientid, string shiftid )
        {
            var companyid = HttpContext.Session.GetString("companyid");
            DataSet ds = util.Fill("exec udp_ShortageReportClientWise @CompanyCode ='" + companyid + "',@Date ='" + date + "', @Region='" + Regionid + "',@ClientCode='" + Clientid + "',@Shift='" + shiftid + "'", util.strElect);

            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            DataTable dt2 = ds.Tables[2];
            DataTable dt3 = ds.Tables[3];

			var data = new
			{
				dt = JsonConvert.SerializeObject(dt),
				dt1 = JsonConvert.SerializeObject(dt1),
				dt2 = JsonConvert.SerializeObject(dt2),
				dt3 = JsonConvert.SerializeObject(dt3)
			};

            
            return Json(data);
        }
        #endregion

        #region Out Of Range
		public IActionResult OutOfRange()
		{
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
            return View();
		}

		[HttpPost]
		public  JsonResult bindclienttobranch(string branchid)
		{
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
       
            if(branchid== "20909")
            {

                DataTable dt = new DataTable();

                dt.Columns.Add("ClientCode", typeof(string));
                dt.Columns.Add("ClientName", typeof(string));

                dt.Rows.Add("GL0072", "Hdb Financial Services Limited");

                var data = JsonConvert.SerializeObject(dt);
                return Json(data);
            }
            else
            {
                DataSet ds = util.Fill("exec udp_GetClientFromBranch @LocationAutoID ='" + branchid + "',@CompanyCode ='" + companyid + "' ", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }
          
        }

		public JsonResult SearchGetOutOfRange(string fromdate,string todate,string Region, string Branch,string client ,string site)
		{
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            string div = "";
            DataTable dt = new DataTable();
            var ds = util.Fill("exec udp_GetOutOfRangeReportGrouplNewApp @ComanyCode ='" + companyid + "',@DDate='" + fromdate + "',@Region='" + Region + "',@Branch='" + Branch + "',@ClientCode='" + client + "',@SiteCode='" + site + "',@ToDate='" + todate + "' ", util.strElect);
            dt = ds.Tables[0];
           
            return Json(JsonConvert.SerializeObject(dt));
        }


        public JsonResult bindSiteToOutrange(string siteid)
        {

            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @LocationAutoID ='All',@BaseCompanyCode ='" + companyid + "',@ClientCode='" + siteid + "' ", util.strElect);
            DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }








        #endregion
        #region AuditStatusReport Report
        public IActionResult AuditStatusReport()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }

            var branchid = HttpContext.Session.GetString("branchid");
            ViewBag.customer = util.PopulateDropDown("exec udpMstSale_Client_Get_Audit @LocationAutoID='"+ branchid + "' ", util.strElect);

            return View();
		}

        public JsonResult GetAudioStatusReport(string todate, string fromdate, string clientcode,string Regid)
        {
            var branchid = HttpContext.Session.GetString("branchid");

            string div = "";
            DataTable dt = new DataTable();
            

            var ds = util.Fill("exec udp_GetAuditReportWithFilters @FromDate='" + fromdate + "',@ToDate='" + todate + "', @LocationAutoId ='" + branchid + "',@ClientCode='" + clientcode + "',@Region='All' ", util.strElect);
            dt = ds.Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }


        #endregion

        public JsonResult GetAppointmentLetterAcceptance(string CompanyCodeid)
        {

            var ds = util.Fill("udp_GetAppointmentLetterAccepted @CompanyCode ='" + CompanyCodeid + "' ", util.strElect);
            DataTable dt = ds.Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }
        public IActionResult AppointmentLetterAcceptance(DataSet data)
        {
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









	}
}


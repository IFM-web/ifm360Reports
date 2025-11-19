using DocumentFormat.OpenXml.Bibliography;
using ifm360Reports.AuthFilter;
using ifm360Reports.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using System.Data;
using System.Net;
using System.Security.Policy;
using System.Text;


namespace ifm360Reports.Controllers
{
    [AuthenticationFilter]
    public class ReportsController : Controller
    {
        db_Utility util = new db_Utility();
        ClsUtility utility = new ClsUtility();
        protected string Customer = string.Empty;
        protected int Customerlevel;
        protected string UserId;
        private readonly string companyid;
        private readonly string locationid;
        private readonly string branchid;

        ResponseMessage _response = new ResponseMessage();
        public ReportsController(IHttpContextAccessor httpContextAccessor)
        {
            Customer = httpContextAccessor.HttpContext.Session.GetString("Customer");
            UserId = httpContextAccessor.HttpContext.Session.GetString("UserName");
            companyid = httpContextAccessor.HttpContext.Session.GetString("companyid");
            locationid = httpContextAccessor.HttpContext.Session.GetString("locationid");
            branchid = httpContextAccessor.HttpContext.Session.GetString("branchid");
            Customerlevel = Convert.ToInt16(httpContextAccessor.HttpContext.Session.GetString("CustomerLevel"));
        }



        public IActionResult PhotoAttendance()

        {



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
                ViewBag.client = CustomerDropDown();


                ViewBag.Region = RegionDropDown();
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }

            return View();
        }



        public IActionResult PhotoAttendanceNew()

        {


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
             
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
            else
            {
            
                ViewBag.client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" + UserId + "'", util.strElect);
                ViewBag.Region = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'RegionMap', @Id = '" + UserId + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }





            return View();
        }

        public IActionResult AttendanceSuper()
        {




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
                ViewBag.client = CustomerDropDown();
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }


            return View();
        }


        public IActionResult AttendanceSuperNew()
        {



            ViewBag.Region = util.PopulateDropDown("exec 'CustomerMap' @CompanyCode='" + companyid + "'", util.strElect);

            ViewBag.client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" + HttpContext.Session.GetString("UserName") + "'", util.strElect);
            ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);



            return View();
        }

        public IActionResult AttendanceSuperWithOutImage()
        {



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
                ViewBag.client = CustomerDropDown();
                ViewBag.Region = util.PopulateDropDown("exec 'CustomerMap' @CompanyCode='" + companyid + "'", util.strElect);


                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }


            return View();
        }


        public IActionResult AttendanceSuperWithOutImageNew()
        {


            //ViewBag.client = CustomerDropDown();
            ViewBag.Region = util.PopulateDropDown("exec 'CustomerMap' @CompanyCode='" + companyid + "'", util.strElect);

            ViewBag.client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" + UserId + "'", util.strElect);
            ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);



            return View();
        }


        public IActionResult VisitReport(string date, string client, string AsmtId, string vtype, string empid, string LocautoId)
        {

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
                //ViewBag.customer = util.PopulateDropDown("exec udpMstSale_Client_Get @LocationAutoID ='" + branchid + "'", util.strElect);
                ViewBag.customer = CustomerDropDown();

                ViewBag.Region = RegionDropDown();
            }



            return View();
        }

        public IActionResult ConsolidatedVisittoReport()
        {

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


        public ResponseMessage GetConsolidatedVisitReport(string todate, string fromdate, string clientcode, string Regid)
        {

            DataTable dt = new DataTable();
            var branchid = HttpContext.Session.GetString("branchid");
            var ds = util.Fill("exec udp_GetCustomerFeedback2 @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@ToDate='" + todate + "',@ClientCode='" + clientcode + "', @Region='" + Regid + "',@UserId='" + UserId + "' ", util.strElect);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }



        [HttpPost]
        public ResponseMessage GetSearchPhotoAttendance(string todate, string Region, string Empno, string client, string shift, string fromdate)
        {

            var UserName = HttpContext.Session.GetString("UserName");


            var ds = util.Fill("exec udp_GetEmployeePhotoAttendanceGroupL2 @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@CompanyCode='" + companyid + "',@Region='" + Region + "',@ClientCode='" + client + "',@Shift='" + shift + "',@ToDate='" + todate + "' ,@UserId='" + UserName + "'", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }

        [HttpPost]

        public ResponseMessage GetSearchAttendanceSuper(string date, string site, string Empno, string client, string shift, string fromdate)
        {




            var ds = util.Fill("exec udp_GetEmployeeSuperAttendanceGroupL @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@AsmtCode='" + site + "',@ClientCode='" + client + "',@ShiftCode='" + shift + "',@ToDate='" + date + "' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }

        [HttpPost]

        public JsonResult GetSearchAttendanceSuperwithoutImage(string date, string site, string Empno, string client, string shift, string fromdate)
        {



            var UserName = HttpContext.Session.GetString("UserName");

            string div = "";
            DataTable dt = new DataTable();


            var ds = util.Fill("exec udp_GetEmployeeSuperAttendanceGroupL_WithoutImage @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@AsmtCode='" + site + "',@ClientCode='" + client + "',@ShiftCode='" + shift + "',@ToDate='" + date + "',@UserId='" + UserName + "' ", util.strElect);
            dt = ds.Tables[0];

            return Json(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]

        public JsonResult bindSite(string siteid)
        {


            DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @LocationAutoID ='" + branchid + "',@BaseCompanyCode ='" + companyid + "',@ClientCode='" + siteid + "' ", util.strElect);
            DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }


        [HttpPost]

        public JsonResult bindclient(string custid)
        {



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

            DataSet ds = util.Fill("exec udpGetVisitType @LocationAutoID ='" + branchid + "',@ClientCode ='" + custid + "'", util.strElect);
            DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }


        [HttpPost]
        public JsonResult GetVisitReport(string date, string custid, string client, string visittype, string EmpId)
        {




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

            ViewBag.Region = RegionDropDown();

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
                ViewBag.client = CustomerDropDown();

            }


            return View();
        }
        public JsonResult BindBranchtoReg(string regid)
        {


            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("LocationAutoID", typeof(string));
                dt.Columns.Add("LocationCode", typeof(string));

                dt.Rows.Add("20909", "Lucknow");

                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }
            else
            {

                DataSet ds = util.Fill("exec Usp_GroupLNewAppDLL 'GetBranchFromRegion', @Id2 ='" + companyid + "',@Id3 ='" + regid + "',@Id='" + UserId + "'", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);
                return Json(data);
            }
        }
        public JsonResult BindSitetoClient(string clietnid)
        {

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

            DataSet ds = util.Fill("exec udp_GetShortageReportGrouplNewApp2 @ComanyCode ='" + companyid + "',@DDate ='" + date + "', @Region='" + Regionid + "', @Branch='" + branchid + "',@employeetype='" + emptype + "',@ClientCode='" + clientcode + "',@SiteCode='" + sitecode + "',@UserId='" + UserId + "'", util.strElect);
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
                ViewBag.client = CustomerDropDown();
                ViewBag.Region = RegionDropDown();
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }
            return View();
        }

        public JsonResult GetSearchShortageReportclientWise(string date, string Regionid, string Clientid, string shiftid)
        {
            var companyid = HttpContext.Session.GetString("companyid");
            DataSet ds = util.Fill("exec udp_ShortageReportClientWise5 @CompanyCode ='" + companyid + "',@Date ='" + date + "', @Region='" + Regionid + "',@ClientCode='" + Clientid + "',@Shift='" + shiftid + "',@UserId='" + UserId + "'", util.strElect);

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


            ViewBag.Region = RegionDropDown();

            return View();
        }

        [HttpPost]
        public JsonResult bindclienttobranch(string branchid)
        {


            if (branchid == "20909")
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
                DataSet ds = util.Fill("exec [Usp_GroupLNewAppDLL] 'GetClientFromBranch', @Id2 ='" + branchid + "',@Id3 ='" + companyid + "',@Id='" + UserId + "' ", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }

        }

        public ResponseMessage SearchGetOutOfRange(string fromdate, string todate, string Region, string Branch, string client, string site)
        {

            string div = "";
            DataTable dt = new DataTable();
            var ds = util.Fill("exec udp_GetOutOfRangeReportGrouplNewApp2 @ComanyCode ='" + companyid + "',@DDate='" + fromdate + "',@Region='" + Region + "',@Branch='" + Branch + "',@ClientCode='" + client + "',@SiteCode='" + site + "',@ToDate='" + todate + "',@UserId='" + UserId + "' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }


        public JsonResult bindSiteToOutrange(string siteid)
        {

            DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @LocationAutoID ='All',@BaseCompanyCode ='" + companyid + "',@ClientCode='" + siteid + "' ", util.strElect);
            DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }








        #endregion
        #region AuditStatusReport Report

        public IActionResult AuditStatusReport()
        {

            var branchid = HttpContext.Session.GetString("branchid");
            ViewBag.customer = CustomerDropDown();

            return View();
        }

        public ResponseMessage GetAudioStatusReport(string todate, string fromdate, string clientcode, string Regid)
        {






            var ds = util.Fill("exec udp_GetAuditReportWithFilters2 @FromDate='" + fromdate + "',@ToDate='" + todate + "', @LocationAutoId ='" + branchid + "',@ClientCode='" + clientcode + "',@Region='All' ,@UserId='" + UserId + "' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }


        #endregion

        public ResponseMessage GetAppointmentLetterAcceptance(string CompanyCodeid)
        {

            var ds = util.Fill("udp_GetAppointmentLetterAccepted2 @CompanyCode ='" + CompanyCodeid + "',@UserId='" + UserId + "' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
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
            ViewBag.Company = util.PopulateDropDown("Usp_GroupLNewAppDLL 'CompanyLogin',@Id='" + UserId + "'", util.strElect); ;
            return View();
        }

        #region Leave Date Report

        public IActionResult LeaveDate()
        {
            return View();
        }

        public ResponseMessage GetLeaveDateData(string Year, string Month)
        {
            var ds = util.Fill("exec udp_GetGroupLLeaveData @Year ='" + Year + "',@Month='" + Month + "' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }
        [HttpPost]
        public JsonResult DeleteAndstatusUpdate(string type, int AutoId, int status)
        {
            string Message = string.Empty;
            if (type == "StatusUpdate")
            {
                string Mes = util.execQuery("Update GroupLNewAppLeaveMaster set ApprovalStatus='" + status + "'  where AutoId='" + AutoId + "'", util.strElect);
                if (Mes == "Successfull")
                {
                    Message = "Leave Record Status Updated Successfully";
                }
                else
                {
                    Message = "Error in Updating Leave Record Status";
                }

            }
            else if (type == "Delete")
            {
                string Mes = util.execQuery("Delete from GroupLNewAppLeaveMaster where AutoId='" + AutoId + "'", util.strElect);

                if (Mes == "Successfull")
                {
                    Message = "Leave Record Deleted Successfully";
                }
                else
                {
                    Message = "Error in Deleting Leave Record";
                }


            }
            return Json(JsonConvert.SerializeObject(new { Message = Message }));
        }


        #endregion


        #region Regularization Report

        public IActionResult Regularization()
        {
            return View();
        }

        public ResponseMessage GetRegularization(string Year, string Month)
        {
            var ds = util.Fill("exec udp_GetGroupLRegualrizationData @Year ='" + Year + "',@Month='" + Month + "' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }
        [HttpPost]
        public JsonResult RegularizationDeleteAndstatusUpdate(string type, int AutoId, int status)
        {
            string Message = string.Empty;
            if (type == "StatusUpdate")
            {
                string Mes = "";
                if (status == 1)
                {
                    Mes = util.execQuery("Update GroupLNewAppAttendanceRegularization set ApprovalStatus='" + status + "'  ,ApprovedByID='" + HttpContext.Session.GetString("UserName") + "',ApprovedByName='" + HttpContext.Session.GetString("UserName") + "',ApprovalDate=getdate() where AutoId='" + AutoId + "'", util.strElect);
                }
                else
                {
                    Mes = util.execQuery("Update GroupLNewAppAttendanceRegularization set ApprovalStatus='" + status + "'  ,ApprovedByID='',ApprovedByName='',ApprovalDate=NULL where AutoId='" + AutoId + "'", util.strElect);
                }

                if (Mes == "Successfull")
                {
                    Message = "Regularization  Record Status Updated Successfully";
                }
                else
                {
                    Message = "Error in Updating Regularization  Record Status";
                }

            }
            else if (type == "Delete")
            {
                string Mes = util.execQuery("Delete from GroupLNewAppAttendanceRegularization where AutoId='" + AutoId + "'", util.strElect);

                if (Mes == "Successfull")
                {
                    Message = "Regularization  Record Deleted Successfully";
                }
                else
                {
                    Message = "Error in Deleting Regularization  Record";
                }


            }
            return Json(JsonConvert.SerializeObject(new { Message = Message }));
        }

        #endregion

        #region MonthlyOutDuty
        public IActionResult MonthlyOutDuty()
        {
            return View();
        }

        public ResponseMessage GetMonthlyOutDuty(string Year, string Month)
        {
            var ds = util.Fill("exec udp_GetGroupLOutDutyData @Year ='" + Year + "',@Month='" + Month + "' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }


        [HttpPost]
        public JsonResult MonthlyOutDutyDeleteAndstatusUpdate(string type, int AutoId, int status)
        {
            string Message = string.Empty;
            if (type == "StatusUpdate")
            {
                string Mes = "";
                if (status == 1)
                {
                    Mes = util.execQuery("Update GroupLNewAppOutDuty set ApprovalStatus='" + status + "'  ,ApprovedByID='" + HttpContext.Session.GetString("UserName") + "',ApprovedByName='" + HttpContext.Session.GetString("UserName") + "',ApprovalDate=getdate() where AutoId='" + AutoId + "'", util.strElect);
                }
                else
                {
                    Mes = util.execQuery("Update GroupLNewAppOutDuty set ApprovalStatus='" + status + "'  ,ApprovedByID='',ApprovedByName='',ApprovalDate=NULL where AutoId='" + AutoId + "'", util.strElect);
                }

                if (Mes == "Successfull")
                {
                    Message = "Monthly Out Duty Status Updated Successfully";
                }
                else
                {
                    Message = "Error in Updating Monthly Out Duty Status";
                }

            }
            else if (type == "Delete")
            {
                string Mes = util.execQuery("Delete from GroupLNewAppOutDuty where AutoId='" + AutoId + "'", util.strElect);

                if (Mes == "Successfull")
                {
                    Message = "Monthly Out Duty Deleted Successfully";
                }
                else
                {
                    Message = "Error in Deleting Monthly Out Duty";
                }


            }
            return Json(JsonConvert.SerializeObject(new { Message = Message }));
        }


        #endregion

        public List<SelectListItem> CustomerDropDown()
        {
            var companyid = HttpContext.Session.GetString("companyid");
            return util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap',@Id2='" + companyid + "', @Id = '" + HttpContext.Session.GetString("UserName") + "'", util.strElect);

        }
        public List<SelectListItem> RegionDropDown()
        {

            return util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'RegionMap', @Id = '" + HttpContext.Session.GetString("UserName") + "',@Id2='" + HttpContext.Session.GetString("companyid") + "'", util.strElect);

        }

        public IActionResult PendingVisitReport()
        {

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
                ViewBag.customer = CustomerDropDown();

                ViewBag.Region = RegionDropDown();
            }
            return View();
        }


        public ResponseMessage GetPendingVisitReport(string ClientCode, string Region, string Duration)
        {

            var ds = util.Fill(@$"exec udp_GetCustomerFeedbackUnDoneReports2 @LocationAutoID='{branchid}',@ClientCode='{ClientCode}',@Region='{Region}',@Duration='{Duration}',@UserId='{UserId}' ", util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;

        }

        public class Attendance
        {
            public string EmpCode { get; set; }
            public string Name { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public Dictionary<int, string> Days { get; set; } = new Dictionary<int, string>();
        }

        #region AttendanceMuster

        public IActionResult AttendanceMuster()
        {



            return View();
        }

        public ResponseMessage GetAttendanceMuster(int Year, int Month)
        {

            var ds = util.Fill(@$"exec udp_GetAttendanceMusterWithShiftGroupLWithoutclient2_1 @LocationAutoId='{branchid}',@Year='{Year}',@Month='{Month}' ", util.strElect);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }

        #endregion

        #region GroupLMuster

        public IActionResult GroupLMuster()
        {



            return View();
        }

        public ResponseMessage GetGroupLMuster(int Year, int Month)
        {

            var ds = util.Fill(@$"exec udp_GetAttendanceMusterWithShiftGroupLWithoutclient2 @LocationAutoId='{branchid}',@Year='{Year}',@Month='{Month}' ", util.strElect);



            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }

        #endregion

        #region AttendanceMusterClientWise
        public IActionResult AttendanceMusterClientWise()
        {
            ViewBag.Client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap',  @Id = '" + HttpContext.Session.GetString("UserName") + "',@id2='" + companyid + "'", util.strElect);


            return View();
        }

        public ResponseMessage GetAttendanceMusterClientWise(int Year, int Month, string Client, string Site)
        {

            var ds = util.Fill(@$"exec udp_GetAttendanceMusterWithShiftGroupL @LocationAutoId='{branchid}',@Year='{Year}',@Month='{Month}',@ClientCode='{Client}',@AsmtID='{Site}' ", util.strElect);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }

        #endregion

        #region Materialreport
        public IActionResult Materialreport()
        {
            return View();
        }

        public ResponseMessage GetAllMaterialreport(string Month, string Year)
        {

            string query = "Usp_GroupLNewAppMaterialReport @UserId='" + UserId + "',@CompanyCode='" + companyid + "',@Month='" + Month + "',@Year='" + Year + "'";
            DataSet ds = util.Fill(query, util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }
        #endregion


        public IActionResult GetReports(string Id)
        {


            string[] Pramsname = { "From date", "To date", "Company", "Region", "Branch", "Client", "Site", "Shift" };
            ViewBag.Pramsname = Pramsname;

            ViewBag.ReportName = Id;
            ViewBag.Type = Id == "Resignation Dashboard" ? "1" : Id == "Site Details Report" ? "2" : Id == "My Task Report" ? "3" : Id == "Resignation Details HR" ? "4" : Id == "Resignation Acceptance Report" ? "5" : Id == "Consolidated My Task Report" ? "6" : "";
            ViewBag.Company = util.PopulateDropDown("Usp_GroupLNewAppDLL 'CompanyLogin',@Id='" + UserId + "'", util.strElect);
            ViewBag.customer = CustomerDropDown();
            ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            ViewBag.ChecklistClient = util.PopulateDropDown("exec udp_GetClientListGroupL @LocationAutoId='" + branchid + "'", util.strElect);

            return View();
        }



        public ResponseMessage GetReport(string Type, string ClientCode, string Site, string fromdate, string ChecklistClient, string ChecklistSite, string Status, string todate, string TourId)
        {

            var region = HttpContext.Session.GetString("locationid");
            string query = @$"Usp_GetGroupLReports @Type='{Type}',@UserId='{UserId}',@CompanyCode='{companyid}',@LocationAutoId='{branchid}',@Region='{region}',@CleintCode='{ClientCode}',@Site='{Site}',@FDate='{fromdate}',@Status='{Status}',
@id1='{ChecklistClient}',@id2='{ChecklistSite}',@TDate='{todate}',@TourId='{TourId}'
";
            DataSet ds = util.Fill(query, util.strElect);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }

        public ResponseMessage myTask(string Type, string ClientCode, string Site, string fromdate, string ChecklistClient, string ChecklistSite, string Status, string todate, string TourId, string HeaderName)
        {

            var region = HttpContext.Session.GetString("locationid");
            string query = @$"Usp_GetGroupLReports @Type='{Type}',@UserId='{UserId}',@CompanyCode='{companyid}',@LocationAutoId='{branchid}',@Region='{region}',@CleintCode='{ClientCode}',@Site='{Site}',@FDate='{fromdate}',@Status='{Status}',
@id1='{HeaderName}',@id2='{ChecklistSite}',@TDate='{todate}',@TourId='{TourId}'
";
            DataSet ds = util.Fill(query, util.strElect);

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                var data = new 
                {
                  dt=  JsonConvert.SerializeObject(ds.Tables[0]),
                  dt2=  JsonConvert.SerializeObject(ds.Tables[1]),

                };
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = data;
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;
        }


        public ResponseMessage getClientListSite(string Id)
        {

            string query = $"udp_GetSiteListGroupLNew  @ClientCode='{Id}' ,@BaseCompanyCode='{companyid}',@LocationAutoId='{branchid}'";
            DataSet ds = util.Fill(query, util.strElect);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;


        }

        public ResponseMessage ResignationAccptreject(string type, string Id)

        {
            string query;
            if (type.Trim().ToLower() == "accept")
            {
                query = $"udp_AcceptResignationHR  @Id='{Id}' ";
            }
            else
            {
                query = $"udp_RejectResignationHR  @Id='{Id}'";
            }

            DataSet ds = util.Fill(query, util.strElect);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Get List";
                _response.Data = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Record Not Found !!";
                _response.Data = null;
            }

            return _response;

        }

        
    }
}


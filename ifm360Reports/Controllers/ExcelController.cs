using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace ifm360Reports.Controllers
{
    public class ExcelController : Controller
    {
        public IActionResult DownloadAttendance()
        {
            using var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Attendance");

            ws.Cell(1, 1).Value = "Emp Code";
            ws.Cell(1, 2).Value = "Name";
            ws.Cell(1, 3).Value = "Designation";
            ws.Cell(1, 4).Value = "Department";
            ws.Cell(1, 5).Value = "Location";

            for (int day = 1; day <= 31; day++)
            {
                var c = ws.Cell(1, 5 + day);
                c.Value = new DateTime(2025, 8, day).ToString("dd-MMM");
                c.Style.Alignment.TextRotation = 90;
                c.Style.Fill.BackgroundColor = XLColor.Red;
                c.Style.Font.FontColor = XLColor.White;
                c.Style.Font.Bold = true;
                c.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                c.Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
            }

            var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;

            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(stream, contentType, "Attendance-Aug-2025.xlsx");
        }
    }
}


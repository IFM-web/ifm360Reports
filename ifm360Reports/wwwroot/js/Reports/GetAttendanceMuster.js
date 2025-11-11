$(document).ready(() => {

    let date = new Date().getMonth() + 1;
    if (date < 10) {
        date = '0' + date
    }
    $("#month").val(date);

});

const months = [
    "", "Jan", "Feb", "Mar", "Apr", "May", "Jun",
    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
];

const DaysName = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursady', 'Friday', 'Saturday'];
function SearchData() {
    $(".preloader").show();
    let month = $("#month").val()
    let Year = $("#Year").val();
    $.ajax({

        url: localStorage.getItem('Myurl') + '/Reports/GetAttendanceMuster',
        type: 'get',
        data: {

            Year: Year,
            Month: month,


        },
        success: function (data) {
            var data = JSON.parse(data);
            console.log(data);

            $(".preloader").hide();
            if (data != '[]') {
                $("#prindDiv").removeClass('d-none');

                $(".companybody").empty();
                $("#Headertb").empty();

                let header1 = `<tr></th><th></th><th></th><th></th>`;
                let header = `<tr><th>SNo</th><th>EmpCode</th><th>EmpName</th>`;
                for (let i = 1; i <= 31; i++) {
                    let tempmonth;
                    if (month < 10) {
                        tempmonth = month.replace('0', '');
                    }
                    else {
                        tempmonth = month;

                    }
                    let date = new Date(`${Year}-${tempmonth}-${i}`);

                    header1 += `<th class='days'><div>${DaysName[date.getDay()]}</div> </th>`
                    header += `<th class='days'><div>${i}-${months[tempmonth]}-${Year}</div> </th>`


                }
                header += `<th class='days' style="background-color:#1F4E78"><div>Duty Days</div> </th>`
                $("#Headertb").append(header1 + `<th class='days' style="background-color:#1F4E78"></th>` + '</tr>');
                $("#Headertb").append(header + '</tr>');

                var rowlen = parseInt($('.companybody tr').length);
                // console.log(data)
                var row = '';
                var row2 = '';

                for (var i = 0; i < data.length; i++) {
                    row += `<tr>
                    <td>${data[i].SNo}</td>
                  
                  <td>${data[i].EmpCode}</td>
                    <td>${data[i].EmpName}</td>
                    `
                    $(".preloader").hide();

                    for (let j = 1; j <= 31; j++) {
                        if (j < 10) {
                            j = '0' + j;
                        }
                        let val = data[i][j];
                        row += `<td style="background-color:${val == 'A' ? '#F44336' : val == 'P' ? '#4CAF50' : val == 'L' ? '#FFCDD2' : val == 'HD' ? '#AA73FF' : val

                            == 'R' ? '#FFBE00' : val == 'H' ? '#00BCD4' : val == 'OD' ? '#2196F3' : val == 'POD' ? '#9c27b0' : val == 'PR' ? '#fd7e14' : val == 'PL' ? '#d3e605' : ''}">${val}</td>`;
                    }
                    row += `  <td>${data[i].DutyDays || 0}</td>`;
                    row += "</tr>"



                }

                $(".companybody").append(row);




                colochange();

            }
            else {
                $(".preloader").hide();
                $(".companybody").empty();
                $(".companybody2").empty();
                alert('Record Not Available');
            }


        },
        error: function (data) {
            $(".preloader").hide();
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}

function colochange() {
    const table = document.getElementById("attendance");
    for (let row of table.rows) {
        for (let cell of row.cells) {
            if (cell.textContent.includes("Sunday")) {
                let colIndex = cell.cellIndex;
                for (let r of table.rows) {
                    r.cells[colIndex].style.backgroundColor = "green";
                }
            }
        }
    }
}


async function exportExcel() {
    const workbook = new ExcelJS.Workbook();
    const ws = workbook.addWorksheet("Attendance");
    let month = $("#month option:selected").text()
    const table = document.getElementById("attendance");
    let sundayColumns = [];


    for (let r = 0; r < table.rows.length; r++) {
        const row = ws.addRow(
            Array.from(table.rows[r].cells).map(c => c.innerText)
        );

        row.eachCell((cell, colNumber) => {

            cell.border = {
                top: { style: "thin" },
                bottom: { style: "thin" },
                left: { style: "thin" },
                right: { style: "thin" },
            };

            if (r === 0 || r === 1) {
                
                const val = cell.value;
                if (val === "Sunday") {
                    sundayColumns.push(colNumber); 
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FF006400" }, 
                    };
                }
                else if (val === '' || val === "SNo" || val === "EmpCode" || val === "EmpName" || val === "DutyDays") {

                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FF1F4E78" },
                    };

                }

                else {
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FFFF0000" },
                    };
                }

                cell.font = { bold: true, color: { argb: "FFFFFFFF" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
                if (colNumber > 3) cell.alignment.textRotation = 90;
            } else {
                const val = cell.value;
                if (val === "P")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FF4CAF50" },
                    };
                if (val === "A")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FFF44336" },
                    };
                if (val === "L")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FFFFCDD2" },
                    };
                if (val === "H")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FF00BCD4" },
                    };
                if (val === "OD")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FF2196F3" },
                    };
                if (val === "HD")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FFAA73FF" },
                    };
                if (val === "PR")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FFFF7E14" },
                    };
                if (val === "POD")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FF9C27B0" },
                    };
                if (val === "PL")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FFD3E605" },
                    };
                if (val === "R")
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "FFFFBE00" },
                    };
            }
        });
    }


    sundayColumns.forEach((colNumber) => {
        ws.getColumn(colNumber).eachCell((cell) => {
            cell.fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "FF006400" },
            };
            cell.font = { bold: true, color: { argb: "FFFFFFFF" } };


        });
    });




    const buf = await workbook.xlsx.writeBuffer();
    saveAs(new Blob([buf]), `Attendance-Muster-${month}-2025.xlsx`);
}


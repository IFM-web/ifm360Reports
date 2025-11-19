
$(document).ready(function () {
    loadGridData();
})
let myurl = localStorage.getItem('Myurl');
function exportexcel(type, fn, dl) {
    var ddd = $("#txtpagename").val();
    var elt = document.getElementById('data-table');
    filename = ddd;
    console.log(filename);
    console.log(elt);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}

document.getElementById("pdfid").addEventListener("click", () => {
    var companyName = $("#CompanyCodeid :selected").text();
    var content = `
<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AppointmentLetterAcceptanceReport ${companyName}</title>

    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        @media print {
            table {
                border-collapse: collapse;
                width: 100%;
                margin-top: 0;
            }
        }
        th,
        td {
            border: 1px solid black;
            padding: 8px;
            text-align: left;
        }

        #emp_name {
            width: 80px;
            display: block;
        }

        #dateField {
            width: 80px;
            display: inline-block;
        }

        @media print {
           
             border:2px solid black;
           
            body {
                -webkit-print-color-adjust: exact;
               
                  page-break-after: always;

                  
            
            background-color: none;
            padding: 20px;
           
            margin: 20px;
             height:95vh;
                
               
            }

            

            .no-print {
                display: none;
            }

            @page {
                size: A4 landscape;
                margin: 6.20mm 0 6.20mm 0;             
                -webkit-print-color-adjust: exact;
              font-family: 'Times New Roman', Times, serif, sans-serif;
                  page-break-after: always;
                 
                header,
                footer {
                    display: none;
                }
          


            }

            header,
            footer {
                display: none;
            }

        }
       

        body {
             -webkit-print-color-adjust: exact;
            font-family: 'Times New Roman', Times, serif, sans-serif;
            background-color: none;
            padding: 20px;
            /*  border:1.2px solid black;*/
            margin: 20px;         
                  height:95vh;
           
        }

        .container {
            background-color: white;
            /* padding: 20px; */
            max-width: 3508px;
            margin: auto;



        }

        .header1 {
            text-align: center;
            margin-bottom: 10px;
            margin: auto;

        }

        .logo {
            max-width: 250px;
            
        }

        h1 {
            font-size: 24px;
            margin-bottom: 10px;
        }

        section {
            margin-bottom: 20px;
        }

        .job-details-row,
        .client-details-row {
            border: 1px solid #000;
            padding: 10px;
            margin-bottom: 10px;
        }

        section.customer-info {
            display: flex;
            justify-content: space-between;
        }

        .job-details-row,
        .client-details-rows {
            display: flex;
            justify-content: space-between;
            flex-wrap: wrap;
        }

        .checkpoints table {
            width: 100%;
            border-collapse: collapse;
        }

        .checkpoints th,
        .checkpoints td {
            border: 1px solid #000;
            padding: 8px;
            text-align: center;
        }

        .checkpoints img {
            max-width: 100px;
            height: auto;
        }

        .signature-box {
            width: 48%;
            display: inline-block;
            text-align: center;
            margin-top: 20px;
        }

        .header1 h1 {
            border: 1px solid;
            font-size: 20px;
            margin-top: 20px;
            padding: 5px 0px;
        }

        .customer-details p {
            text-align: left;
        }

        .site-details p {
            text-align: end;
        }

        #data-table {
            #thead {
                background-color: #c84143;
            }

            th {
                color: white;

            }
        }

        .job-details h2,
        .client-details h2,
        .checkpoints h2 {
            font-size: 20px;
            padding-bottom: 10px;
            padding-top: 20px;
        }

        @media (max-width: 768px) {

            .customer-info .customer-details,
            .customer-info .site-details {
                width: 100%;
                text-align: center;
            }

            .signature-box {
                width: 100%;
                margin-top: 10px;
            }

            .checkpoints img {
                max-width: 70px;
            }
        }
    </style>
</head>

`


    var headerContent = `<body>
    <div id="pagecontainer">
        <div class="container" id="content">
            <div class="header1">
                 <img src="https://ifm360.in/grouplreportingportal/GroupL.jfif" alt="Group L Logo" class="logo">
                <p style="margin-top:-20px;">3rd Floor, w31, Okhla Industrial Area Phase 2 <br /> New Delhi 110020 </p>
                <h1 style='text-align: center;' id="myhid">AppointmentLetterAcceptanceReport ${companyName} </h1>
            </div>

        </div>
          </div>
        </body>

</html>

`;


    var datadiv = document.getElementById("tableid").innerHTML;

    var content1 = content + headerContent;

    var datadiv = document.getElementById("tableid");
    const hideFrame = document.createElement("iframe");
    
    var popupwin = window.open();

    popupwin.document.write(content1 + datadiv.innerHTML);
    popupwin.document.close();
    var logoImage = popupwin.document.getElementById("logoimag");
    popupwin.onload = function () {
        popupwin.focus();
        popupwin.print();
        popupwin.close();
    }
});
function loadGridData()
{
    $(".preloader").show();
    var CompanyCodeid = $("#CompanyCodeid").val();


    $.ajax({
        url: myurl + '/Reports/GetAppointmentLetterAcceptance',
        type: 'GET',
        data: {
            CompanyCodeid: CompanyCodeid
        },
        success: function (data) {
            $(".preloader").hide();
            $('#data-table tbody').empty();
            if (data.statusCode ==200) {
                var sno = 1;
                var Data = JSON.parse(data.data);
                for (var i = 0; i < Data.length; i++) {
                    var row = '<tr>';
                    row += '<td>' + sno++ + '</td>';
                    row += '<td>' + Data[i].CompanyDesc + '</td>';
                    row += '<td>' + Data[i].Region + '</td>';
                    row += '<td>' + Data[i].Branch + '</td>';
                    row += '<td>' + Data[i].EmployeeCode + '</td>';
                    row += '<td>' + Data[i].EmployeeName + '</td>';
                    row += '<td>' + Data[i].ApprovalDate + '</td>';
                    row += '</tr>';
                    $('#data-table tbody').append(row);
                }
            } else {
                $(".preloader").hide();
                $('#data-table tbody').append('<tr><td colspan="7" class="text-center">No data available.</td></tr>');
            }
        },
        error: function (xhr, status, error) {
            $(".preloader").hide();
            console.log('Error loading data:', error);
            $('#data-table tbody').html('<tr><td colspan="7" class="text-center">Error fetching data.</td></tr>');
        }
    });
}
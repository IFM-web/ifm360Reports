/// <reference path="../xlxs.full.min.js" />
$(document).ready(function () {
    document.getElementById('txttodate').value = new Date().toISOString().substring(0, 10);
    document.getElementById('txtfromdate').value = new Date().toISOString().substring(0, 10);
})


let myurl = localStorage.getItem('Myurl');
var contentdata = '';
function SearchData() {
    $(".preloader").show();
    
    
    $.ajax({
       
       url: myurl+'/Reports/GetSearchPhotoAttendance',
        type: 'post',
        data: {
            todate: $("#txttodate").val(),
            fromdate: $("#txtfromdate").val(),
            Region: $("#region").val(),
            Empno: $("#txtemployeeno").val(),
            client: $("#client").val(),
            shift: $("#ddlshift").val(),
        },
        success: function (data) {
            $(".preloader").hide();
            if (data != '[]') {


                $(".companybody").empty();
                var data = JSON.parse(data);
                console.log(data)
                var rowlen = parseInt($('.companybody tr').length);
                // console.log(data)
                var row = '';
                for (var i = 0; i < data.length; i++) {
                    var url = 'data: image/jpeg;base64,' + data[i].EmployeeImage

                    row += "<tr id='row" + i + "'><td style=''>" + parseInt(i + 1) + "</td><td  style='' class='Region'>" + data[i].Region + "</td><td  style='' class='itemn'>" + data[i].ClientName + "</td><td style=''><span class='EmployeeNumber'>" + data[i].EmployeeNumber + "</span></td><td style=''><span class='EmployeeName' id='emp_name'>" + data[i].EmployeeName + "</span></td><td style=''><span class='ShiftCode'>" + data[i].ShiftCode + "</span></td><td style=''><span class='Status'>" + data[i].Status + "</span></td><td style=''><span class='Date' id='dateField' >" + data[i].Date + "</span></td><td style=''><span class='Time'>" + data[i].Time + "</span></td><td style=''><span class='Latitude'>" + data[i].Latitude + "</span></td><td style=''><span class='Longitude'>" + data[i].Longitude + "</span></td><td style=''><span class='LocationName'>" + data[i].LocationName + "</span></td><td style=''><img  data-toggle='modal' data-target='#myModal' onclick='image(\"" + url +"\")' style='height: 100px; width: 100px;'src= '" + url + "' class='EmployeeImage'></></td></tr>";


                }

                $(".companybody").prepend(row);




            }
            else {
                $(".preloader").hide();
                $(".companybody").empty();
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


function exportexcel(type, fn, dl) {
    var data = $(".companybody").val()


    var clientname = $("#client :selected").text();
    var ddd = $("#txtpagename").val();
    var elt = document.getElementById('data-tablehid');
    filename = ddd + clientname;
    console.log(filename);
    console.log(elt);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });


    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}



// XLSX.writeFile(wb, fn || (ddd+ (type+ '.' || 'xlsx')));




//document.getElementById('Genratepdf').addEventListener('click', () => {

//    $.ajax({
//        url: '/Reports/DownloadPdf',
//        type: 'post',
//        data: {
//            date: $("#txtdate").val(),
//            Region: $("#region").val(),
//            Empno: $("#txtemployeeno").val(),
//            client: $("#client").val(),
//            shift: $("#ddlshift").val(),
//        },
//        success: function () {

//        },
//        error: function () {

//        }
//    });


//});
var ddltext = $("#txtdate").val();

var x = document.getElementById("txttodate").value;

var details = '<h1>' + x + '</h1>'
var button = document.getElementById("Genratepdf");
var makepdf = document.getElementsByClassName("prindDiv");


var Startdate = $("#txtdate").val()






//button.addEventListener("click", function () {
//    var mywindow = window.open("", "PRINT",
//        "height=400,width=1000");
//    var he = 'Photo Atandance Report';
//    var newhtm = he + makepdf;
//    mywindow.document.write(newhtm.innerHTML);
//    mywindow.document.close();
//    mywindow.focus();
//    mywindow.print();
//    mywindow.close();
//    return true;
//});








document.getElementById("Genratepdf").addEventListener("click", () => {
    var companyName = $("#client :selected").text();
    var selectedDate = document.getElementById("txttodate").value;
    //  var headerContent = `
    //   <section class="customer-info">
    //<div class="customer-details"><p>Client Name: <strong> ${companyName}</strong></p>
    //  </div><div class="site-details">Date: <strong> ${selectedDate}</strong><p></p></div>
    //  </section><br/>

    // `
    var content = `
<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Photo Attendance Report of ${companyName}</title>

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
                <h1 style='text-align: center;' id="myhid">Photo Attendance Report Of ${companyName} </h1>
            </div>

        </div>
          </div>
        </body>

</html>

`;


    var datadiv = document.getElementById("prindDiv").innerHTML;

    var content1 = content + headerContent;

    var datadiv = document.getElementById("prindDiv");
    const hideFrame = document.createElement("iframe");
    //var popupwin = window.open('', '_blank', 'width=200', 'height=200', 'left:100', 'top:100');

    //popupwin.document.open();
    //popupwin.document.write(content1 + datadiv.innerHTML);
    //popupwin.document.close();
  
    //popupwin.focus();
    //popupwin.print();
    //popupwin.close();
    // return true;
    var popupwin = window.open();

    popupwin.document.write(content1 + datadiv.innerHTML);
    popupwin.document.close();
    var logoImage = popupwin.document.getElementById("logoimag");
    popupwin.onload = function () {
        popupwin.focus();
        popupwin.print();
        popupwin.close();
    }
    //const { jsPDF } = window.jspdf;
    //const doc = new jsPDF();

    //const table = document.getElementById("dynamicTable");

    //doc.autoTable({ html: datadiv });

    //doc.save("table_data.pdf");

});

function image(id) {
    $("#img01").attr("src", id);
    //$(".modal-body").css("backgroundImage", "url(" + id + ")"); 


}

var angle = 0; 

function rotateImage() {
        angle += 90;    
    var image = document.getElementById('img01');
    image.style.transform = 'rotate(' + angle + 'deg)';
}
function resetImageRotation() {
    angle = 0; 
    var image = document.getElementById('img01');
    image.style.transform = 'rotate(0deg)';
}
$(document).ready(() => {
    document.getElementById('fromdate').value = new Date().toISOString().substring(0, 10);
    GetClientbyTask()
  
})

let myurl = localStorage.getItem('Myurl');
function GetClientbyTask() {
    let Date = $('#fromdate').val();
    $.ajax({

        url: myurl + '/Dropdown/GetClientbyTask',
        type: 'post',
        data: { Date },
        success: function (data) {
            var dropdown = $('#Client');
            dropdown.empty();
            if (data.statusCode == 200) {

                //dropdown.append($('<option></option>').attr('value', 'All').text('All'));
                for (var i = 0; i < data.data.length; i++) {

                    dropdown.append($('<option></option>').attr('value', data.data[i].value).text(data.data[i].text));
                }
                getClientListSite();
                ChecklistHeader()
                GetTour();

            }
            else {
                ChecklistHeader()
                getClientListSite();
                $('#TourId').empty();
                $("#dataclient").addClass('d-none');
                $("#Printdiv").empty();
                alert('No tasks for the selected date');
            }
        },
        error: function (error) {
            console.log(error.massage);
        }
    })
}

function GetTour() {
    let Id = $('#Client').val();

    $.ajax({

        url: myurl + '/Admin/GetTour',
        type: 'get',
        data: { Id: Id, },
        success: function (data) {
            var dropdown = $('#TourId');

            dropdown.empty();
            if (data.length > 0) {
                var data = JSON.parse(data);


               
                for (var i = 0; i < data.length; i++) {

                    dropdown.append($('<option></option>').attr('value', data[i].TourAutoID).text(data[i].TourDesc));
                }
            }
            else {
                dropdown.empty();
    
            }

        },
        error: function (error) {
            console.log(error.massage);
        }
    })
}

function getClientListSite() {
  
    $.ajax({

        url: myurl + '/Dropdown/GetSitebyTask',
        type: 'post',
        data: { Id: $("#Client").val(), Date : $('#fromdate').val(), },
        success: function (data) {
            if (data.statusCode == 200) {
                var data = data.data;

                var dropdown = $('#Site');

                dropdown.empty();
                //dropdown.append($('<option></option>').attr('value', 'All').text('All'));
                for (var i = 0; i < data.length; i++) {

                    dropdown.append($('<option></option>').attr('value', data[i].value).text(data[i].text));
                }
            }
        },
        error: function (error) {
            console.log(error.massage);
        }
    })
}

function ChecklistHeader() {
  
    $.ajax({

        url: myurl + '/Dropdown/ChecklistHeader',
        type: 'get',
        data: { Id: $("#Client").val() },
        success: function (data) {
            if (data.statusCode == 200) {
                var data = data.data;

                var dropdown = $('#ChecklistHeader');

                dropdown.empty();
                //dropdown.append($('<option></option>').attr('value', 'All').text('All'));
                for (var i = 0; i < data.length; i++) {

                    dropdown.append($('<option></option>').attr('value', data[i].value).text(data[i].text));
                }
            }
        },
        error: function (error) {
            console.log(error.massage);
        }
    })
}


const GetReport = () => {
    $(".preloaderDiv").show();
    let t = $("#Type").val();
    let tuu = $("#TourId").val()
    if (t == "3") {
        if (tuu == null) {
            alert("Task Name Required !")
            return;
        }
    }
    $.ajax({

        url: myurl + '/Reports/myTask',
        type: 'Get',
        data: {
            Type: $("#Type").val(), ClientCode: $("#Client").val(), Site: $("#Site").val(), fromdate: $("#fromdate").val()
            , todate: $("#todate").val(), ChecklistClient: $("#ChecklistClient").val(), ChecklistSite: $("#ChecklistSite").val(), Status: $("#Status").val()
            , TourId: $("#TourId").val(), HeaderName: $("#ChecklistHeader").val() 
        },
        success: function (data) {
            console.log(data);
            $(".preloaderDiv").hide();
            if (data.statusCode > 200) {
                alert("Record Not Found !!")
                $("#Printdiv").empty();
                return;
            }


            var Record = JSON.parse(data.data.dt2);
            var Details = JSON.parse(data.data.dt);
            if (Record.length > 0) {
                $("#sitename").text($("#Site option:selected").text())
                $("#customername").text($("#Client option:selected").text())
                $("#HeaderName").text($("#ChecklistHeader option:selected").text())
                $("#staffname").text(Details[0].EmpName)
                $("#designation").text(Details[0].Designation)
                $("#visitdate").text(Details[0].Date)
                $("#dataclient").removeClass('d-none');
                CreateTableFromArray(Record, 'Printdiv')
            }


        },
        error: function (error) {
            console.log(error.massage);
            $(".preloaderDiv").hide();
        }
    })
} 

function exportexcel(type, fn, dl) {
    var ddd = $("#header").text();
    var elt = document.getElementById('data-table');
    var filename = ddd;


    if (elt == null) {
        alert("No Data to Export");
        return;
    }

    var tableClone = elt.cloneNode(true);


    var headers = tableClone.querySelectorAll("tr th");


    for (var i = headers.length - 1; i >= 0; i--) {
        var headerText = headers[i].innerText.trim();


        if (headerText === "Action" || headerText.startsWith("Hid_")) {

            tableClone.querySelectorAll("tr").forEach(function (row) {
                if (row.cells[i]) {
                    row.deleteCell(i);
                }
            });
        }
    }

    var wb = XLSX.utils.table_to_book(tableClone, { sheet: "sheet1" });


    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}





document.getElementById("Genratepdf").addEventListener("click", () => {
    var companyName = $("#customername").text();

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
    <title>Visit Report of ${companyName}</title>

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
             max-width:100px;
              display: inline-block;
            word-wrap: break-word;
        }

        #dateField {
            width: 80px;
            display: inline-block;
        }

        #ShiftDetails {
            width: 70px;
            display: inline-block;
        }

        @media print {
           
             border:2px solid black;
           
            body {
                     -webkit-print-color-adjust: exact !important;
        print-color-adjust: exact !important; 
               
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
                size: A4 portrait;
                margin: 6.20mm 6.20mm  6.20mm 6.20mm;             
                -webkit-print-color-adjust: exact;
              font-family: 'Times New Roman', Times, serif, sans-serif;
                  page-break-after: always;
                   -webkit-print-color-adjust: exact !important;
                  padding:10px;
                 
                    border:2px solid black;
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
          
            margin: 5px;         
                
           
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

          
        }
          th {
                color: white !important;
                   background-color: #c84143 !important;
                    border: solid 1px black !important;

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
        #sitename {
            max-width:100px;
            display: inline-block;
            word-wrap: break-word;
        }
     
        #detailstable{
            margin:20px 0;
            
        }
        table tr td {
        border: solid 1px black !important;
        border-collapse: collapse;
        color:black !important;
        
    }
       
    </style>
</head>

`


    var headerContent = `<body>
    <div id="pagecontainer">
        <div class="container" id="content">
            <div class="header1" style="margin-top:-20px;">
                <img src="https://ifm360.in/grouplreportingportal/GroupL.jfif" alt="Group L Logo" class="logo">
                <p style="margin-top:-20px;">3rd Floor, w31, Okhla Industrial Area Phase 2 <br /> New Delhi 110020 </p>
               
            </div>

        </div>
          </div>
        </body>

</html>

`;


    var datadiv = document.getElementById("prindDiv").innerHTML;



    var content1 = content + headerContent;


    var popupwin = window.open();


    popupwin.document.write(content1 + datadiv);
    popupwin.document.close();
    var logoImage = popupwin.document.getElementById("logoimag");
    popupwin.onload = function () {
        popupwin.focus();
        popupwin.print();
        popupwin.close();
    }





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
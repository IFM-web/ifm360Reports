$(document).ready(function () {
    document.getElementById('txtdate').value = new Date().toISOString().substring(0, 10);
    var company = $("#custid").val();
    if (company !== "") {
        $("#custid").trigger('change');
    }
    // $(".preloader").hide();
})

let myurl = localStorage.getItem('Myurl');
function bindsite() {
    $.ajax({
       
       url:  myurl +'/Reports/bindclient',
        type: 'post',
        data: { custid: $("#custid").val(), },
        success: function (data) {
            bindvisittype();
            var data = JSON.parse(data);

            var dropdown = $('#client');
            dropdown.empty();
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].AsmtId).text(data[i].CodeName));
            }

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}
function bindvisittype() {
    $.ajax({
    
        url: myurl + '/Reports/bindvisittype',
        type: 'post',
        data: { custid: $("#custid").val(), },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#visittype');
            dropdown.empty();
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].ChecklistHeader).text(data[i].ChecklistHeader));
            }

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}



function SearchData() {
    $(".preloader").show();
    $.ajax({
       
       url:  myurl +'/Reports/GetVisitReport',
        type: 'post',
        data: {
            date: $("#txtdate").val(),
            custid: $("#custid").val(),          
            client: $("#client").val(),
            visittype: $("#visittype").val(),
        },
        success: function (data) {
            console.log(data);
            $(".preloader").hide();
            if (data.dt1 != '[]') {

                $("#signature").show();
                $("#dataclient").show();
                $(".companybody").empty();
                var datatable = JSON.parse(data.dt);
                var datatable1 = JSON.parse(data.dt1);
                var datatable2 = JSON.parse(data.dt2);

                $("#customername").text(datatable[0].ClientName);
                $("#sitename").text(datatable[0].AsmtName);
                $("#staffname").text(datatable1[0].EmpName);
                $("#designation").text(datatable1[0].Designation);
                $("#visitdate").text(datatable1[0].VisitDate);
                $("#clientName").text(datatable1[0].ClientName);
                $("#clientphone").text(datatable1[0].ClientPhone);
                $("#clientemail").text(datatable1[0].ClientEmail);
                $("#clientremark").text(datatable1[0].ClientRemarks);
                $("#clientrating").text(datatable1[0].ClientRating);
                $("#ClientDesignation").text(datatable1[0].ClientDesignation);
                var ClientSignature = 'data: image/jpeg;base64,' + datatable1[0].ClientSignature;
                var StaffSignature = 'data: image/jpeg;base64,' + datatable1[0].EmpSign;
                console.log(ClientSignature)
                console.log(StaffSignature)
                document.getElementById('ClientSignature').setAttribute('src', ClientSignature);
                document.getElementById('StaffSignature').setAttribute('src', StaffSignature);
               

                var rowlen = parseInt($('.companybody tr').length);
                // console.log(data)
                var row = '';
                for (var i = 0; i < datatable2.length; i++) {
                    var url = 'data: image/jpeg;base64,' + datatable2[i].VisitImage

                    row += "<tr id='row" + i + "'><td style=''>" + parseInt(i + 1) + "</td><td  style='' class='ChecklistName'>" + datatable2[i].ChecklistName + "</td><td style=''><span class='Status'>" + datatable2[i].Status + "</span></td><td style=''><span class='Remarks' id='Remarks'>" + datatable2[i].Remarks + "</span></td><td style=''><img  style='height: 100px; width: 100px;'src= '" + (datatable2[i].VisitImage !== null ? url : '/grouplreportingportal/grouplreportingportal/NoImage.jpg') + "' class='EmployeeImage'></></td></tr>";


                }

                $(".companybody").prepend(row);




            }
           
            else {
                $(".preloader").hide();
                $(".companybody").empty();
                $("#dataclient").hide();
              
                $("#signature").hide();
                
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
                size: A4 portrait;
                margin: 6.20mm 6.20mm  6.20mm 6.20mm;             
                -webkit-print-color-adjust: exact;
              font-family: 'Times New Roman', Times, serif, sans-serif;
                  page-break-after: always;
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
        #sitename {
            max-width:100px;
            display: inline-block;
            word-wrap: break-word;
        }
        //#sitename1{
        //    width:400px;
        //}
        #detailstable{
            margin:20px 0;
            
        }
        #signature{
             margin:40px 0;
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
    var topdata = document.getElementById("dataclient").innerHTML;
    var signature = document.getElementById("signature").innerHTML;

    var content1 = content + headerContent;


    var popupwin = window.open();

  
    popupwin.document.write(content1 + topdata + datadiv + signature);
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


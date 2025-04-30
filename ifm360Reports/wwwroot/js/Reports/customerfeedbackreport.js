

$(document).ready(function () {
    let currentDate = new Date();
/*    currentDate.setDate(currentDate.getDate() - 7);*/
    document.getElementById('txtfromdate').value = new Date().toISOString().substring(0, 10);
    document.getElementById('txttodate').value = new Date().toISOString().substring(0, 10);

    var company = $("#txtfromdate").val();
    if (company !== "") {
        $("#txtfromdate").trigger('change');
        var company = $("#custid").val();
        if (company !== "") {
            $("#custid").trigger('change');
        }
    }
})


let myurl = localStorage.getItem('Myurl');
function SearchData() {
    $(".preloader").show();
    $.ajax({
       
       url: myurl+'/Reports/GetConsolidatedVisitReport',
        type: 'post',
        data: {
            todate: $("#txttodate").val(),
            fromdate: $("#txtfromdate").val(),
            clientcode: $("#custid").val(),
            Regid: $("#Regid").val(),
          
        },
        success: function (data) {
            console.log(data);
            $(".preloader").show();
            if (data != '[]') {


                $(".companybody").empty();
                var data = JSON.parse(data);
                


                var rowlen = parseInt($('.companybody tr').length);
                // console.log(data)
                var row = '';
                var row2 = '';

                for (var i = 0; i < data.length; i++) {
                    $(".preloader").hide();
                    var url = 'data: image/jpeg;base64,' + data[i].VisitImage

                    row += "<tr id='row" + i + "'><td> <button type='submit' id='btn' class='btn btn-success' onclick='GetVisitReport(\"" + data[i].CompletionDate + "\",\"" + data[i].ClientCode + "\",\"" + data[i].AsmtId + "\",\"" + data[i].VisitType + "\",\"" + data[i].EmpID + "\",\"" + data[i].LocationAutoID +"\")'>Visit Report</button></td><td style=''>" + parseInt(i + 1) + "</td><td  style='' class='zone'>" + data[i].Zone + "</td><td  style='' class='branch'>" + data[i].Branch + "</td><td  style='' class='CustomerName'>" + data[i].CustomerName + "</td><td style=''><span class='AsmtName'>" + data[i].AsmtName + "</span></td><td> <span class='ClientDesignation'>" + data[i].ClientOccupantName + "</span></td ><td> <span class='ClientDesignation'>" + data[i].ClientDesignation + "</span></td ><td> <span class='ClientDesignation'>" + data[i].ClientPhone + "</span></td ><td> <span class='ClientDesignation'id='clientemail'>" + data[i].ClientEmail + "</span></td > <td> <span class='ClientDesignation'>" + data[i].ClientRating + "</span></td > <td> <span class='ClientDesignation'>" + data[i].ClientRemarks + "</span></td > <td> <span class='ClientDesignation'id ='dateField'>" + data[i].CompletionDate + "</span></td ><td> <span class='Empid'>" + data[i].EmpID + "</span></td ><td> <span class='ClientDesignation'>" + data[i].EmpName + "</span></td > </tr>";

                    row2 += "<tr id='row" + i + "'><td style=''>" + parseInt(i + 1) + "</td><td  style='' class='zone'>" + data[i].Zone + "</td><td  style='' class='branch'>" + data[i].Branch + "</td><td  style='' class='CustomerName'>" + data[i].CustomerName + "</td><td style=''><span class='AsmtName'>" + data[i].AsmtName + "</span></td><td> <span class='ClientOccupantName'>" + data[i].ClientOccupantName + "</span></td ><td> <span class='ClientDesignation'>" + data[i].ClientDesignation + "</span></td ><td> <span class='ClientPhone'>" + data[i].ClientPhone + "</span></td ><td> <span class='ClientEmail'id='clientemail'>" + data[i].ClientEmail + "</span></td > <td> <span class='ClientRating'>" + data[i].ClientRating + "</span></td > <td> <span class='ClientRemarks'>" + data[i].ClientRemarks + "</span></td > <td> <span class='CompletionDate'id ='dateField'>" + data[i].CompletionDate + "</span></td ><td> <span class='Empid'>" + data[i].EmpID + "</span></td > <td> <span class='EmpName'>" + data[i].EmpName + "</span></td > </tr>";

                    
                } 

                $(".companybody").prepend(row);
                $(".companybody2").prepend(row2);





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


function SearchData2() {
    $(".preloader").show();
    $.ajax({
        
        url: myurl+'/Reports/GetConsolidatedVisitReport',
        type: 'post',
        data: {
            todate: $("#txttodate").val(),
            fromdate: $("#txtfromdate").val(),
            clientcode: $("#custid").val(),
            Regid: $("#Regid").val(),

        },
        success: function (data) {
            console.log(data);
            $(".preloader").show();
            if (data != '[]') {


                $(".companybody").empty();
                var data = JSON.parse(data);



                var rowlen = parseInt($('.companybody tr').length);
                // console.log(data)
                var row = '';
                var row2 = '';

                for (var i = 0; i < data.length; i++) {
                    $(".preloader").hide();
                    var url = 'data: image/jpeg;base64,' + data[i].VisitImage

                    row += "<tr id='row" + i + "'><td> <button type='submit' id='btn' class='btn btn-success' onclick='VisittopReport(\"" + data[i].CompletionDate + "\",\"" + data[i].ClientCode + "\",\"" + data[i].AsmtId + "\",\"" + data[i].VisitType + "\",\"" + data[i].EmpID + "\",\"" + data[i].LocationAutoID + "\")'>Visit Report</button></td><td style=''>" + parseInt(i + 1) + "</td><td  style='' class='zone'>" + data[i].Zone + "</td><td  style='' class='branch'>" + data[i].Branch + "</td><td  style='' class='CustomerName'>" + data[i].CustomerName + "</td><td style=''><span class='AsmtName'>" + data[i].AsmtName + "</span></td><td> <span class='ClientDesignation'>" + data[i].ClientOccupantName + "</span></td ><td> <span class='ClientDesignation'>" + data[i].ClientDesignation + "</span></td ><td> <span class='ClientDesignation'>" + data[i].ClientPhone + "</span></td ><td> <span class='ClientDesignation'id='clientemail'>" + data[i].ClientEmail + "</span></td > <td> <span class='ClientDesignation'>" + data[i].ClientRating + "</span></td > <td> <span class='ClientDesignation'>" + data[i].ClientRemarks + "</span></td > <td> <span class='ClientDesignation'id ='dateField'>" + data[i].CompletionDate + "</span></td ><td> <span class='Empid'>" + data[i].EmpID + "</span></td ><td> <span class='ClientDesignation'>" + data[i].EmpName + "</span></td > </tr>";

                    row2 += "<tr id='row" + i + "'><td style=''>" + parseInt(i + 1) + "</td><td  style='' class='zone'>" + data[i].Zone + "</td><td  style='' class='branch'>" + data[i].Branch + "</td><td  style='' class='CustomerName'>" + data[i].CustomerName + "</td><td style=''><span class='AsmtName'>" + data[i].AsmtName + "</span></td><td> <span class='ClientOccupantName'>" + data[i].ClientOccupantName + "</span></td ><td> <span class='ClientDesignation'>" + data[i].ClientDesignation + "</span></td ><td> <span class='ClientPhone'>" + data[i].ClientPhone + "</span></td ><td> <span class='ClientEmail'id='clientemail'>" + data[i].ClientEmail + "</span></td > <td> <span class='ClientRating'>" + data[i].ClientRating + "</span></td > <td> <span class='ClientRemarks'>" + data[i].ClientRemarks + "</span></td > <td> <span class='CompletionDate'id ='dateField'>" + data[i].CompletionDate + "</span></td ><td> <span class='Empid'>" + data[i].EmpID + "</span></td > <td> <span class='EmpName'>" + data[i].EmpName + "</span></td > </tr>";


                }

                $(".companybody").prepend(row);
                $(".companybody2").prepend(row2);





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

function exportexcel(type, fn, dl) {
    var data = $("#txttodate").val()
    var data1 = $("#txtfromdate").val()
    


   
    var ddd = $("#txtpagename").val();
    var elt = document.getElementById('data-tablehid');
    filename = ddd + data1 + ' To '+ data;
    console.log(filename);
    console.log(elt);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });


    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}


document.getElementById("Genratepdf").addEventListener("click", () => {
    var data = $("#txttodate").val()
    var data1 = $("#txtfromdate").val()

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
    <title>Consolidated Visit Report of ${data1} To ${data}</title>

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
                size: A4 landscape;
                margin: 6.20mm 6.20mm 6.20mm 0;             
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
          
            margin: 20px;         
                  
           
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

        #data-table tr td:first-child {
    display: none;
}

        .job-details h2,
        .client-details h2,
        .checkpoints h2 {
            font-size: 20px;
            padding-bottom: 10px;
            padding-top: 20px;
        }
        #btn{
            display:none;
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
        #clientemail{
           max-width:100px;
            display: inline-block;
            word-wrap: break-word;
            
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
            <br/>

        </div>
          </div>
        </body>

</html>

`;


    var datadiv = document.getElementById("prindDiv").innerHTML;
   // var topdata = document.getElementById("dataclient").innerHTML;

    var content1 = content + headerContent;

    var datadiv = document.getElementById("prindDiv");
    const hideFrame = document.createElement("iframe");
   

    var popupwin = window.open();
    popupwin.document.write(content1  + datadiv.innerHTML);
    popupwin.document.close();
    var logoImage = popupwin.document.getElementById("logoimag");
    popupwin.onload = function () {
        popupwin.focus();
        popupwin.print();
        popupwin.close();
    }

});



function bindsite() {
    $.ajax({
       
       url: myurl+'/Reports/bindclient',
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
        
        url: myurl+'/Reports/bindvisittype',
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


function GetVisitReport(date, cleint, AsmtId, vtype, EmpId,LocautoId) {
    window.location.href = "/grouplreportingportal/Reports/visitreport?date=" + date + "&&client=" + cleint + " &&AsmtId=" + AsmtId + "&&vtype=" + vtype + "&&empid=" + EmpId + " &&LocautoId=" + LocautoId+"";
}

function VisittopReport(date, cleint, AsmtId, vtype, EmpId,LocautoId) {
    window.location.href = "/grouplreportingportal/Reports/VisittopReport?date=" + date + "&&client=" + cleint + " &&AsmtId=" + AsmtId + "&&vtype=" + vtype + "&&empid=" + EmpId + " &&LocautoId=" + LocautoId+"";
}



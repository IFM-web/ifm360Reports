$(document).ready(function () {

    
    const today = new Date();

  
    today.setDate(today.getDate() - 7);

  
    const formattedDate = today.toISOString().split('T')[0];

    document.getElementById('txtfromdate').value = formattedDate;
    
    $('#txttoDate').val(new Date().toISOString().substring(0, 10)) 
  

    SearchData();
    // $(".preloader").hide();
})

let myurl = localStorage.getItem('Myurl');
var contentdata = '';

function SearchData() {
    $(".preloader").show();


    $.ajax({
       
       url: myurl +'/Reports/GetAudioStatusReport',
        type: 'post',
        data: {
            fromdate: $("#txtfromdate").val(),
            todate: $("#txttoDate").val(),
            Regid: $("#Regid").val(),
            clientcode: $("#custid").val(),
          
        },
        success: function (data) {
            $(".preloader").hide();
            if (data != '[]') {


                $(".companybody").empty();
                var data = JSON.parse(data);
              
                var rowlen = parseInt($('.companybody tr').length);
                // console.log(data)
                var row = '';
                for (var i = 0; i < data.length; i++) {
                    row += "<tr id='row" + i + "'><td style=''>" + parseInt(i + 1) + "</td><td  style='' class='itemn'>" + data[i].ClientName + "</td><td style=''><span class='TotalBranches'>" + data[i].TotalBranches + "</span></td><td style=''><span class='TotalVisitsDone' id='TotalVisitsDone'>" + data[i].TotalVisitDone + "</span></td><td style=''><span class='BranchesNotVisited'>" + data[i].BranchNotVisited + "</span></td><td style=''><span class='LowestRating'>" + (data[i].LowestRating !== null ? data[i].LowestRating : "") + "</span></td><td style=''><span class='No.ofUnitsofLowestRating' id='dateField' >" + (data[i].LowestRatingUnits !== null ? data[i].LowestRatingUnits : '') + "</span></td><td style=''><span class='HighestRatin'>" + (data[i].HighestRating !== null ? data[i].HighestRating : '') + "</span></td><td style=''><span class='No.ofUnitsofHighestRating'>" + (data[i].HighestRatingUnits !== null ? data[i].HighestRatingUnits:'' )+ "</span></td></tr>";


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
    var data = $("#txttodate").val()
    var data1 = $("#txtfromdate").val()




    var ddd = $("#txtpagename").val();
    var elt = document.getElementById('data-tablehid');
    filename = ddd;
    console.log(filename);
    console.log(elt);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });


    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}


document.getElementById("Genratepdf").addEventListener("click", () => {
    var companyName = $("#custid :selected").text();

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
    <title>Audit Status Report </title>

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
                margin: 8.20mm 8.30mm 11.5mm 0;             
                -webkit-print-color-adjust: exact;
              font-family: 'Times New Roman', Times, serif, sans-serif;
                  page-break-after: always;
                 
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
            thead {
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
            <div class="header1" style="margin-top:-20px;">
                <img src="/grouplreportingportal/GroupL.jfif" alt="Group L Logo" class="logo">
                <p style="margin-top:-20px;">3rd Floor, w31, Okhla Industrial Area Phase 2 <br /> New Delhi 110020 </p>
                <br/>
               
            </div>

        </div>
          </div>
        </body>

</html>

`;


    var datadiv = document.getElementById("prindDiv").innerHTML;
    // var topdata = document.getElementById("dataclient").innerHTML;
    // var signature = document.getElementById("signature").innerHTML;

    var content1 = content + headerContent;

    var datadiv = document.getElementById("prindDiv");
    //const hideFrame = document.createElement("iframe");
    var popupwin = window.open('', '_blank', 'width=100%', 'height=100%');

    popupwin.document.open();
    popupwin.document.write(content1 + datadiv.innerHTML);
    //popupwin.document.close();

    popupwin.focus();
    popupwin.print();
    popupwin.close();
    return true;

});
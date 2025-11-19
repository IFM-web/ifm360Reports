
$(document).ready(() => {
   document.getElementById('fromdate').value = new Date().toISOString().substring(0, 10);
    document.getElementById('todate').value = new Date().toISOString().substring(0, 10);
   
    let ClientDiv = $("#ClientDiv");
    let SiteDiv = $("#SiteDiv");
    let Fdatediv = $("#Fdatediv");
    let Todatediv = $("#Todatediv");
    let ChecklistClientDiv = $("#ChecklistClientDiv");
    let ChecklistSiteDiv = $("#ChecklistSiteDiv");
    let StatusDiv = $("#StatusDiv");
    let btnsearch = $("#btnsearchtext");
    let TourDiv = $("#TourDiv");
    let type = $("#Type").val();

   
    Fdatediv.hide();
    Todatediv.hide();
    if (type === "3" || type=="6") {
       
        ChecklistClientDiv.show();
        }
        else {
        ChecklistClientDiv.hide();
    }


 

    if (type === "1") {
       
        StatusDiv.show();
        }
        else {
        StatusDiv.hide();
    }
    if (type == "3" ) {


        ChecklistSiteDiv.show()
        TourDiv.show();
        Fdatediv.show();
        $("#labledate").text('Date')
       
    }
    else if (type === "1") {

        ChecklistSiteDiv.hide();
        Fdatediv.hide();
    }
    
    if (type == "6") {
        Fdatediv.show();
        Todatediv.show();

    }
  

    if (type === "4") {
        btnsearch.text("Click to view details");
    }
    

    getClientListSite();
   

})



$("#Company").on('change', (e) => {
    bindRegion(e.target.value);
})


$("#ChecklistClient").on('change', (e) => {
    getClientListSite();
})


$("#Region").on('change', (e) => {
    bindbranch();
})

$("#Client").on('change', (e) => {
    GetTour(e.target.value);
    bindsite(e.target.value);
 
})
let myurl = localStorage.getItem('Myurl');
function bindRegion(id) {
    $.ajax({

        url: myurl + '/Home/bindRegion2',
        type: 'post',
        data: { id: id },
        success: function (data) {

            var data = JSON.parse(data);

            var dropdown = $('#Region');
            dropdown.empty();
            dropdown.append('<option value"All">All</option>')
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].HrLocationCode).text(data[i].HrLocationDesc));
            }

            $('#branchid').val("All")
        },
        error: function (error) {
            alert(error.massage);
        }
    })
}
function bindbranch() {
    $.ajax({

        url: myurl + '/Reports/BindBranchtoReg',
        type: 'post',
        data: { regid: $("#Region").val() },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#Branch');
            dropdown.empty();
            dropdown.append('<option value"All">All</option>')
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].LocationAutoID).text(data[i].LocationCode));
            }

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}
function bindsite(Id) {
    $.ajax({

        url: myurl + '/Reports/bindSiteToOutrange',
        type: 'post',
        data: { siteid: Id, },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#Site');
           
            dropdown.empty();
            dropdown.append($('<option></option>').attr('value', 'All').text('All'));
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].AsmtId).text(data[i].AsmtName));
            }
        

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}

function getClientListSite() {
    GetTour($("#ChecklistClient").val())
    $.ajax({

        url: myurl + '/Reports/getClientListSite',
        type: 'post',
        data: { Id: $("#ChecklistClient").val(), },
        success: function (data) {
            if (data.statusCode == 200) {
                var data = JSON.parse(data.data);

                var dropdown = $('#ChecklistSite');

                dropdown.empty();
                //dropdown.append($('<option></option>').attr('value', 'All').text('All'));
                for (var i = 0; i < data.length; i++) {

                    dropdown.append($('<option></option>').attr('value', data[i].AsmtId).text(data[i].AsmtName));
                }
            }
        },
        error: function (error) {
            console.log(error.massage);
        }
    })
}


const GetReport = () => {
    let t = $("#Type").val();
    let tuu = $("#TourId").val()
    if (t == "3") {
        if (tuu == null) {
            alert("Task Name Required !")
            return;
        }
    }
    $.ajax({

        url: myurl + '/Reports/GetReport',
        type: 'Get',
        data: {
            Type: $("#Type").val(), ClientCode: $("#Client").val(), Site: $("#Site").val(), fromdate: $("#fromdate").val()
            , todate: $("#todate").val(), ChecklistClient: $("#ChecklistClient").val(), ChecklistSite: $("#ChecklistSite").val(), Status: $("#Status").val()
            , TourId: $("#TourId").val()
        },
        success: function (data) {

            if (data.statusCode > 200) {
                alert("Record Not Found !!")
                $("#Printdiv").empty();
                return;
            }


            var data = JSON.parse(data.data);
            if (data.length > 0) {

                CreateTableFromArray(data, 'Printdiv')
            }     


        },
        error: function (error) {
            console.log(error.massage);
        }
    })
} 


const ResignationAccptreject = (Type,Id) => {
    $.ajax({

        url: myurl + '/Reports/ResignationAccptreject',
        type: 'Get',
        data: {
            type: Type,Id:Id
        },
        success: function (data) {
            var data = JSON.parse(data);
            console.log(data)
            alert(data[0].MessageString);
            if (data.length > 0) {

                GetReport();
            }
          


        },
        error: function (error) {
            alert(error.massage);
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






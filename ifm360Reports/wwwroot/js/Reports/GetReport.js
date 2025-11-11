
$(document).ready(() => {
   document.getElementById('fromdate').value = new Date().toISOString().substring(0, 10);
    //document.getElementById('Todate').value = new Date().toISOString().substring(0, 10);

   
        let ClientDiv = $("#ClientDiv");
    let SiteDiv = $("#SiteDiv");
    let Fdatediv = $("#Fdatediv");
    let ChecklistClientDiv = $("#ChecklistClientDiv");
    let ChecklistSiteDiv = $("#ChecklistSiteDiv");
        let type = $("#Type").val();

        console.log(type);
        console.log(ClientDiv);

    if (type === "3" ) {
        ChecklistClientDiv.show();
        }
        else {
        ChecklistClientDiv.hide();
    }
    if (type == "3") {

        ChecklistSiteDiv.show()
        Fdatediv.show();
    }
    else {
    
        ChecklistSiteDiv.hide();
        Fdatediv.hide();
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
    $.ajax({

        url: myurl + '/Reports/getClientListSite',
        type: 'post',
        data: { Id: $("#ChecklistClient").val(), },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#ChecklistSite');
           
            dropdown.empty();
            //dropdown.append($('<option></option>').attr('value', 'All').text('All'));
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].AsmtId).text(data[i].AsmtName));
            }
        

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}


const GetReport = () => {
    $.ajax({

        url: myurl + '/Reports/GetReport',
        type: 'Get',
        data: {
            Type: $("#Type").val(), ClientCode: $("#Client").val(), Site: $("#Site").val(), fromdate: $("#fromdate").val()
            , ChecklistClient: $("#ChecklistClient").val(), ChecklistSite: $("#ChecklistSite").val()
        },
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {

                CreateTableFromArray(data, 'Printdiv')
            }
            else {
                $("#Printdiv").empty();
                alert("Record Not Found !!")
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
    filename = ddd;

    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });


    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}



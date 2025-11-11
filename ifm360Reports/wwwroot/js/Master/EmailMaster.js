$(document).ready(function()
    {
    showdata();
});


function SaveData() {

    let val = Validation();
    if (val != '') {
        alert(val);
        return;
    }


    var EmpId = $('#txtEmpId').val();
    var EmpName = $('#txtEmpName').val();
    var EmailId = $('#txtemailId').val();
    var Id = $('#HidId').val();
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailRegex.test(EmailId)) {
        alert("Please enter a valid email address.");
        return;
    }

    $.ajax({
        url: localStorage.getItem('Myurl') + '/Master/InsertEmailMaster',
        type: 'post',
        data: { EmpId: EmpId, EmpName: EmpName, EmailId: EmailId, Id: Id },
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {
         
                alert(data[0].Message);
            }
            if(data[0].Status=="Success")
              clean();
        },
        error: function (error) {
            alert(error.Message);
        }
    });
}

function showdata() {
    $.ajax({
        url: localStorage.getItem('Myurl') + '/Master/GetEmailMaster',
        type: 'get',
        data: {},
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {
             
                $('#PrintDiv').empty();
                CreateTableFromArray(data, 'printDiv');
            }
        },
        error: function (error) {
            alert(error.Message);
        }
    });
}
function clean(){
    $('#txtEmpId').val('');
    $('#txtEmpName').val('');
    $('#txtemailId').val('');
    $('#btn').text('Submit');
    $('#HidId').val(0);

    $('#txtEmpId').prop("disabled", false);

    showdata();
}
function Edit(id) {
    var EmpID = $('#EmpID' + id).text();
    var EmpName = $('#EmpName' + id).text();
    var EmpEmail = $('#EmpEmail' + id).text();
    $('#txtEmpId').val(EmpID);
    $('#txtEmpName').val(EmpName);
    $('#txtemailId').val(EmpEmail);
    $('#btn').text('Update');
    $('#HidId').val(1);
    $('#txtEmpId').prop("disabled", true);
}

function Delete(EmpId) {
    $.ajax({
        url: localStorage.getItem('Myurl') + '/Master/DeleteEmailMaster',
        type: 'post',
        data: { EmpId: EmpId },
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {
              
                alert(data[0].Message);
                showdata() 
            }
           
        },
        error: function (error) {
            alert(error.Message);
        }
    });
}

function exportexcel(type, fn, dl) {
    var elt = document.getElementById('data-table');
    var filename = 'Email Master';

  
    var clonedTable = elt.cloneNode(true);


    var headerRow = clonedTable.rows[0];
    var removeColIndex = -1;
    var removeColName = 'Action';

 
    for (var i = 0; i < headerRow.cells.length; i++) {
        var cellText = headerRow.cells[i].innerText.trim();
        if (cellText.toLowerCase() === removeColName.toLowerCase()) {
            removeColIndex = i;
            break;
        }
    }

    if (removeColIndex >= 0) {
        for (var row of clonedTable.rows) {
            if (row.cells.length > removeColIndex) {
                row.deleteCell(removeColIndex);
            }
        }
    }


    var wb = XLSX.utils.table_to_book(clonedTable, { sheet: "sheet1" });


    XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}


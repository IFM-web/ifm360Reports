$(document).ready(() => {
    $("#ddlClient").select2();
})
function showGrid() {    
    var val = $('#ddlClient').val();
    $('#txtAutoId').val(0);
    $('#btn').text('Submit');
    $('#txtCheckListHeader').val('');
   
        $.ajax({
            url: localStorage.getItem('Myurl') + '/Master/GetCheckListHeader',
            type: 'get',
            data: { val: val },
            dataType: 'json',
            success: function (data) {
                var data = JSON.parse(data);
                if (data.length > 0) {
                  
                    $('#PrintDiv').empty();
                    CreateTableFromArray(data, 'PrintDiv');
                }
            },
            error: function (error) {
                console(error.Message);
             
            }
        });
    
}


function addCheckListHeader() {
    let val = Validation();
    if (val != '') {
        alert(val);
        return;
    }
    var clientCode = $('#ddlClient').val();
    var checkListHeader = $('#txtCheckListHeader').val();
    var AutoId = $('#txtAutoId').val();
    $.ajax({
        url: localStorage.getItem('Myurl') + '/master/addCheckListHeader',
        type: 'post',
        data: { clientCode: clientCode, checkListHeader: checkListHeader, AutoId: AutoId },
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {
                if (data[0].Status == 'Success')
                    clean();
                console.log(data);
                alert(data[0].Message);
            }
            
        },
        error: function (error) {
            alert(error.Message);
        }
    });
}

function deleteRow(Autoid) {
    $.ajax({
        url: localStorage.getItem('Myurl') + '/master/deleteCheckListHeader',
        type: 'post',
        data: { Autoid: Autoid },
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {
                console.log(data);
                alert(data[0].Message);
            }
            clean();
        },
        error: function (error) {
            alert(error.Message);
        }
    });
}
function edit(id) {
    var AutoId = $('#Hid_AutoID' + id).text();
    var checkListHeader = $('#ChecklistHeader' + id).text();
    $('#txtCheckListHeader').val(checkListHeader);
    $('#txtAutoId').val(AutoId);
    $('#btn').text('Update');
}

function clean() {
    $('#txtAutoId').val(0);
    $('#btn').text('Submit');
    $('#txtCheckListHeader').val('');

    showGrid();

}
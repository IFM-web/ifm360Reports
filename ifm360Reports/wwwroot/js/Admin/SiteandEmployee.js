
$(document).ready(() => {
    $("#ddlUser").select2();
    $("#ddlUser").select2();

    let Divforsite = $("#Divforsite");
    let divforemp = $(".divforemp");
    let Types = $('#Type').val();

    if (Types == "Site") {
        Divforsite.show();
        divforemp.hide();
    }
    if (Types != "Site") {
        Divforsite.hide();
        divforemp.show();
    }

})



function show() {
    var CleintCode = $('#ddlClient').val();
    var BranchId = $('#BranchId').val();
    var isactive = $('#isactive').val();
    var Type = $('#Type').val();

    $.ajax({
        url: localStorage.getItem('Myurl') + '/Admin/snowStatusDetails',
        type: 'get',
        data: { CleintCode, Type, BranchId, isactive },
        success: function (data) {
            var data = JSON.parse(data.data);
            if (data.length > 0) {
                console.log(data);
                $(".btnsave").removeClass('d-none');
                $("#PrintDiv").empty();
                CreateTableFromArray(data, 'PrintDiv');
            }
            else {
                $("#PrintDiv").empty();
            }
        },
        error: function (error) {
            alert(error.Message);
        }
    });

}

function save() {
    let checkedValues = [];
  
    $('#PrintDiv tbody tr').each(function (i, row) {
        let r = $(row);
        let cobox = r.find("#Status").is(":checked") ? 1 : 0;
        let Id = r.find(".Hid_Id").text();
        if (Id != '') {
            checkedValues.push({ Id: Id,Status: cobox });
        }
    })
    console.log("Checked Values:", checkedValues);
    $.ajax({
        url: localStorage.getItem('Myurl') + '/Admin/saveManageStatus',
        type: 'post',
        data: { data: JSON.stringify(checkedValues), Clientcode: $("#ddlClient").val(), type: $("#Type").val(), BranchId:$('#BranchId').val() },
        success: function (data) {
            var data = JSON.parse(data);
            alert(data[0].Message);
            show()
        },
        error: function (error) {
            alert(error.Message);
        }
    });
}

function bindbranch() {
    $.ajax({

        url: localStorage.getItem('Myurl') +  '/Dropdown/GetBranchByRegion',
        type: 'get',
        data: { Id: $("#Region").val() },
        success: function (data) {
            var data = data.data;

            var dropdown = $('#BranchId');
            dropdown.empty();
            dropdown.append($('<option></option>').attr('value', '0').text('Select'));
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].value).text(data[i].text));
            }

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}

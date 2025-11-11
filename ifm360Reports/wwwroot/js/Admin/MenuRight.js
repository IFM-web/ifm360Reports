
$(document).ready(() => {
    $("#ddlUser").select2();
    $("#ddlUser").select2();
})



function show() {
    var user = $('#ddlUser').val();
    var parentMenu = $('#ddlParent').val();

    if (user == '0') {
        alert("select User")
        return;
    }
  
        $.ajax({
            url: localStorage.getItem('Myurl') +'/Admin/showMenuRight',
            type: 'post',
            data: { parentMenu: parentMenu, user: user },
            success: function (data) {
                var data = JSON.parse(data);
                if (data.length > 0) {
                    console.log(data);
                    let mappedData = data.map(obj => {
                        return Object.fromEntries(
                            Object.entries(obj).map(([key, value]) => [key, value == null ? 0 : value])
                        );
                    });
                    $("#PrintDiv").empty();
                    CreateTableFromArray(mappedData, 'PrintDiv');
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
    let UserId = $('#ddlUser').val();
    $('#PrintDiv tbody tr').each(function (i, row) {
        let r = $(row);
        let cobox = r.find("#menuCheckBox").is(":checked")?1:0;
        let MenuId = r.find(".Hid_id").text();
        if (MenuId != '') {
            checkedValues.push({ UserId: UserId, MenuId: MenuId, Status: cobox });
        }
    })
    console.log("Checked Values:", checkedValues);
    $.ajax({
        url: localStorage.getItem('Myurl') +'/Admin/saveMenuRight',
        type: 'post',
        data: { data: JSON.stringify(checkedValues) },
        success: function (data) {
            var data = JSON.parse(data);
            alert(data[0].Message);
        },
        error: function (error) {
            alert(error.Message);
        }
    });
}

//function save() {
//    // Get all checked checkboxes inside #PrintDiv
//    let checkedValues = [];
//    $('#PrintDiv input[type="checkbox"]:checked').each(function () {
//        checkedValues.push($(this).val());
//    });

//    if (checkedValues.length === 0) {
//        alert("Please select at least one menu.");
//        return;
//    }

//    console.log("Checked Values:", checkedValues);

//    // Send to server via AJAX
//    $.ajax({
//        url: '/master/saveSelectedMenus',
//        type: 'POST',
//        data: JSON.stringify({ menus: checkedValues, user: $('#user').val() }),
//        contentType: 'application/json',
//        success: function (response) {
//            alert("Saved successfully!");
//        },
//        error: function (err) {
//            console.error(err);
//            alert("Error saving menu rights.");
//        }
//    });
//}


$(document).ready(() => {
    $("#ddlClient").select2();
})

function checkAmt(input) {
    // Remove any character that's not digit or dot
    input.value = input.value.replace(/[^0-9.]/g, '');

    // Only allow one dot
    const parts = input.value.split('.');
    if (parts.length > 2) {
        input.value = parts[0] + '.' + parts[1];
    }

    // Allow only 1 digit after decimal
    if (parts[1]?.length > 2) {
        input.value = parts[0] + '.' + parts[1].substring(0, 2);
    }
}

const SaveData = () => {
    let val = Validation();
    if (val == "") {
        Save();
    
    }

    else
        alert(val);
}


const Save = () => {
    let url = localStorage.getItem('Myurl') + "/Master/SaveFrequency";
    let data = {
        Id: $("#HidId").val(),
        ClientCode: $("#ddlClient").val(),
        Frequency: $("#txtFrequency").val(),
        StartTime: $("#startTime").val(),
        EndTime: $("#endTime").val(),

    }

    Common(url, data, '', '', '', '');
}

const Cleardata = () => {
    $("#HidId").val(0);
    //$("#ddlClient").val(0);
    $("#txtFrequency").val('');
    $("#startTime").val('');
    $("#endTime").val('');
    $("#btnsave").text('Save');
    Showdata();
}

const Showdata = () => {
    $("#HidId").val(0);
  
    $("#txtFrequency").val('');
    $("#startTime").val('');
    $("#endTime").val('');
    $("#btnsave").text('Save');
    $.ajax({
        url: localStorage.getItem('Myurl') + "/Master/GetFrequency",
        type: 'Get',
        data: { ClientCode: $("#ddlClient").val() },
        success: (data) => {
            var data = JSON.parse(data);
            CreateTableFromArray(data, 'printDiv');
        },
        error: (data) => {
            console.log(data);
        }

    })
}


const Delete = (Id) => {

    let res = confirm("Are sure to Delete this !! ")
    if (!res) {
        return;
    }
    Cleardata();
    let url = localStorage.getItem('Myurl') + "/Master/DeleteFrequency";
    let data = {
        Id: Id

    }

    Common(url, data, '', '', '', '');

}
function Edit(Id) {
    Idd = $("#Hid_Id" + Id + "").text()
    ClientCode = $("#ClientCode" + Id + "").text()
    FromTime = $("#FromTime" + Id + "").text()
    ToTime = $("#ToTime" + Id + "").text()
    TourName = $("#FrequencyName" + Id + "").text()
    $("#HidId").val(Idd);
    $("#ddlClient").val(ClientCode);
    $("#txtFrequency").val(TourName);
    $("#startTime").val(FromTime);
    $("#endTime").val(ToTime);
    $("#btnsave").text('Update');

}
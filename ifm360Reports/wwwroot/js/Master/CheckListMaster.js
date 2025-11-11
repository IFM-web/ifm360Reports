
$(document).ready(() => {
    $("#ddlClient").select2();
    $("#HeaderName").select2();

    $('#Datatype').select2({
        placeholder: "Select",
        closeOnSelect: false,
        width: '100%',// Optional: ensures it matches form-control width
        templateResult: formatOptionWithCheckbox,
        templateSelection: formatSelectedOption
    });
    $('#Datatype').on('select2:select select2:unselect', function (e) {
        $(this).select2('close');
        $(this).select2('open');
    });
})


function formatOptionWithCheckbox(option) {
    if (!option.id) {
        return option.text;
    }

    // Create a checkbox and set checked status if selected
    const isSelected = $('#Datatype').val()?.includes(option.id);

    const $checkbox = $('<span><input type="checkbox" style="margin-right: 5px;" /> ' + option.text + '</span>');

    if (isSelected) {
        $checkbox.find('input').prop('checked', true);
    }

    return $checkbox;
}


function formatSelectedOption(option) {
    return option.text;
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
    let url = localStorage.getItem('Myurl') + "/Master/SaveCheckListMaster";
    let data = {
        Id: $("#HidId").val(),
        ClientCode: $("#ddlClient").val(),
        HeaderId: $("#HeaderName").val(),
        CheckListName: $("#CheckListName").val(),
        DataType: $("#Datatype").val(),
        DataTypeNames: $("#Datatype option:selected").text(),

    }

    Common(url, data, '', '', '', '');

}

const Cleardata = () => {
    $("#HidId").val(0);
    $("#CheckListName").val('');
    $("#Datatype").val(0);
    $("#Datatype").trigger('change')
    $("#btnsave").text('Submit');
    Showdata();
}

const Showdata = () => {
    
    $.ajax({
        url: localStorage.getItem('Myurl') + "/Master/GetCheckListMaster",
        type: 'Get',
        data: { ClientCode: $("#HeaderName").val() },
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
   
    let url = localStorage.getItem('Myurl') + "/Master/DeleteCheckListMaster";
    let data = {
        Id: Id

    }

    Common(url, data, '', '', '', '');

}
function Edit(Id) {
    Idd = $("#Hid_Id" + Id + "").text()
    ClientCode = $("#ClientCode" + Id + "").text()
    ChecklistName = $("#ChecklistName" + Id + "").text()
    DataType = $("#Hid_DataType" + Id + "").text()
 
    $("#HidId").val(Idd);
    $("#ddlClient").val(ClientCode);
    $("#CheckListName").val(ChecklistName);
    let arrIds = DataType.split(',').map(s => s.trim());
    $("#Datatype").val(arrIds);

    $("#Datatype").trigger('change');
  
    $("#btnsave").text('Update');

}

function BindHeader() {
   
    $.ajax({
        url: localStorage.getItem('Myurl') + "/Master/CheckListHeader",
        type: 'Get',
        data: { ClientCode: $("#ddlClient").val() },
        success: (data) => {
            var data = JSON.parse(data);
            var dwonlist = $("#HeaderName");
            dwonlist.empty();
            for (var e of data) {
                dwonlist.append(`<option value="${e.Id}">${e.Name}</option>`)
            }
            Cleardata();
        },
        error: (data) => {
            console.log(data);
        }

    })
}


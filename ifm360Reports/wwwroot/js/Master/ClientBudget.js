
$(document).ready(() => {
    //$("#ddlClient").select2();
})
let myurl = localStorage.getItem('Myurl');
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
    let url = localStorage.getItem('Myurl') +"/Master/SaveClient";
    let data = {
        Id: $("#HidId").val(),
        ClientCode: $("#ddlClient").val(),
        Amount: $("#txtAmt").val(),
        SiteID: $("#ddlSite").val(),
       
    }

    Common(url, data, '', '', '', '');
}

const Cleardata = () => {
    $("#HidId").val(0);
        //$("#ddlClient").val(0);
    $("#txtAmt").val('');
    $("#btnsave").text('Save');
    Showdata();

  
}

const Showdata = () => {
    $.ajax({
      url : localStorage.getItem('Myurl') + "/Master/GetClient",
        type: 'Get',
        data: { ClientCode: $("#ddlSite").val() },
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

   let res= confirm("Are sure to Delete this !! ")
    if (!res) {
        return;
    }
    Cleardata();
    let url = localStorage.getItem('Myurl') + "/Master/DeleteClientBugdet";
        let data = {
            Id:Id

        }

        Common(url, data, '', '', '', '');
    
}
function Edit(Id) {
    Idd = $("#Hid_Id" + Id + "").text()
    ClientCode = $("#ClientCode" + Id + "").text()
    siteId = $("#Hid_siteId" + Id + "").text()
    Amount = $("#Amount" + Id + "").text()
    $("#HidId").val(Idd);
    $("#ddlClient").val(ClientCode);
    $("#txtAmt").val(Amount);
    $("#ddlSite").val(siteId);
    $("#btnsave").text('Update');

}

function bindsitetoclient() {
    $.ajax({

        url: myurl + '/Reports/BindSitetoClient',
        type: 'post',
        data: { clietnid: $("#ddlClient").val(), },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#ddlSite');
            dropdown.empty();
            dropdown.append($("<option value='0'>Select</option>"));
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].AsmtId).text(data[i].AsmtName));
            }

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}

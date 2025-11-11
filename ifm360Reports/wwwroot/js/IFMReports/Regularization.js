

$(document).ready(function () {
    Month
    var month = new Date().getMonth() + 1;
    $("#Month").val(month);
    loadGridData();
})
let myurl = localStorage.getItem('Myurl');
function exportexcel(type, fn, dl) {
    var ddd = $("#txtpagename").val();
    var elt = document.getElementById('data-table');
    filename = ddd;
    console.log(filename);
    console.log(elt);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}


function loadGridData()
{
    $(".preloader").show();
    var Year = $("#Year").val();
    var Month = $("#Month").val();


    $.ajax({
        url: myurl + '/Reports/GetRegularization',
        type: 'GET',
        data: {
            Year: Year,
            Month: Month,
        },
        success: function (data) {
            $(".preloader").hide();
            $('#data-table tbody').empty();
            if (data && data.length > 0) {
                var sno = 1;
                var Data = JSON.parse(data);
                console.log(Data);
                for (var i = 0; i < Data.length; i++) {
                    var row = '<tr>';
                    row += '<td>' + sno++ + '</td>';
                    row += '<td>' + Data[i].CompanyDesc + '</td>';
                    row += '<td>' + Data[i].ClientCode + '</td>';
                    row += '<td>' + Data[i].ClientSiteName + '</td>';
                    row += '<td>' + Data[i].EmpID + '</td>';                   
                    row += '<td>' + Data[i].EmpName + '</td>';
                    row += '<td>' + Data[i].Designation + '</td>';
                    row += '<td>' + Data[i].ShiftDetails + '</td>';
                    row += '<td>' + Data[i].Date + '</td>';
                    row += `<td> ${Data[i].ApplyDate ||''} </td>`;
                    row += '<td>' + Data[i].FromTime + '</td>';
                    row += '<td>' + Data[i].ToTime + '</td>';
              
                
                    row += '<td>' + Data[i].ApprovalStatus + '</td>';
                    row += `<td> ${Data[i].ApprovedByID || ''} </td>`;
                    row += `<td> ${Data[i].ApprovedByName || ''} </td>`;
                    row += `<td> ${Data[i].Remarks || ''} </td>`;
                    row += `<td><button onclick="DeleteOrUpdate('StatusUpdate',${Data[i].Autoid},'${Data[i].ApprovalStatus}')" class="btn btn-primary">Update Status</button></td>`;
                    row += `<td>${Data[i].ApprovalStatus =='Pending'?`<button onclick="DeleteOrUpdate('Delete',${Data[i].Autoid},'${Data[i].ApprovalStatus}')" class="btn btn-danger">Delete</button>`:` `}</td>`;
                    row += '</tr>';
                    row += '</tr>';
                    $('#data-table tbody').append(row);
                }
            } else {
                $(".preloader").hide();
                $('#data-table tbody').append('<tr><td colspan="7" class="text-center">No data available.</td></tr>');
            }
        },
        error: function (xhr, status, error) {
            $(".preloader").hide();
            console.log('Error loading data:', error);
            $('#data-table tbody').html('<tr><td colspan="7" class="text-center">Error fetching data.</td></tr>');
        }
    });


}

function DeleteOrUpdate(type, AutoId, status) {

    confirmationMessage = type === 'Delete' ? 'Are you sure you want to delete this record?' : 'Are you sure you want to update the status?';
    confirmation = confirm(confirmationMessage);
    if (!confirmation) {
        return;
    }

    $.ajax({
        url: myurl + '/Reports/RegularizationDeleteAndstatusUpdate',
        type: 'post',
        data: {
            type: type, AutoId: AutoId, status: status == "Approved" ? 0 : 1
        },
        success: function (data) {
            var data = JSON.parse(data);
            alert(data.Message);
            loadGridData();
        },
        error: function (xhr, status, error) {
            $(".preloader").hide();
            console.log('Error loading data:', error);
            $('#data-table tbody').html('<tr><td colspan="7" class="text-center">Error fetching data.</td></tr>');
        }
    });


}


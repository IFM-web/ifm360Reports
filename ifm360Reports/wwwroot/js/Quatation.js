

$(document).ready(function () {
    $("#quotation").hide();
    $("#tbleiddata").hide();
})

var ticketdetails = $("#ticketdetails");
var addqoutation = $("#addqoutation");
var itemsquotationTable = $("#itemsquotation");

function tbliddata() {
    
    var itemcodeid = $('#itemcodeid').find("option:selected").val();
    var tickitnoid = $('#tickitnoid').find("option:selected").val();

    if (itemcodeid != 0 && tickitnoid != 0) {
        $("#tbleiddata").show();
    } else {
       /// alert('Record Not Available');
    }
}


 




function BindBranch() {
    //var tickitnoid = $("#tickitnoid").text();

    var tickitnoid = $('#tickitnoid').val();
    $.ajax({
        url: '/CMS/BindBranch',
        //  url: '/Reports/SearchGetOutOfRange',
        type: 'post',
        data: {
            tickitnoid: tickitnoid

        },
        success: function (data) {
       

            data = JSON.parse(data);
            console.log(data)
            if (data.length > 0) {
                ticketdetails.removeClass('d-none');
                addqoutation.addClass('d-none');
                itemsquotationTable.addClass('d-none');

                $("#customernameid").val(data[0].ClientName);
                $("#firstid").show();
                $("#firstid1").show();

                $("#tickitdateid").val(data[0].TicketDate);
                $("#branchid").val(data[0].AsmtName);
                $("#descriptionid").val(data[0].TicketDescription);
                $("#observationid").val(data[0].TicketObservation);
                $("#quirmentid").val(data[0].TicketRequirements);
                tbliddata();
            }
            else {
                ticketdetails.addClass('d-none');
                $("#section2").hide();
                $("#itemcode").text('');
                $("#itemname").text('');
                $("#itemunit").text('');
                $("#itemrate").text('');
                $("#itemGST").text('');
                $("#qty").val('');
            }
           
        },
        error: function (data) {

            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });

}


function AddQoutationDiv() {
    addqoutation.removeClass('d-none')
    

}


function BindCode() {
    var itemcodeid = $('#itemcodeid').val();
    $.ajax({
        url: '/CMS/BindCode',
        //  url: '/Reports/SearchGetOutOfRange',
        type: 'post',
        data: {
            itemcodeid: itemcodeid

        },
        success: function (data) {
            console.log(data);
            if (data != '[]') {
                data = JSON.parse(data);
                var rowlen = parseInt($('.companybody tr').length);
                var row = '';

                for (var i = 0; i < data.length; i++) {

                    $("#itemcode").val(data[i].ItemCode);
                    $("#itemname").val(data[i].itemDescription);
                    $("#itemunit").val(data[i].Unit);
                    $("#itemrate").val(data[i].Rate);
                    $("#itemGST").val('18%');
                    $("#qty").val(1);
                    
                    $("#grossamt").val(data[i].Rate +data[i].Rate * 18/100);





                }


            }




        }
        
    });

}












var allitem = [];

function deleteRow(id) {
    
    console.log(allitem)
    if (id !== -1) {
        allitem.splice(id, 1);
        showTable(allitem)
    }
}
function addtoquotation() {
   // itemsquotationTable.removeClass('d-none');
    var items = {
        ItemCode: $("#itemcode").val(),
        Itemname: $("#itemname").val(),
        Itemunit: $("#itemunit").val(),
        Itemrate: $("#itemrate").val(),
        ItemGst: $("#itemGST").val(),
        ItemQty: $("#qty").val(),
        grossAmount: $("#grossamt").val(),
        TicketNO: $("#tickitnoid").val(),
    }

    allitem.push(items);
  
    alert('Item Added SuccessFully')
    console.log(allitem);
   
    $("#itemcode").val('');
    $("#itemname").val('');
    $("#itemunit").val('');
    $("#itemrate").val('');
    $("#itemGST").val('');
    $("#qty").val('');
    $("#grossamt").val('');
   
    showTable(allitem)
    
   
}

document.getElementById("qty").addEventListener("input", function (e) {
    this.value = this.value.replace(/[^0-9]/g, ""); 

    var qty = this.value;
    var Itemrate = $("#itemrate").val();
    var totalcost = qty * Itemrate

    var gstamt = totalcost * 18 / 100
    $("#grossamt").val(qty * Itemrate + gstamt);
});



function showTable(allitem) {
    if (allitem.length > 0) {
        $("#itemsquotation").removeClass('d-none');        
        $("#btnsave").removeClass('d-none');

        $("#quotationTableBody").empty();
        var row = '';
        for (var i = 0; i < allitem.length; i++) {
            row += `  <tr>
    <td>${i + 1}</td>
    <td class='ItemCode'>${allitem[i].ItemCode}</td>
    <td><span class='itemDescription'>${allitem[i].Itemname}</span></td>
    <td><span class='GST'>18%</span></td>
    <td>${allitem[i].ItemQty}</td>
    <td>${allitem[i].Itemunit}</td>
    <td class='rate'>${allitem[i].Itemrate}</td>
    <td class='grossamt'>${allitem[i].grossAmount}</td>
    <td>
      <button class='btn btn-danger' onclick="deleteRow('${i}')">
        Remove
      </button>
    </td>
  </tr>

  `;

        }
        $("#quotationTableBody").append(row);
    }
    else {
        $("#quotationTableBody").empty();
        $("#btnsave").addClass('d-none');
        $("#itemsquotation").addClass('d-none');
    }

  
   
}

function SaveData() {
    var newdata = JSON.stringify(allitem);
    $.ajax({
        url: '/CMS/InsertQoutation',
        data: { data: newdata },
        type: 'post',
        
        success: function (data) {
            alert(data.message);
            location.reload();


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}



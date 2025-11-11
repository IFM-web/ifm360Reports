

function Validation() {
    var msg = "";
    var charregex = /^[a-zA-Z\s]+$/;
    var intregex = /^[0-9]+$/;
    var charintregex = /^[a-zA-Z0-9\s]+$/;


    $(".mandatory").each(function () {
        if ($(this).val() == "" || $(this).val() == "0") {
            var name = $(this).attr('name')
            console.log($(this).val());
            msg += "" + name + "  Required !!\n";
        }
    });

    $(".checktaxes").each(function () {
        if ($(this).val() == "") {
            var name = $(this).attr('name')
            console.log($(this).val());
            msg += "" + name + "  Required !!\n";
        }
    });
    $(".chkint").each(function () {
        if (!intregex.test($(this).val())) {
            var name = $(this).attr('name')
            msg += "Enter Valid " + name + "!!\n";
        }
    });
    $(".chkcharint").each(function () {
        if (!charintregex.test($(this).val())) {
            var name = $(this).attr('name')
            msg += "Enter Valid " + name + "!!\n";
        }
    });
    $(".chkchar").each(function () {
        if (!charregex.test($(this).val())) {
            var name = $(this).attr('name')
            msg += "Enter Valid " + name + "!!\n";
        }
    });
    return msg;
}





function CommonAjax(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            console.log(data);
            if (data.Message != "") {
                swal("Message", data.Message, data.Status == "Success" ? "success" : "error");
            }

            if (data.Message == "Success") {
                clear();
            }


            $(divid).empty();
            if (data.Data != undefined) {
                CreateTableFromArray(data.Data, divid);
                dtable();
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

function CommonAjax1(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        //data: var_data,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            console.log(data);
            if (data.Message != "") {
                swal("Message", data.Message, data.Message == "Success" ? "success" : "error");
            }
            if (data.Message == "Success") {
                clear();
            }
            CreateTableFromArray(data.Data, divid);
            dtable();
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
function CommonAjaxx(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            console.log(data);
            //if (data.Message != "") {
            //    swal("Message", data.Message, data.Status == "Success" ? "success" : "error");
            //}
            //if (data.Message == "Success") {
            //    clear();
            //}
            //$(divid).empty();
            if (data.Data != undefined) {
                CreateTableFromArray(data.Data, divid);
                dtable();
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


function BindtrnTable(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            console.log(data.Data);
            if (divid == "1") {
                if (data.Status == "error") {
                    alert(data.Message);

                }


                else {

                    var array = data.Data;
                    console.log(array);
                    var row = ""
                    for (var i = 0; i < array.length; i++) {
                        row += "<tr id='row" + i + "'><td><span class='barcode' id='txtbarcode'>" + array[i].barcode + "</td><td><span class='productname' id='txtproductname'>" + array[i].productname + "</span></td><td style='display:none'><span class='itemid' id='txtitem'>" + array[i].ItemID + "</span></td><td><span class='item' id='txtitemcode" + i + "'> " + array[i].ItemCode + "</span></td></tr>";


                    }
                }

                $(".tbl").append(row);
            }
            else if (divid == 2) {
                if (data.Data.length > 0) {
                    $("#txtquantityof").val(data.Data[0]["Quantity"]);
                }
                else {
                    $("#txtquantityof").val('');
                }

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


function CommonAjaxgrid(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            console.log(data);

            //if (data.Message != "") {
            //    swal("Message", data.Message, data.Message == data.Message ? "success" : "error");
        //}
            //if (a == 5) {
            //    console.log(data)
            //}
            //else {
            //    $(divid).innerHTML = ''
            //}

            //if (data.Message == "Success") {
            //    clear();
            //}



            $(divid).empty();
            if (data.Data != undefined && data.Message == '') {
                $("#txtrno").val(GenereteReceiptNo());
                CreateTableFromArray(data.Data, divid);
                dtable();

                var element1 = document.getElementById("btnSubmit");


                if (element1 !== null) {
                    element1.disabled = true;
                }

                calculae();
                ShowDetails();



            }

            else if (data.Message == '') {
                $("#Printdiv").empty();
                $("#PrintDetails").empty();


                var element1 = document.getElementById("amtresullist");
                element1.style.visibility = "hidden";
                $("#txtenteramt").val("");


                swal("Message", "Record not Found", "Record not Found" == "Record not Found" ? "error" : "error");

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
function CommonAjaxReport(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            console.log(data);

            //if (data.Message != "") {
            // swal("Message", data.Message, data.Message == data.Message ? "success" : "error");
            //}
            
            //if (a == 5) {
            //    console.log(data)
            //}
            //else {
            //    $(divid).innerHTML = ''
            //}

            //if (data.Message == "Success") {
            //    clear();
            //}



            $(divid).empty();
            if (data.Data != undefined && data.Data.length >1) {
                // $("#txtrno").val(GenereteReceiptNo());
                CreateTableFromArray(data.Data, divid);
                dtable();
                       

            }

            else { 
            $("#Printdiv").empty();   
               swal("Message", "Record not Found", "Record not Found" == "Record not Found" ? "error" : "error");

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









function CommonAjaxBom(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            console.log(data);
            if (data.Message != "") {
                swal("Message", data.Message, data.Message == "Success" ? "success" : "error");
            }
            if (data.Message == "Success") {
                clear();
            }
            if (data.Data != undefined) {
                CreateTableFromArray(data.Data, divid);
                //dtable();
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


function Common(var_url, var_data, var_type, var_ct, var_dt, type) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            var data = JSON.parse(data);
            alert(data[0].Message);
            if (data[0].Status == "Success") {
                Cleardata();
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


function BindDropdown(var_url, var_data, var_type, var_ct, var_dt, var_id) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        type: var_type,
        url: var_url,
        data: var_data,
        async: false,
        success: function (json, result) {
            $(var_id).empty();
            json = json || {};
            $(var_id).append('<option value="0">Select</option>');
            for (var i = 0; i < json.length; i++) {
                $(var_id).append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            }

        },
        error: function () {
            alert("Data Not Found");
        }
    });
}


function CreateTableFromArray(arrItems, divid) {
    let col = [];
    for (let i = 0; i < arrItems.length; i++) {
        for (let key in arrItems[i]) {
            if (col.indexOf(key) === -1) {
                col.push(key);
            }
        }
    }

    // Create a Table.
    let table = document.createElement("table");
    table.setAttribute('id', 'data-table');
    table.setAttribute('class', 'table table-bordered');
    // Create table header row.

    let tr = table.insertRow(-1);		// table row.

    for (let i = 0; i < col.length; i++) {
        let th = document.createElement('th');  // table header.
        //console.log(col[i]);
        th.innerHTML = col[i];
        var result = col[i].includes("Hid_") == true ? 'none' : '';
        tr.appendChild(th);
        th.setAttribute('style',
            'font:18px Calibri;border: solid 1px #DDD;' +
            'border-collapse: collapse; font-weight:bold;' +
            'padding: 2px 3px; text-align: center;' +
            'display:' + result + ';'
        );
    }

    // Add JSON to the table as rows.
    for (let z = 0; z < arrItems.length; z++) {
        tr = table.insertRow(-1);

        for (let j = 0; j < col.length; j++) {
            let tabCell = tr.insertCell(-1);
            var result = col[j].includes("Hid_") == true ? 'none' : '';
            tabCell.innerHTML = arrItems[z][col[j]];
            tabCell.setAttribute('style',
                'font:18px Calibri;border: solid 1px #DDD;' +
                'border-collapse: collapse; ' +
                'padding: 2px 3px; text-align: center;' +
                'display:' + result + ';'
            );
            tabCell.setAttribute('class', "" + col[j].replace(" ", "") + "");
            tabCell.setAttribute('id', "" + col[j].replace(" ", "") + "" + z + "");

        }
    }

    // Show the table.
    let container = document.getElementById('' + divid + '');
    container.innerHTML = '';
    container.appendChild(table);


    //jQuery.noConflict();
    //(function ($jq) {
    //    var myTable = $jq("#data-table");
    //    var thead = myTable.find("thead");
    //    var thRows = myTable.find("tr:has(th)");
    //    if (thead.length === 0) {  //if there is no thead element, add one.
    //        thead = $jq("<thead></thead>").appendTo(myTable);
    //    }
    //    var copy = thRows.clone(true).appendTo("thead");
    //    thRows.remove();
    //    myTable.dataTable();
    //})(jQuery);


}



$('#filterInput').on('keyup', function () {
    let filter = $(this).val().toUpperCase();


    $('#data-table tbody tr').each(function (index) {
        if (index === -1) return;

        let row = $(this);
        let match = false;

        // Check each cell in the row
        row.find('td').each(function () {
            if ($(this).text().toUpperCase().indexOf(filter) > -1) {
                match = true;
                return false;
            }
            else {
                row.add('<tr><td>Not Found</td></tr>')
            }
        });


        row.toggle(match);
    });
});








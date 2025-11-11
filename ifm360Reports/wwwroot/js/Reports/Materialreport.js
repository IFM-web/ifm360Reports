$(document).ready(() => {

    let date = new Date().getMonth() + 1;
  
    $("#month").val(date);
    SearchData()
});
function SearchData() {
    $(".preloader").show();
    $.ajax({
        url: localStorage.getItem('Myurl') + "/Reports/GetAllMaterialreport",
        type: "get",
        data: { Month: $("#month").val(), Year: $("#Year").val()},
        success: function (data) {
            $(".preloader").hide();
            var data = JSON.parse(data);
            if (data.length!=0) {
                console.log(data);
                let mappedData = data.map(obj => {
                    return Object.fromEntries(
                        Object.entries(obj).map(([key, value]) => [key, value == null ? 0 : value])
                    );
                });
             
                CreateTableFromArray(mappedData, 'prindDiv');
            }
            else {
                alert("Record Not Found !!")
                $("#prindDiv").empty();
            }
        },
        error: function (error) {
            $(".preloader").hide();
            console.log(error.message);
        }
    });
}

function exportExcel(type, fn, dl) {
    var data = $(".companybody").val()
    let month = $("#month option:selected").text()
    let Year = $("#Year option:selected").text()
    var ddd = $("#txtpagename").val();
    var elt = document.getElementById('data-table');
    filename = "Consolidated Material Report -" + month+"-" + Year
    console.log(filename);
    console.log(elt);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });


    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (filename + '.' + (type || 'xlsx')));
}

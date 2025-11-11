$(document).ready(()=> {

   // SHowData();

});

let myurl = localStorage.getItem('Myurl');
function bindRegion() {
    var list = $("#Company").val();
    $.ajax({

        url: myurl + '/Home/bindRegion',
        type: 'post',
        data: {
            id: list
        },
        success: function (data) {

            var data = JSON.parse(data);

            var dropdown = $('#Region');
            dropdown.empty();
            dropdown.append('<option value="0">Select</option>')
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].HrLocationCode).text(data[i].HrLocationDesc));
            }
            bindbranch()

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}

function bindbranch() {
    $.ajax({

        url: myurl + '/Home/bindBranch',
        type: 'post',
        data: { id: $("#Company").val(), locid: $("#Region").val() },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#branch');
            dropdown.empty();
           /* dropdown.append('<option value="All">All</option>')*/
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].LocationAutoID).text(data[i].LocationCode));
            }

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}


//function BindCustomer() {

//    $("#customSelect").val('');
//    $("#customSelectName").val('');
//    let url = localStorage.getItem("Myurl") + '/Admin/BindCustomer';
//    $.ajax({
//        url: url,
//        type: 'post',
//        data: { Company: $("#Company").val() },
//        success: (data) => {
//            var data = JSON.parse(data);
//            var i = 0;
//            var container = document.getElementById("checkboxContainer");
//            container.innerHTML = '';

//            for (var e of data) {
//                const dd = `
//      <div class="form-check ms-2" style="margin-left: 4px;">
         
//        <input class="form-check-input chkItem" type="checkbox" value="${e.Id}" id="chk${i}">
//        <label class="form-check-label" style="font-weight: 100;" for="chk${i}">${e.Name}</label>
//      </div>
//    `;
//                container.innerHTML += dd;
//                ++i;
//            }
//        },
//        error: (data) => {
//            console.log(data);
//        }


//    });
//}


//const displayBox = document.getElementById('customSelect');
//const displayBoxName = document.getElementById('customSelectName');
//const menu = document.getElementById('checkboxContainer');
//const wrapper = document.querySelector('.custom-multiselect');


//displayBoxName.addEventListener('click', function () {
//    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
//});


//document.addEventListener('click', function (event) {
//    if (!wrapper.contains(event.target)) {
//        menu.style.display = 'none';
//    }
//});


//menu.addEventListener('change', function () {
//    const selectedCheckboxes = Array.from(menu.querySelectorAll(".chkItem:checked"));


//    const selectedValues = selectedCheckboxes.map(cb => cb.value);
//    displayBox.value = selectedValues.join(",");


//    const selectedTexts = selectedCheckboxes.map(cb => {
//        const label = cb.nextElementSibling;
//        return label ? label.innerText.trim() : "";
//    });
//    displayBoxName.value = selectedTexts.join(",");
//});


function SaveData() {
    let val = Validation();
    if (val != '') {
        alert(val);
        return;
    }
    Data = {
        UserId: $("#Userselect").val(),
        Company: $("#Company").val(),
        Region: $("#Region").val(),
        Branch: $("#branch").val(),
       
    };

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/SaveUserMapping',
        type: 'Post',

        data: Data,
        success: (data) => {
            var data = JSON.parse(data);
            if (data[0].Status == "Success") {
                alert(data[0].Message);
                Clear();
                SHowData();
            }
            else
                alert(data[0].Message);
        },
        error: (data) => {
            console.log(data);
        }
    })
}

function Clear() {
    //$("#Userselect").val(0);
    //$("#Userselect").trigger('change');
    //$("#Company").val(0);
    //$("#Company").trigger('change');
    //$("#Region").val(0);
    //$("#Region").trigger('change');
    $("#branch").val(0);
    $("#branch").trigger('change');

}
function SHowData() {

  

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/ShowUserMapping',
        type: 'Get',

        data: { UserId: $("#Userselect").val()},
        success: (data) => {
            CreateTableFromArray(JSON.parse(data), 'printdiv');
        },
        error: (data) => {
            console.log(data);
        }
    })
}

function BOMSHowData(UserId, Company, Region) {
 
  

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/UserBranchList',
        type: 'Get',

        data: { UserId: UserId, Company: Company, Region: Region },
        success: (data) => {
            CreateTableFromArray(JSON.parse(data), 'BOMDIV');
        },
        error: (data) => {
            console.log(data);
        }
    })
}

function DeleteBOMSHowData(Id) {

  

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/DeleteBranchMap',
        type: 'post',

        data: {Id:Id},
        success: (data) => {
            var data = JSON.parse(data);
            if (data[0].Status == "Success") {
                alert(data[0].Message);
                BOMSHowData(data[0].UserId, data[0].CompanyCode, data[0].Region);
                SHowData()
                $("#exampleModal").modal("show");

            }
            else
                alert(data[0].Message);
        },
        error: (data) => {
            console.log(data);
        }
    })
}

$(".close").on("click", () => {
    $("#exampleModal").modal("hide");
})
function formatOption(option) {
    if (!option.id) return option.text;

    // Check if this option is already selected
    var isSelected = $(option.element).prop("selected");

    // Create checkbox with checked state
    var $checkbox = $('<span><input type="checkbox" style="margin-right:5px;" ' +
        (isSelected ? 'checked' : '') + '/>' + option.id + ' - ' + option.text + '</span>');

    return $checkbox;
}


//function formatOption(option) {
//    if (!option.id) return option.text;

//    var selected = $(option.element).prop("selected");

//    var $result = $(
//        '<span><input type="checkbox" class="branch-checkbox" data-id="' + option.id +
//        '" style="margin-right:5px;" ' + (selected ? 'checked' : '') + '/>' +
//        option.text + '</span>'
//    );

//    return $result;
//}

$('#branch').select2({
    placeholder: "Select branch",
    closeOnSelect: false,
    templateResult: formatOption,
    templateSelection: function (data) {
        // Hide "All" from selection display
        if (!data.id || data.id === "All") return '';
        return data.text;
    }
});

// Delegated click handler for checkboxes
//$(document).on('click', '.branch-checkbox', function (e) {
//    e.stopPropagation();
//    e.preventDefault(); // stop default toggle

//    var $cb = $(this);
//    var id = String($cb.data('id'));
//    var $select = $('#branch');
//    var vals = $select.val() || [];

//    if (id === 'All') {
//        // "All" logic: toggle all options except "All"
//        var allVals = $select.find('option').map(function () { return this.value; }).get()
//            .filter(v => v !== 'All');

//        var allSelected = allVals.every(v => vals.includes(v));

//        if (!allSelected) {
//            $select.val(allVals).trigger('change');
//        } else {
//            $select.val([]).trigger('change');
//        }
//    } else {
//        // Single option toggle
//        if (vals.includes(id)) {
//            vals = vals.filter(v => v !== id);
//            $cb.prop('checked', false);
//        } else {
//            vals.push(id);
//            $cb.prop('checked', true);
//        }
//        $select.val(vals).trigger('change');
//    }
//});

// Sync checkboxes whenever selection changes (keyboard, programmatic, or internal changes)
//$('#branch').on('change', function () {
//    var vals = $(this).val() || [];
//    var allVals = $(this).find('option').map(function () { return this.value; }).get()
//        .filter(v => v !== 'All');
//    var allSelected = allVals.length && allVals.every(v => vals.includes(v));

//    $('.branch-checkbox').each(function () {
//        var $c = $(this);
//        var id = String($c.data('id'));
//        if (id === 'All') {
//            // check "All" if all options selected
//            $c.prop('checked', allSelected);
//        } else {
//            // check individual options
//            $c.prop('checked', vals.includes(id));
//        }
//    });
//});


// When selection changes, re-render to update checkboxes
$('#Company').select2();
$('#Company').select2();
$('#Region').select2();

$('#Userselect').select2();

$("#branch").on("change", function (e) {
    if ($(this).prop("selectedIndex") === 0) {
       
    }
});


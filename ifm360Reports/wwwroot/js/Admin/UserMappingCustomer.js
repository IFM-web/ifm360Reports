$(document).ready(() => {

   // SHowData();

});

let myurl = localStorage.getItem('Myurl');



function SaveData() {

    let val = Validation();
    if (val != '') {
        alert(val);
        return;
    }

    Data = {
        UserId: $("#Userselect").val(),    
        Customer: $("#Customer").val(),

    };

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/SaveCustomerMapping',
        type: 'Post',

        data: Data,
        success: (data) => {
            var data = JSON.parse(data);
            if (data[0].Status == "Success") {
                alert(data[0].Message);
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

function Delete(Id) {

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/DeleteCustomerMap',
        type: 'Post',

        data: {UserId:Id},
        success: (data) => {
            var data = JSON.parse(data);
            if (data[0].Status == "Success") {
                alert(data[0].Message);
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


$("#Userselect").on('change', () => {

    bindCUstomer();

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/ShowCustomerMapping',
        type: 'Get',

        data: { UserId: $("#Userselect").val(), },
        success: (data) => {
            CreateTableFromArray(JSON.parse(data), 'printdiv');
        },
        error: (data) => {
            console.log(data);
        }
    })
});
function formatOption(option) {
    if (!option.id) return option.text;

    // Check if this option is already selected
    var isSelected = $(option.element).prop("selected");

    // Create checkbox with checked state
    var $checkbox = $('<span><input type="checkbox" style="margin-right:5px;" ' +
        (isSelected ? 'checked' : '') + '/>' + option.id + ' - ' + option.text + '</span>');

    return $checkbox;
}


function formatOption(option) {
    if (!option.id) return option.text;

    // check if this option is selected
    var selected = $(option.element).prop("selected");

    // return checkbox + label
    return $('<span><input type="checkbox" style="margin-right:5px;" ' +
        (selected ? 'checked' : '') + '/>' + option.text + '</span>');
}

//$('#Customer').select2({
//    placeholder: "Select Customer",
//    closeOnSelect: false,   // don't close dropdown after each selection
//    templateResult: formatOption,
//    templateSelection: function (data) {
//        if (!data.id) return data.text;
//        return data.id + " - " + data.text;
//    }
//});


function formatOption(option) {
    if (!option.id) return option.text;

    var selected = $(option.element).prop("selected");

    var $result = $(
        '<span><input type="checkbox" class="branch-checkbox" data-id="' + option.id +
        '" style="margin-right:5px;" ' + (selected ? 'checked' : '') + '/>' +
        option.text + '</span>'
    );

    return $result;
}

$('#Customer').select2({
    placeholder: "Select Customer",
    closeOnSelect: false,
    templateResult: formatOption,
    templateSelection: function (data) {
        // Hide "All" from selection display
        if (!data.id || data.id === "All") return '';
        return data.text;
    }
});

// Delegated click handler for checkboxes
$(document).on('click', '.branch-checkbox', function (e) {
    e.stopPropagation();
    e.preventDefault(); // stop default checkbox toggle

    var $cb = $(this);
    var id = String($cb.data('id'));
    var $select = $('#Customer');
    var vals = $select.val() || [];

    if (id === 'All') {
        // Get all checkbox elements except "All"
        var $allCheckboxes = $('.branch-checkbox').not('[data-id="All"]');
        var allVals = $allCheckboxes.map(function () { return $(this).data('id'); }).get();

        var allSelected = allVals.every(v => vals.includes(String(v)));

        if (!allSelected) {
            // Select all
            $select.val(allVals).trigger('change');
            $allCheckboxes.prop('checked', true);
            $cb.prop('checked', true);
        } else {
            // Deselect all
            $select.val([]).trigger('change');
            $allCheckboxes.prop('checked', false);
            $cb.prop('checked', false);
        }

    } else {
        // Single checkbox toggle
        if (vals.includes(id)) {
            vals = vals.filter(v => v !== id);
            $cb.prop('checked', false);
        } else {
            vals.push(id);
            $cb.prop('checked', true);
        }

        $select.val(vals).trigger('change');

        // Update "All" checkbox if all others are selected
        var allIds = $('.branch-checkbox').not('[data-id="All"]').map(function () {
            return $(this).data('id');
        }).get();

        var allSelected = allIds.every(v => vals.includes(String(v)));
        $('.branch-checkbox[data-id="All"]').prop('checked', allSelected);
    }
});


//// Sync checkboxes whenever selection changes (keyboard, programmatic, or internal changes)
//$('#Customer').on('change', function () {
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


function bindCUstomer() {
    $.ajax({

        url: myurl + '/Admin/bindCustomertoMap',
        type: 'post',
        data: { Id: $("#Userselect").val() },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#Customer');
            dropdown.empty();
            dropdown.append('<option value="All">All</option>')
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].ClientCode).text(data[i].ClientName));
            }

        },
        error: function (error) {
            alert(error.massage);
        }
    })
}

$('#Userselect').select2();


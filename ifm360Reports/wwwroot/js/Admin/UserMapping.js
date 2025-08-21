$(document).ready(()=> {

    SHowData();

});

function BindCustomer() {

    $("#customSelect").val('');
    $("#customSelectName").val('');
    let url = localStorage.getItem("Myurl") + '/Admin/BindCustomer';
    $.ajax({
        url: url,
        type: 'post',
        data: { Company: $("#Company").val() },
        success: (data) => {
            var data = JSON.parse(data);
            var i = 0;
            var container = document.getElementById("checkboxContainer");
            container.innerHTML = '';

            for (var e of data) {
                const dd = `
      <div class="form-check ms-2" style="margin-left: 4px;">
         
        <input class="form-check-input chkItem" type="checkbox" value="${e.Id}" id="chk${i}">
        <label class="form-check-label" style="font-weight: 100;" for="chk${i}">${e.Name}</label>
      </div>
    `;
                container.innerHTML += dd;
                ++i;
            }
        },
        error: (data) => {
            console.log(data);
        }


    });
}


const displayBox = document.getElementById('customSelect');
const displayBoxName = document.getElementById('customSelectName');
const menu = document.getElementById('checkboxContainer');
const wrapper = document.querySelector('.custom-multiselect');


displayBoxName.addEventListener('click', function () {
    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
});


document.addEventListener('click', function (event) {
    if (!wrapper.contains(event.target)) {
        menu.style.display = 'none';
    }
});


menu.addEventListener('change', function () {
    const selectedCheckboxes = Array.from(menu.querySelectorAll(".chkItem:checked"));


    const selectedValues = selectedCheckboxes.map(cb => cb.value);
    displayBox.value = selectedValues.join(",");


    const selectedTexts = selectedCheckboxes.map(cb => {
        const label = cb.nextElementSibling;
        return label ? label.innerText.trim() : "";
    });
    displayBoxName.value = selectedTexts.join(",");
});


function SaveData() {
    
    Data = {
        UserId: $("#Userselect").val(),
        Customer: $("#customSelect").val(),
        CustomerName: $("#customSelectName").val(),
    };

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/SaveUserMapping',
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

function SHowData() {

  

    $.ajax({

        url: localStorage.getItem("Myurl") + '/Admin/ShowUserMapping',
        type: 'Get',

        data: {},
        success: (data) => {
            CreateTableFromArray(JSON.parse(data), 'printdiv');
        },
        error: (data) => {
            console.log(data);
        }
    })
}
  
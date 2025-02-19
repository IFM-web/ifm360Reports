﻿const ones = [
    "",
    "one",
    "two",
    "three",
    "four",
    "five",
    "six",
    "seven",
    "eight",
    "nine",
];
const teens = [
    "eleven",
    "twelve",
    "thirteen",
    "fourteen",
    "fifteen",
    "sixteen",
    "seventeen",
    "eighteen",
    "nineteen",
];
const tens = [
    "",
    "ten",
    "twenty",
    "thirty",
    "forty",
    "fifty",
    "sixty",
    "seventy",
    "eighty",
    "ninety",
];
const thousands = ["", "thousand", "lakh", "crore"];

function numberToWords(num) {
    if (num === "0") return "zero";

    let numStr = num.toString();
    let word = "";
    let n = numStr.length;

    if (n > 9) return "overflow"; // number out of bounds

    // Pad the number with leading zeros for easier parsing
    numStr = numStr.padStart(9, "0");

    // Split the number into groups of two
    let crore = numStr.slice(0, 2);
    let lakh = numStr.slice(2, 4);
    let thousand = numStr.slice(4, 6);
    let hundred = numStr[6];
    let ten = numStr.slice(7);

    if (parseInt(crore) > 0) {
        word += `${convertTwoDigits(crore)} crore `;
    }
    if (parseInt(lakh) > 0) {
        word += `${convertTwoDigits(lakh)} lakh `;
    }
    if (parseInt(thousand) > 0) {
        word += `${convertTwoDigits(thousand)} thousand `;
    }
    if (parseInt(hundred) > 0) {
        word += `${ones[hundred]} hundred `;
    }
    if (parseInt(ten) > 0) {
        word += convertTwoDigits(ten);
    }

    return word.trim();
}

function convertTwoDigits(num) {
    num = parseInt(num, 10);
    if (num < 10) return ones[num];
    if (num > 10 && num < 20) return teens[num - 11];
    let unit = num % 10;
    let ten = Math.floor(num / 10);
    return `${tens[ten]} ${ones[unit]}`.trim();
}

function convertRupeesPaise(amount) {
    let [rupees, paise] = amount.toString().split(".");

    let rupeesInWords = numberToWords(parseInt(rupees));
    let paiseInWords = paise ? convertTwoDigits(paise.padEnd(2, "0")) : "";

    let result = "";
    if (rupeesInWords) {
        result += `${rupeesInWords} rupees`;
    }
    if (paiseInWords) {
        result += ` and ${paiseInWords} paise`;
    }

    return result.trim();
}




function printData(id) {

    $.ajax({
        url: '/CMS/GetDatabyId',
        type: 'Post',
        data: {data: id},
        success: function (data) {
            var data1 = JSON.parse(data.data);
            var data2 = JSON.parse(data.data2);
            console.log(data);
            console.log(data2);
            print = '';



var htmlforprint =`<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Quotation</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            padding: 0;
        }
        h1, h2 {
            text-align: center;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        th, td {
            border: 1px solid #000;
            padding: 8px;
            text-align: left;
        }
        th {
            background-color: #f2f2f2;
            text-align: center;
        }
        .text-right {
            text-align: right;
        }
        .text-center {
            text-align: center;
        }
        .header-table, .footer-table {
            width: 100%;
            margin-top: 20px;
        }
        .header-table td, .footer-table td {
            border: none;
            padding: 5px 0;
        }
        .footer-row td {
            font-weight: bold;
        }
        .amount-words {
            margin-top: 10px;
            font-style: italic;
        }
        .logo {
            text-align: center;
            margin-bottom: 10px;
        }
        .logo img {
            max-width: 150px;
        }
      

  @media print {
   .print-footer1 {
    position: fixed;
    bottom: 0;
    left: 0;
    width: 100%;
    text-align: center;
    font-size: 12px;
    color: #333;
    border-top: 1px solid #ccc;
    padding: 5px 0;
    background: #fff;
  }
    @page {
                size: A4;
                margin: 6.20mm 6.20mm 6.20mm 6.20mm;             
                -webkit-print-color-adjust: exact;
              font-family: 'Times New Roman', Times, serif, sans-serif;
                  page-break-after: always;
                 
                    border:2px solid black;
                header,
                footer {
                    display: none;
                }
          


            }

            header,
            footer {
                display: none;
            }

  
}
 .print-footer, header{
      display: none;
 }

.no-print {
    display: none;
  }



    </style>
</head>
<body>
    <div class="logo">
        <img id="logoimag" src="https://localhost:7056/grouplreportingportal/GroupL.jfif" alt="Group L Logo">
    </div>
    <h2>Quotation</h2>
    <table class="header-table">
        <tr>
            <td><strong>Customer Name:</strong> ${data2[0].ClientName}</td>
            <td><strong>Ticket No.:</strong> ${id}</td>
        </tr>
        <tr>
            <td><strong>Branch:</strong> ${data2[0].AsmtName}</td>
            <td><strong>Ticket Date:</strong> ${data2[0].TicketDate}</td>
        </tr>
    </table>

    <table>
        <thead>
            <tr>
                <th>S.NO</th>
                <th>Item Code</th>
                <th>Item Name</th>
                <th>GST</th>
                <th>Qty</th>
                <th>Unit</th>
                <th>Rate</th>
                <th>Gross Amount</th>
            </tr>
        </thead>
        <tbody>`
            var row = '';
            var total = 0;
           
        for (var i = 0; i < data1.length; i++) {
            row += `
            <tr>
             <td>${i+1}</td>
                <td>${data1[i].ItemCode}</td>
                <td>${data1[i].ItemName}</td>
                <td>${data1[i].ItemGST}</td>
                <td>${data1[i].ItemQty}</td>
                <td>${data1[i].ItemUnit}</td>
                <td>${data1[i].ItemRate}</td>
                <td>${data1[i].ItemQty * data1[i].ItemRate}</td>
                </tr>

                
           `;
            total += data1[i].ItemQty * data1[i].ItemRate
            
            }

           var gst= total * 18 / 100;
            var gs = gst + total;
            var words = convertRupeesPaise(gs)


            var print=  htmlforprint + row +` </tbody>
        <tfoot>
            <tr>
                <td colspan="6" class="text-right"><strong>Total</strong></td>
                <td colspan="2" class="text-right">${total}</td>
            </tr>
            <tr>
                <td colspan="6" class="text-right"><strong>Management Fee</strong></td>
                <td colspan="2" class="text-right">0</td>
            </tr>
            <tr>
                <td colspan="6" class="text-right"><strong>GST 18%</strong></td>
                <td colspan="2" class="text-right">${gst}</td>
            </tr>
            <tr>
                <td colspan="6" class="text-right"><strong>Grand Total</strong></td>
                <td colspan="2" class="text-right">${total+gst}</td>
            </tr>
        </tfoot>
    </table>

    <p class="amount-words">
    
        <strong>Amount in Words:</strong> ${words}
    </p>
    <p class="print-footer1">
    This is a system generated quotation. Signature is not required <br>
<b>Thank you for your Business</b>
    </p>
</body>
</html>
`




 var popupwin = window.open('', '_blank', 'width=200', 'height=200', 'left:100', 'top:100');

    popupwin.document.open();
            popupwin.document.write(print);
   popupwin.document.close();
//popupwin.download;
// popupwin.click()


            var logoImage = popupwin.document.getElementById("logoimag");
            logoImage.onload = function () {
                popupwin.focus();
                popupwin.print();
                popupwin.close();
            };
 return true;
        },
        error: function (err) {
            alert(err.message);
        }

    });
//const { jsPDF } = window.jspdf;
//const doc = new jsPDF();

//const table = document.getElementById("dynamicTable");

//doc.autoTable({ html: datadiv });

//doc.save("table_data.pdf");

};
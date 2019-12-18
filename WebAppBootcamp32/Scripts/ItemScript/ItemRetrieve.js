$(document).ready(function () {
    loadData();
    $("#addItem").click(function () {
        $("#myModal").modal('hide');
        $('#Update').hide();
    });

    $("#closeItem").click(function () {
        $("#myModal").modal('hide');
        $('#Update').hide();
    });

    
});

function ValidateName(name) {
    var nameReg = /^[A-Z][a-z 0-9]+$/i;
    return nameReg.test(name);
}

function ClearScreen() {
    $('#name').val('');
    $('#stock').val('');
    $('#price').val('');
    $('#supplier').val('');
    $('#Update').hide();
    $('#Save').show();
}

$('#demoGrid').dataTable({
    "aoColumns": [null, null, null, null, null, { "bSortable": false }]
});

function loadData() {
    $.ajax({
        url: "/Items/itemJsonList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            var html = '';
            //const obj = JSON.parse(result);
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + key++ + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Stock + '</td>';
                html += '<td>' + item.Price + '</td>';
                html += '<td>' + item.Supplier.Name + '</td>';
                html += '<td><a href="#" onclick="return GetById(' + item.Id + ')">Edit</a> | <a href="#" onclick="return Delete(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            console.log(errormessage.responseText);
        }
    });
}

var Suppliers = []
function LoadSupplier(element) {
    if (Suppliers.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Suppliers/supplierJsonList/",
            success: function (data) {
                Suppliers = data;
                renderSupplier(element);
            }
        })
    }
    else {
        renderSupplier(element);
    }
}

function renderSupplier(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option />').val('0').text('Pilih Supplier'));
    $.each(Suppliers, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}
LoadSupplier($('#supplier'));

function GetById(Id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Items/GetById",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            window.alert(result.Supplier['Id']);
            debugger;
            $('#idItem').val(result.Id);
            $('#name').val(result.Name);
            $('#stock').val(result.Stock);
            $('#price').val(result.Price);
            $('#supplier').val(result.Supplier.Id);
            
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();

        }
    })
}

function save() {
    debugger;
    if ($('#name').val() == 0) {
        Swal.fire('Please fill Name');
    } else if (!ValidateName($('#name').val())) {
        Swal.fire('Please fill Name correctly');
    } else if ($('#stock').val() == 0) {
        Swal.fire('Please fill Stock');
    } else if ($('#price').val() == 0) {
        Swal.fire('Please fill Price');
    } else {
        var supplierObj = new Object({
            'Name': $('#name').val(),
            'Stock': $('#stock').val(),
            'Price': $('#price').val(),
            'Supplier.Id': $('#supplier').val()
        });
        $.ajax({
            type: "POST",
            url: "/Items/Create",
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: { itemVM: supplierObj }
        }).then((result) => {
            debugger;
            if (result >= 1) {
                Swal.fire({
                    icon: 'success',
                    title: 'Item has been saved',
                    showConfirmButton: false,
                    timer: 1500
                });
                loadData();
                ClearScreen();
                $('#myModal').modal('hide');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                });
                ClearScreen();
            }
        });

    }
}

function Delete(Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "GET",
                url: "/Items/Delete/",
                data: { id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Item has been deleted',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    loadData();
                    ClearScreen();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                    })
                    ClearScreen();
                }
            });
        }
    })
}

function update() {
    debugger;
    if ($('#name').val() == 0) {
        Swal.fire('Please fill Name');
    } else if (!ValidateName($('#name').val())) {
        Swal.fire('Please fill Name correctly');
    } else if ($('#stock').val() == 0) {
        Swal.fire('Please fill Stock');
    } else if ($('#price').val() == 0) {
        Swal.fire('Please fill Price');
    } else {
        var data = new Object({
            'Id': $('#idItem').val(),
            'Name': $('#name').val(),
            'Stock': $('#stock').val(),
            'Price': $('#price').val(),
            'Supplier': $('#supplier').val()
        });
        
        $.ajax({
            url: "/Items/UpdateItem/",
            data: data
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    title: 'Item has been saved',
                    showConfirmButton: false,
                    timer: 1500
                });
                loadData();
                ClearScreen();
                $('#myModal').modal('hide');
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                });
                ClearScreen();
            }
        });
    }
}
$('#demoGrid').dataTable({
    "order": [],
    "aoColumns": [null, null, null, null, { "bSortable": false }],
    "columnDefs": [{
        "targets": 'no-sort',
        "orderable": false,

    }],
    "responsive": true
});


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

    $("#editItem").click(function () {
        $("#editModal").modal('show');
    });
});

function loadData() {
    $.ajax({
        url: "/Suppliers/supplierJsonList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + key++ + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td>' + item.CreateDate + '</td>';
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

function ValidateName(name) {
    var nameReg = /^[A-Z][a-z 0-9]+$/i;
    return nameReg.test(name);
}


function save() {
    if ($('#name').val() == 0) {
        Swal.fire('Please fill Name');
    } else if (!ValidateName($('#name').val())) {
        Swal.fire('Please fill Name correctly');
    } else if ($('#email').val() == 0) {
        Swal.fire('Please fill Email');
    } else if (!ValidateName($('#name').val())) {
        Swal.fire('Please fill Email correctly');
    } else {
        var role = new Object();
        role.Name = $('#name').val();
        role.Email = $('#email').val();
        $.ajax({
            type: 'POST',
            url: '/Suppliers/Create',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: role
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    title: 'Supplier has been saved',
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

function ClearScreen() {
    $('#name').val('');
    $('#email').val('');
    $('#Update').hide();
    $('#Save').show();
}

function Validate() {
    debugger;
    if ($('#name').val() == "") {
        Swal.fire("Oops", "Please Insert Name Properly", "error")
    } else if ($('#email').val() == "") {
        Swal.fire("Oops", "Please Insert Email Properly", "error")
    } else if ($('#id').val() == 0) {
        Save();
    } else {
        Edit();
    }
}

function GetById(Id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Suppliers/GetById",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            //window.alert(result.Name);
            //window.alert(result.Email);
            debugger;
            $('#idSupplier').val(result.Id);
            $('#name').val(result.Name);
            $('#email').val(result.Email);

            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();

        }
    })
}

function update() {
    debugger;
    if ($('#name').val() == 0) {
        Swal.fire('Please fill Name');
    } else if (!ValidateName($('#name').val())) {
        Swal.fire('Please fill Name correctly');
    } else if ($('#email').val() == 0) {
        Swal.fire('Please fill Email');
    } else if (!ValidateName($('#name').val())) {
        Swal.fire('Please fill Email correctly');
    } else {
        var data = new Object();
        data.Id = $('#idSupplier').val();
        data.Name = $('#name').val();
        data.Email = $('#email').val();
        $.ajax({
            type: "GET",
            url: "/Suppliers/UpdateSupplier",
            data: data
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    title: 'Supplier has been saved',
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
                url: "/Suppliers/Delete/",
                data: { id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Supplier has been deleted',
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

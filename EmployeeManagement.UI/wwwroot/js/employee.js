$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
});

function bindEvents() {
    $(".employeeDetails").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");

        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);

                showEmployeeDetailCard();
                //setTimeout(hideEmployeeDetailCard,2000);

            },
            error: function (error) {
                console.log(error);
            }
        });


    });

    /*$(".employeeDetails").on("click", function (event) {
        var x = 'html'
        $(#id).html(x);
        $(#id).show();
    });*/

    
    $(".employeeDelete").on("click",function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        if (confirm("Do you want to delete?"))
        {
            $.ajax({
                url: 'https://localhost:44383/api/internal/employee/deleteemployees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function (result) {  
                    location.reload();    
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });


    $("#createform").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Name = $("#name").val();
        employeeDetailedViewModel.Department = $("#dept").val();
        employeeDetailedViewModel.Age = Number($("#age").val());
        employeeDetailedViewModel.Address = $("#address").val();

        var data = JSON.stringify(employeeDetailedViewModel);

        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/insert-employees',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (result) {
                location.reload(true);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $("#updateform").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Id = Number($("#empId").val());
        employeeDetailedViewModel.Name = $("#empName").val();
        employeeDetailedViewModel.Department = $("#empDept").val();
        employeeDetailedViewModel.Age = Number($("#empAge").val());
        employeeDetailedViewModel.Address = $("#empAddress").val();

        var data = JSON.stringify(employeeDetailedViewModel);

        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/update-employees',
            type: 'PUT',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (result) {
                location.reload(true);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
}

function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}
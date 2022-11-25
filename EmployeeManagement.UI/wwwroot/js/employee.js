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
                setTimeout(hideEmployeeDetailCard,7000);

            },
            error: function (error) {
                console.log(error);
            }
        });
    });
 
    $("#createform").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Name = $("#name").val();
        employeeDetailedViewModel.Department = $("#dept").val();
        employeeDetailedViewModel.Age = Number($("#age").val());
        employeeDetailedViewModel.Address = $("#address").val();

        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/insert-employees',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(employeeDetailedViewModel),           
            success: function () {

                location.reload();
                
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeEdit").on("click", function (event) {
        
        var employeeId = event.currentTarget.getAttribute("data-id");

        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $("#updateEmployeeId").val(result.id)
                $("#updateEmployeeName").val(result.name)
                $("#updateEmployeeDepartment").val(result.department)
                $("#updateEmployeeAge").val(result.age)
                $("#updateEmployeeAddress").val(result.address)
            },
            error: function (error) {
                console.log(error);
            }
        });
        $("#updateform").submit(function (event) {

            var employeeDetailedViewModel = {};

            employeeDetailedViewModel.Id = Number($("#updateEmployeeId").val());
            employeeDetailedViewModel.Name = $("#updateEmployeeName").val();
            employeeDetailedViewModel.Department = $("#updateEmployeeDepartment").val();
            employeeDetailedViewModel.Age = Number($("#updateEmployeeAge").val());
            employeeDetailedViewModel.Address = $("#updateEmployeeAddress").val();

            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: 'https://localhost:44383/api/internal/employee/update-employees',
                type: 'PUT',
                data: JSON.stringify(employeeDetailedViewModel),
                dataType: 'json',                
                success: function () {

                    location.reload();
                },
                error: function (error) {                   
                    console.log(error);
                }
            });
        });
    });

    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        if (confirm("Do you want to delete?")) {
            $.ajax({
                url: 'https://localhost:44383/api/internal/employee/deleteemployees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function () {
                    location.reload();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });
}

function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}
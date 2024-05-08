$(document).ready(function () {


GetUsers();

function GetUsers() {
    $.ajax({
        url: 'https://localhost:7235/api/Users',
        method: "GET",
        contentType: "application/json",
        success: function (users) {
            console.log("Users fetched:", users);
            let rows = "";
            $.each(users, function (index, user) {
                rows += rowUser(user);
            });
            $("#userstable tbody").empty().append(rows);
        },
        error: function (jqXHR, exception) {
            console.log("Error fetching users:", jqXHR.status, exception);
        }
    });
}

function rowUser(user) {
    return `<tr data-rowid="${user.id}">
                    <td>${user.id}</td>
                    <td>${user.login}</td>
                    <td>${user.password}</td>
                    <td>${user.status}</td>
                    <td>
                        <a class="editLink" data-id="${user.id}">Change</a> | 
                        <a class="removeLink" data-id="${user.id}">Delete</a>
                    </td>
                </tr>`;
}

$("#submit").click(function (e) {
    e.preventDefault();
    let form = document.forms["usersForm"];
    let id = form.elements["Id"].value;
    let login = form.elements["login"].value;
    let password = form.elements["password"].value;
    let status = form.elements["status"].value;

    if (id == 0) {
        CreateUser(login, password, status);
    } else {
        EditUser(id, login, password, status);
    }
});

$("#reset").click(function (e) {
    e.preventDefault();
    let form = document.forms["usersForm"];
    form.reset();
    form.elements["Id"].value = 0;
});

$("body").on("click", ".editLink", function () {
    let id = $(this).data("id");
    GetUser(id);
});

$("body").on("click", ".removeLink", function () {
    let id = $(this).data("id");
    DeleteUser(id);
});

function CreateUser(login, password, status) {
    $.ajax({
        url: "https://localhost:7235/api/Users",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            login: login,
            password: password,
            status: status,
        }),
        success: function (user) {
            $("#userstable tbody").append(rowUser(user));
            let form = document.forms["usersForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, exception) {
            console.log("Error creating user:", jqXHR.status, exception);
        }
    });
}

function EditUser(userId, userLogin, userPassword, userStatus) {
    let request = JSON.stringify({
        id: userId,
        login: userLogin,
        password: userPassword,
        status: userStatus,
    });

    $.ajax({
        url: "https://localhost:7235/api/Users",
        contentType: "application/json",
        method: "PUT",
        data: request,
        success: function (user) {
            $("tr[data-rowid='" + user.id + "']").replaceWith(rowUser(user));
            let form = document.forms["usersForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, exception) {
            console.log("Error editing user:", jqXHR.status, exception);
        }
    });
}

function GetUser(id) {
    $.ajax({
        url: `https://localhost:7235/api/Users/${id}`,
        method: "GET",
        contentType: "application/json",
        success: function (user) {
            console.log("Fetched user:", user);
            let form = document.forms["usersForm"];
            form.elements["Id"].value = user.id;
            form.elements["login"].value = user.login;
            form.elements["password"].value = user.password;
        },
        error: function (jqXHR, exception) {
            console.log("Error fetching user:", jqXHR.status, exception);
        }
    });
}

function DeleteUser(id) {
    if (!confirm("Are you sure you want to delete this user?")) {
        return;
    }

    $.ajax({
        url: `https://localhost:7235/api/Users/${id}`,
        contentType: "application/json",
        method: "DELETE",
        success: function (user) {
            $("tr[data-rowid='" + user.id + "']").remove();
        },
        error: function (jqXHR, exception) {
            console.log("Error deleting user:", jqXHR.status, exception);
        }
    });
    }
})

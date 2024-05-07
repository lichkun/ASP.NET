GetUsers();

function GetUsers() {
    $.ajax({
        url: 'https://localhost:7235/api/Users',
        method: "GET",
        contentType: "application/json",
        success: function (users) {

            let rows = "";
            $.each(users, function (index, user) {
                rows += row(user);
            })
            $("#userstable tbody").append(rows);
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}

function GetUser(id) {
    $.ajax({
        url: 'https://localhost:7235/api/Users/' + id,
        method: 'GET',
        contentType: "application/json",
        success: function (user) {
            let form = document.forms["usersForm"];
            form.elements["Id"].value = user.id;
            form.elements["login"].value = user.login;
            form.elements["password"].value = user.password;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}

function CreateUser(userLogin, userpassword, userStatus) {
    $.ajax({
        url: "https://localhost:7235/api/Users",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            login: userLogin,
            password: userpassword,
            status: userStatus,
            songs: null
        }),
        success: function (user) {
            $("table tbody").append(row(user));
            let form = document.forms["usersForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

function EditUser(userId, userLogin, userpassword, userStatus) {
    let request = JSON.stringify({
        id: userId,
        login: userLogin,
        password: userpassword,
        status: userStatus,
    });
    $.ajax({
        url: "https://localhost:7235/api/Users",
        contentType: "application/json",
        method: "PUT",
        data: request,
        success: function (user) {
            $("tr[data-rowid='" + user.id + "']").replaceWith(row(user));
            let form = document.forms["usersForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}


function DeleteUser(id) {
    if (!confirm("Are you sure to delete this user?"))
        return;
    $.ajax({
        url: "https://localhost:7235/api/Users/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (user) {
            $("tr[data-rowid='" + user.id + "']").remove();
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

let row = function (user) {
    return "<tr data-rowid='" + user.id + "'><td>" + user.id + "</td>" +
        "<td>" + user.login + "</td> <td>" + user.password + "</td><td>" + user.status + "</td >" +
        "<td><a class='editLink' data-id='" + user.id + "'>Change</a> | " +
        "<a class='removeLink' data-id='" + user.id + "'>Delete</a></td></tr>";
};

$("#reset").click(function (e) {
    e.preventDefault();
    let form = document.forms["usersForm"];
    form.reset();
    form.elements["Id"].value = 0;
});

$("#submit").click(function (e) {
    e.preventDefault();
    let form = document.forms["usersForm"];
    let id = form.elements["Id"].value;
    let name = form.elements["login"].value;
    let password = form.elements["password"].value;
    let status = form.elements["status"].value;
    if (id == 0)
        CreateUser(name, password, status);
    else
        EditUser(id, name, password, status);
});

$("body").on("click", ".editLink", function () {
    let id = $(this).data("id");
    GetUser(id);
});

$("body").on("click", ".removeLink", function () {
    let id = $(this).data("id");
    DeleteUser(id);
});

$("#linkUsers").click(function () {
    $(".content").css("display", "none")
    $("#contentUsers").css("display", "block")
})

$("#linkGenres").click(function () {
    $(".content").css("display", "none")
    $("#contentGenres").css("display", "block")
})

$("#linkArtists").click(function () {
    $(".content").css("display", "none")
    $("#contentArtists").css("display", "block")
})







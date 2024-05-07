$(document).ready(function () {

    $("#linkUsers").click(function () {
        $(".content").css("display", "none");
        $("#contentUsers").css("display", "block");
    });

    $("#linkArtists").click(function () {
        $(".content").css("display", "none");
        $("#contentArtists").css("display", "block");
    });

    $("#linkGenres").click(function () {
        $(".content").css("display", "none");
        $("#contentGenres").css("display", "block");
    });

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

    GetArtists();

    function rowArtist(user) {
        return `<tr data-rowid="${user.id}">
                    <td>${user.id}</td>
                    <td>${user.name}</td>
                    <td>
                        <a class="editLinkArtist" data-id="${user.id}">Change</a> | 
                        <a class="removeLinkArtist" data-id="${user.id}">Delete</a>
                    </td>
                </tr>`;
    }

    function GetArtists() {
        $.ajax({
            url: 'https://localhost:7235/api/Artists',
            method: "GET",
            contentType: "application/json",
            success: function (users) {
                console.log("Fetched artists:", users);
                let rows = "";
                $.each(users, function (index, user) {
                    rows += rowArtist(user);
                });
                $("#artistsTable tbody").empty().append(rows);
            },
            error: function (jqXHR, exception) {
                console.log("Error fetching artists:", jqXHR.status, exception);
            }
        });
    }

    $("#submitArtist").click(function (e) {
        e.preventDefault();
        let form = document.forms["artistsForm"];
        let id = form.elements["Id"].value;
        let name = form.elements["name"].value;

        if (id == 0) {
            CreateArtist(name);
        } else {
            EditArtist(id, name);
        }
    });

    $("#resetArtist").click(function (e) {
        e.preventDefault();
        let form = document.forms["artistsForm"];
        form.reset();
        form.elements["Id"].value = 0;
    });

    $("body").on("click", ".editLinkArtist", function () {
        let id = $(this).data("id");
        GetArtist(id);
    });

    $("body").on("click", ".removeLinkArtist", function () {
        let id = $(this).data("id");
        DeleteArtist(id);
    });

    function CreateArtist(userName) {
        $.ajax({
            url: "https://localhost:7235/api/Artists",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                name: userName,
                songs: null
            }),
            success: function (user) {
                $("#artistsTable tbody").append(rowArtist(user));
                let form = document.forms["artistsForm"];
                form.reset();
                form.elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                console.log("Error creating artist:", jqXHR.status, exception);
            }
        });
    }

    function EditArtist(userId, userName) {
        let request = JSON.stringify({
            id: userId,
            name: userName,
        });

        $.ajax({
            url: "https://localhost:7235/api/Artists",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (user) {
                $("tr[data-rowid='" + user.id + "']").replaceWith(rowArtist(user));
                let form = document.forms["artistsForm"];
                form.reset();
                form.elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                console.log("Error editing artist:", jqXHR.status, exception);
            }
        });
    }

    function GetArtist(id) {
        $.ajax({
            url: `https://localhost:7235/api/Artists/${id}`,
            method: "GET",
            contentType: "application/json",
            success: function (user) {
                console.log("Fetched artist:", user);
                let form = document.forms["artistsForm"];
                form.elements["Id"].value = user.id;
                form.elements["name"].value = user.name;
            },
            error: function (jqXHR, exception) {
                console.log("Error fetching artist:", jqXHR.status, exception);
            }
        });
    }

    function DeleteArtist(id) {
        if (!confirm("Are you sure you want to delete this artist?")) {
            return;
        }

        $.ajax({
            url: `https://localhost:7235/api/Artists/${id}`,
            contentType: "application/json",
            method: "DELETE",
            success: function () {
                $("tr[data-rowid='" + id + "']").remove();
            },
            error: function (jqXHR, exception) {
                console.log("Error deleting artist:", jqXHR.status, exception);
            }
        });
    }


    GetGenres();

    function rowGenre(user) {
        return `<tr data-rowid="${user.id}">
                <td>${user.id}</td>
                <td>${user.name}</td>
                <td>
                    <a class="editLinkGenres" data-id="${user.id}">Change</a> | 
                    <a class="removeLinkGenres" data-id="${user.id}">Delete</a>
                </td>
            </tr>`;
    }

    function GetGenres() {
        $.ajax({
            url: 'https://localhost:7235/api/Genres',
            method: "GET",
            contentType: "application/json",
            success: function (users) {
                let rows = "";
                $.each(users, function (index, user) {
                    rows += rowGenre(user);
                });
                $("#genresTable tbody").empty().append(rows);
            },
            error: function (jqXHR, exception) {
                console.log("Error fetching artists:", jqXHR.status, exception);
            }
        });
    }

    $("#submitGenres").click(function (e) {
        e.preventDefault();
        let form = document.forms["genresForm"];
        let id = form.elements["Id"].value;
        let name = form.elements["name"].value;

        if (id == 0) {
            CreateGenre(name);
        } else {
            EditGenre(id, name);
        }
    });

    $("#resetGenres").click(function (e) {
        e.preventDefault();
        let form = document.forms["genresForm"];
        form.reset();
        form.elements["Id"].value = 0;
    });

    $("body").on("click", ".editLinkGenres", function () {
        let id = $(this).data("id");
        GetGenre(id);
    });

    $("body").on("click", ".removeLinkGenres", function () {
        let id = $(this).data("id");
        DeleteGenre(id);
    });

    function CreateGenre(userName) {
        $.ajax({
            url: "https://localhost:7235/api/Genres",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                name: userName,
                songs: null
            }),
            success: function (user) {
                $("#genresTable tbody").append(rowGenre(user));
                let form = document.forms["genresForm"];
                form.reset();
                form.elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                console.log("Error creating artist:", jqXHR.status, exception);
            }
        });
    }

    function EditGenre(userId, userName) {
        let request = JSON.stringify({
            id: userId,
            name: userName,
        });

        $.ajax({
            url: "https://localhost:7235/api/Genres",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (user) {
                $("tr[data-rowid='" + user.id + "']").replaceWith(rowGenre(user));
                let form = document.forms["genresForm"];
                form.reset();
                form.elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                console.log("Error editing artist:", jqXHR.status, exception);
            }
        });
    }

    function GetGenre(id) {
        $.ajax({
            url: `https://localhost:7235/api/Genres/${id}`,
            method: "GET",
            contentType: "application/json",
            success: function (user) {
                let form = document.forms["genresForm"];
                form.elements["Id"].value = user.id;
                form.elements["name"].value = user.name;
            },
            error: function (jqXHR, exception) {
                console.log("Error fetching artist:", jqXHR.status, exception);
            }
        });
    }

    function DeleteGenre(id) {
        if (!confirm("Are you sure you want to delete this genre?")) {
            return;
        }

        $.ajax({
            url: `https://localhost:7235/api/Genres/${id}`,
            contentType: "application/json",
            method: "DELETE",
            success: function () {
                $("tr[data-rowid='" + id + "']").remove();
            },
            error: function (jqXHR, exception) {
                console.log("Error deleting artist:", jqXHR.status, exception);
            }
        });
    }
});

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

$(document).ready(function () {

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


})

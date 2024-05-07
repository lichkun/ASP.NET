    GetArtists();

    function GetArtists() {
        $.ajax({
            url: 'https://localhost:7235/api/Artists',
            method: "GET",
            contentType: "application/json",
            success: function (users) {

                let rows = "";
                $.each(users, function (index, user) {
                    rows += row(user);
                })
                $("#artistsTable tbody").append(rows);
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.status + '\n' + exception);
            }
        });
    }

    function GetArtist(id) {
        $.ajax({
            url: 'https://localhost:7235/api/Artists/' + id,
            method: 'GET',
            contentType: "application/json",
            success: function (user) {
                let form = document.forms["artistsForm"];
                form.elements["Id"].value = user.id;
                form.elements["name"].value = user.name;
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.status + '\n' + exception);
            }
        });
    }

    function CreateArtist(userName) {
        console.log("create")
        $.ajax({
            url: "https://localhost:7235/api/Artists",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                name: userName,
            }),
            success: function (user) {
                $("table tbody").append(row(user));
                let form = document.forms["artistsForm"];
                form.reset();
                form.elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.status + '\n' + exception);
            }
        })
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
                $("tr[data-rowid='" + user.id + "']").replaceWith(row(user));
                let form = document.forms["artistsForm"];
                form.reset();
                form.elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.status + '\n' + exception);
            }
        })
    }


    function DeleteArtist(id) {
        if (!confirm("Are ypu sure to delete this user?"))
            return;
        $.ajax({
            url: "https://localhost:7235/api/Artists/" + id,
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
            "<td>" + user.name + "</td> " +
            "<td><a class='editLinkArtist' data-id='" + user.id + "'>Change</a> | " +
            "<a class='removeLinkArtist' data-id='" + user.id + "'>Delete</a></td></tr>";
    };

    $("#resetArtist").click(function (e) {
        e.preventDefault();
        let form = document.forms["artistsForm"];
        form.reset();
        form.elements["Id"].value = 0;
    });

    $("#submitArtist").click(function (e) {
        console.log("submit");
        e.preventDefault();
        let form = document.forms["artistsForm"];
        let id = form.elements["Id"].value;
        let name = form.elements["name"].value;
        if (id == 0)
            CreateArtist(name);
        else
            EditArtist(id, name);
    });

    $("body").on("click", ".editLinkArtist", function () {
        let id = $(this).data("id");
        GetArtist(id);
    });

    $("body").on("click", ".removeLinkArtist", function () {
        let id = $(this).data("id");
        DeleteArtist(id);
    });



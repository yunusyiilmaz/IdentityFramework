"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/myhub").build();

connection.start()

connection.on("ChangeRole", (userId, roleId) => {

    const requestJson = JSON.stringify({ userId, roleId });

    $.ajax({
        method: 'POST',
        url: '/check-change-role',
        data: requestJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (response) => {
            if (response)
                window.location.reload();
        }
    });

});

connection.on("DeleteUser", () => {

    $.ajax({
        method: 'POST',
        url: '/check-delete-user',
        success: (response) => {
            if (response)
                window.location.reload();
        }
    });

});
/***************************************************************************************************/
connection.on("RealTime", () => {

    const requestJson = JSON.stringify({});

    $.ajax({
        method: 'POST',
        url: '/check-RealTime',
        data: requestJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (response) => {
            if (response)
                window.location.reload();
        }
    });

});

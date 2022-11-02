var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Home/Index')
    .build();

//connection.on('receiveMessage', addMessageToChat);
connection.on("receiveMessage", (message, nickName) => {
    debugger;
    addMessageToChat(message);

    //const _message = $(".message").clone();
    //_message.removeClass("message");
    //_message.find("p").html(message);
    //_message.find("h5")[0].innerHTML = nickName;
    //$(".messages").append(_message);
});

connection
    .start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('sendMessage', message)
}
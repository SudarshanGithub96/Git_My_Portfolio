
var languageBot = 1;
function toggle_lang(para) {
    let english = document.getElementById('chat_wrap_eng');
    let arabic = document.getElementById('chat_wrap_ar');
    if (para === 1) {
        english.style.display = "block";
        arabic.style.display = "none";
        languageBot = 1;
    }
    else {
        english.style.display = "none";
        arabic.style.display = "block";
        languageBot = 2;
    }
    showOrHideDiv()
    return languageBot;
}

function sendMessage2() {
    let messageInput = document.getElementById("message_input");
    let OutMessage = messageInput.value.trim();

    if (OutMessage === "") {
        // Do not proceed if the input message is empty
        return false;
    }


    if (languageBot == 1) {
        var chatbox = document.querySelector(".chatbox_en");
    } else if (languageBot == 2) {
        var chatbox = document.querySelector(".chatbox_ar");
    }
    let chat1 = document.createElement("li");
    chat1.classList.add("chat", "outgoing");
    chat1.innerHTML = `<p>${OutMessage}</p>`;
    chatbox.appendChild(chat1);
    messageInput.value = "";

    let botReply = document.createElement("li");
    botReply.classList.add("chat", "incoming");
    botReply.innerHTML = `<span class="d-flex align-items-center">
                            <span><img src="../assets/images/chatbot_img.svg" alt="chatbot" class="img-fluid"></span>
                            <span class="chatBotloader">
                              <span id="one"></span>
                              <span id="two"></span>
                              <span id="three"></span>
                            </span>
                        </span>`
    chatbox.appendChild(botReply);
    chatbox.scrollTop = chatbox.scrollHeight;

    if (OutMessage !== "") {
        var message = OutMessage;
        $.ajax({
            url: "/Home/SendMessage",
            type: 'POST',
            contentType: 'application/json chatset=utf-8',
            data: JSON.stringify(message),

            success: function (response, textStatus, jqXHR) {
                const InMessage = response;
                let chat = document.createElement("li");
                chatbox.appendChild(chat);
                messageInput.value = "";

                function typeWriter(element, text, speed) {
                    let i = 0;
                    function type() {
                        if (i < text.length) {
                            element.innerHTML += text.charAt(i);
                            i++;
                            chatbox.scrollTop = chatbox.scrollHeight;
                            setTimeout(type, speed);
                        }
                    }
                    type();
                }

                let botMessage = document.createElement("div");
                botMessage.className = "d-flex";
                let botImage = document.createElement("span");
                let img = document.createElement("img");
                img.src = "../assets/images/chatbot_img.svg";
                img.alt = "chatbot";
                img.className = "img-fluid";
                botImage.appendChild(img);
                let botText = document.createElement("p");
                botMessage.appendChild(botImage);
                botMessage.appendChild(botText);

                switch (jqXHR.status) {
                    case 200:
                        chatbox.removeChild(botReply);
                        botReply.innerHTML = "";
                        botReply.appendChild(botMessage);
                        typeWriter(botText, InMessage, 50);
                        break;
                    case 204:
                        console.log('No Content');
                        break;
                    case 400:
                        console.log(`Bad Request: ${jqXHR.responseText}`);
                        break;
                    case 401:
                        console.log(`Unauthorized: ${jqXHR.responseText}`);
                        break;
                    case 404:
                        console.log(`Not Found: ${jqXHR.responseText}`);
                        break;
                    default:
                        chatbox.removeChild(botReply);
                        botReply.innerHTML = "";
                        botReply.appendChild(botMessage);
                        typeWriter(botText, InMessage, 50);
                }
                chatbox.appendChild(botReply);
            },
            error: function (xhr) {
                var errorMessage = 'Error: ' + xhr.status + ' ' + xhr.statusText;
                if (xhr.responseText) {
                    errorMessage = xhr.responseText;
                }
                $('#chatList').append('<li class="chat incoming"><p>' + errorMessage + '</p></li>');
            }
        });
    }
}

//Enter Button Click
document.getElementById("message_input").addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        sendMessage2();
    }
});
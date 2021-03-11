window.pepipost =  (data,apikey) => {
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.onreadystatechange = function () {//Call a function when the state changes.
        if (xhr.readyState === 4 && xhr.status == 200) {
            console.log(this.responseText);
            return "a";
        }else {
            console.log(this.responseText);
            return "b";
        }
    }
    xhr.open("POST", "https://api.pepipost.com/v5/mail/send");
    xhr.setRequestHeader("api_key", apikey);
    xhr.setRequestHeader("content-type", "application/json");
    xhr.send(data);
}
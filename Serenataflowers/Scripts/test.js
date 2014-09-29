function validateEmail() {
    var fieldVal = document.getElementById('txtEmailChilkat').value;
    if (fieldVal == "") {
        alert("Please enter email id.");
        return false;
    }
    else {
        return true;
    }
}
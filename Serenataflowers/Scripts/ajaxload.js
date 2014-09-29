
function InitializeRequest(sender, args) {
    //debugger;
    if (document.getElementById('ProgressDiv') != null)
        $get('ProgressDiv').style.display = 'block';
    else
        createContorl();
}

function EndRequest(sender, args) {
    //debugger;
    if (document.getElementById('ProgressDiv') != null)
        $get('ProgressDiv').style.display = 'none';
    else
        createContorl();
}

function createContorl() {
    //debugger;
    var parentDiv = document.createElement("div");
    parentDiv.setAttribute("class", "ModalProgressContainer");
    parentDiv.setAttribute("Id", "ProgressDiv");
    parentDiv.style.height = "50000px";

    var innerContent = document.createElement("div");
    innerContent.setAttribute("class", "ModalProgressContent");

    var img = document.createElement("img");
    var previousUrl = document.URL;
    var position = previousUrl.indexOf("Checkout")
    if (position == -1) {
        img.setAttribute("src", "images/ajax-loader.gif");
    }
    else {
        img.setAttribute("src", "../images/ajax-loader.gif");
    }

    var textDiv = document.createElement("div");
    textDiv.innerHTML = 'Loading....';
    
    innerContent.appendChild(img);
    innerContent.appendChild(textDiv);

    parentDiv.appendChild(innerContent);

    document.body.appendChild(parentDiv);
}
//var alreadyrunflag=0;
//document.onreadystatechange = function () {
//    if (this.readyState == "complete") {
//        alreadyrunflag = 1;
//        EndRequest('', '');
//    }
//}

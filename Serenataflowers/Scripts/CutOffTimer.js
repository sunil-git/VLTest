var endtime = "", hrs = 0, mns = 0, sec = 0, cuttime = 0;
var myVar;

function showcookieParam(T_Cut, T_Hours, T_Minutes, T_Seconds, T_End) {
    endtime = T_End;
    hrs = Number(T_Hours), mns = Number(T_Minutes), sec = Number(T_Seconds), cuttime = Number(T_Cut);
    if (hrs < 1) {
        document.getElementById("spnCutMsg").style.display = "block";       
        if (mns < 10) {
            document.getElementById("spnCutMsg").style.color = "Red";
        }
        else {
            document.getElementById("spnCutMsg").style.color = "#0095cc";
        }
    }
    else {
        document.getElementById("spnCutMsg").style.display = "none";
       // document.getElementById("spnNewLine").style.display = "none";
    }
   

    var x = "You have <b>" + mns + " minutes " + sec + " seconds</b> remaining to place this order for delivery on <b>" + endtime + "</b>.";
    document.getElementById("spnCutMsg").innerHTML = x;
    //alert(document.getElementById("spnCutMsg").innerHTML);
    myVar = setInterval(function () { UpdateTime() }, 1000);
}


function UpdateTime() {
    cuttime = cuttime - 1;
    if (cuttime > 0) {
        if (sec == 0) {
            sec = 59;
            if (mns == 0) {
                mns = 59;
                if (hrs > 0)
                    hrs = hrs - 1;
                else
                    hrs = 0;
            }
            else
                mns = mns - 1;
        }
        else {
            sec = sec - 1;
        }
      if (hrs < 1) {
          document.getElementById("spnCutMsg").style.display = "block";
         
            if (mns < 10) {
                document.getElementById("spnCutMsg").style.color = "Red";
            }
            else {
                document.getElementById("spnCutMsg").style.color = "#0095cc";
            }
        }
        else {
            document.getElementById("spnCutMsg").style.display = "none";
           
        }
        var x = "You have <b>" + mns + " minutes " + sec + " seconds</b> remaining to place this order for delivery on <b>" + endtime + "</b>.";
        document.getElementById("spnCutMsg").innerHTML = x;
      //  alert(document.getElementById("spnCutMsg").innerHTML);
    }
    else {
       // document.getElementById("spnCutMsg").innerHTML = "";
        clearInterval(myVar);
        //document.getElementById("divRowDelCutOff").style.display = "none";
        //document.getElementById("spnNewLine").style.display = "none";
        location.reload(true);
    }
}
/*
function revealDeliveryCutoffMsg(txtMsg) {
    document.getElementById('ucDeliverControl_divCutOff').style.display = 'block';
    document.getElementById('ucDeliverControl_hdnDeliveryCutOff').value = "true";
    $('#edit-delivery').reveal();
    document.getElementById('ucDeliverControl_spanCutOffMsg').innerHTML = txtMsg;
}


*/


/*  
Add other js functions into reveal popup for the page specific. 
*/
/*
function revealDelCutOffOnLoad(txtMsg) {
    document.getElementById('ucDeliverControl_divCutOff').style.display = 'block';
    document.getElementById('ucDeliverControl_hdnDeliveryCutOff').value = "true";
    $('#edit-delivery').reveal();
    document.getElementById('ucDeliverControl_spanCutOffMsg').innerHTML = txtMsg;

    getStorage();
}

*/


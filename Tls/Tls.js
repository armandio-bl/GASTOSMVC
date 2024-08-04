document.oncontextmenu = new Function("return false;");
//document.onkeypress = KeyPressed;
parent.window.history.go(1);

document.onkeydown = function (e) {
    if (e)
        document.onkeypress = function () { return true; }

    var evt = e ? e : event;
    if (evt.keyCode == 116) {
        if (e)
            document.onkeypress = function () { return false; }
        else {
            evt.keyCode = 0;
            evt.returnValue = false;
        }
    }
}

function gEBI(WObj) {
    return document.getElementById(WObj)
}
function SPLdTblMN() {
    var oTblMn = gEBI("TblMn");
    with (oTblMn) {
        style.position = "absolute"; style.top = 0; style.left = 0; style.height = "100%"; style.width = "100%";
    }
    document.body.appendChild(oTblMn);
    var oTD = gEBI("TD1"); var oCell = gEBI("FrmMn");
    with (oCell) {
        style.height = "100%"; style.width = "100%";
    }
    oTD.appendChild(oCell);
}

function SPLdTblX() {
    var oTblMn = gEBI("TblMn");
    with (oTblMn) {
        style.position = "absolute"; style.top = 0; style.left = 0; style.height = "700px"; style.width = "100%";
    }
    document.body.appendChild(oTblMn);
    var oTD = gEBI("TD1"); var oCell = gEBI("iFRM");
    with (oCell) {
        style.height = "100%"; style.width = "100%";
    }
    oTD.appendChild(oCell);
}

function SPLSubX() {
    var oTD = gEBI("TDSubMn"); var oCell = gEBI("iFRMRes");
    with (oCell) {
        style.height = "100%"; style.width = "100%";
    }
    oTD.appendChild(oCell);
}
function FNFrcWrtONLYBlanc(WElem, WText, WFocus) {
    if (gEBI(WElem).value === "" || gEBI(WElem).length == 0) {
        alert(WText); gEBI(WElem).focus(); gEBI(WElem).style.background = "#CCCCCC"; return false;
    }
    return true;
}
function FNFrcWrt(WElem, WText, WFocus) {
    if (gEBI(WElem).value === "" || gEBI(WElem).length == 0 || gEBI(WElem).value == "0") {
        alert(WText); gEBI(WElem).focus(); gEBI(WElem).style.background = "#CCCCCC"; return false;
    }
    return true;
}
function FNMail(WMail) {
    var WCad = gEBI(WMail).value;
    var filter = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
    if (filter.test(WCad)) {
        return true;
    }
    else {
        return false;
    }
}
function FNCombo(WCbx, WValor) {
    var nItems = gEBI(WCbx).options.length
    for (var J = 0; J < nItems; J++) {
        if (gEBI(WCbx).options[J].value == WValor) {
            gEBI(WCbx).options[J].selected = true; break;
        }
    }
}
function InOut(tdID, sClass) {
    gEBI(tdID).className = sClass;
}
function FNRuta(WRuta) {
    var aFile = WRuta.split("/"); var Item = aFile.length - 1; WFile = aFile[Item]; var aFile = null;
    return WFile;
}
function FNSnd(WPage) {
    var d = document.forms[0];
    with (d) {
        action = FNRuta(WPage); submit();
    }
    var d = null;
}

function FNSndX(WPage) {
    var d = document.forms[0];
    with (d) {
        action = WPage; submit();
    }
    var d = null;
}

function SPShowPag(sPage) {
    parent.frames[0].location = sPage;
}

function FNAJAX() {
    if (window.XMLHttpRequest) return new XMLHttpRequest(); // code for IE7+, Firefox, Chrome, Opera, Safari
    if (window.ActiveXObject) return new ActiveXObject("Microsoft.XMLHTTP"); // code for IE6, IE5
    return null;
}

function SPCambiaSve() {
    gEBI("HDEsGuardar").value = "1";
}

function FNOnlyNumDotFormat(objeto, evt) {
    var WCad = "" + objeto.value; var keyCode;
    if (evt.keyCode > 0) {
        keyCode = evt.keyCode;
    }
    else if (typeof (evt.charCode) != "undefined") {
        keyCode = evt.charCode;
    }
    if (keyCode == 190) return true;
    if (keyCode == 46) return true;
    if ((keyCode < 7 || keyCode > 9) && (keyCode < 37 || keyCode > 40) && (keyCode < 48 || keyCode > 57)) return false;
}

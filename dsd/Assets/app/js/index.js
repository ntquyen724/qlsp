
window.onload = function () {
    MaskedInput1({
        elm: document.getElementById('email'), // select only by id
        format: '____________@__________',
        separator: ' ()-'
    });
};
function formatT() {
    var x = document.getElementById("email");
    var iz = x.value.indexOf("@");
    var text = "";
    var te = x.value.indexOf("_");
    if (x.value.indexOf("_") > 0) {
        for (var i = 1; i < x.value.indexOf("_"); i++) {
            if (i === iz-1) {
                text += "@";
            }
            else {
                text += "_";
            }
        }
    }
    else {
        text = "____________@__________";
    }
    return text;
};

function TabExample(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    if (evt.keyCode == 9) {
        myFunction();
        if (evt.preventDefault) {
            evt.preventDefault();
        }
        return false;
    }
    else if (evt.keyCode > 45 && evt.keyCode < 105) {
        var x = document.getElementById("email");
        var i = x.value.indexOf("@");
        if (x.value.indexOf("@") - 1 == x.value.indexOf("_")) {
            x.value = [x.value.slice(0, i - 1), evt.key, x.value.slice(i - 1)].join('');
            document.getElementById("email").setSelectionRange(x.value.indexOf("_")-1,x.value.indexOf("_"));
        } else if (x.value.indexOf("@") - 1 < x.value.indexOf("_")) {
            x.value = [x.value.slice(0, x.value.indexOf("_")), evt.key, x.value.slice(x.value.lastIndexOf("_"))].join('');
            document.getElementById("email").setSelectionRange(x.value.indexOf("_") - 1, x.value.indexOf("_"));
        }
       
    }
    else if (evt.keyCode == 8) {
        MaskedInput1.format = formatT();
    }
};
function myFunction() {
    var x = document.getElementById("email");
    var i = x.value.indexOf("@");
    var i1 = x.value.indexOf("_");
    if (i - i1 > 1) {
        var test = x.value.slice(0, i1);
        var test2 = x.value.slice(i);
        MaskedInput1.format = formatT();
            x.value = x.value.slice(0, i1) + x.value.slice(i); 
    }
    x.value = x.value.slice(0, i - 1) + x.value.slice(i); 
    document.getElementById("email").focus();
    document.getElementById("email").setSelectionRange(i1+1, i1+1);
};
(function (a) { a.MaskedInput1 = function (f) { if (!f || !f.elm || !f.format) { return null } if (!(this instanceof a.MaskedInput1)) { return new a.MaskedInput1(f) } var o = this, d = f.elm, s = f.format, i = f.allowed || "0123456789qwertyuiopasdfghjklzxcvbnm.", h = f.allowedfx || function () { return true }, p = f.separator || "/:-", n = f.typeon || "_YMDhms", c = f.onbadkey || function () { }, q = f.onfilled || function () { }, w = f.badkeywait || 0, A = f.hasOwnProperty("preserve") ? !!f.preserve : true, l = true, y = false, t = s, j = (function () { if (window.addEventListener) { return function (E, C, D, B) { E.addEventListener(C, D, (B === undefined) ? false : B) } } if (window.attachEvent) { return function (D, B, C) { D.attachEvent("on" + B, C) } } return function (D, B, C) { D["on" + B] = C } }()), u = function () { for (var B = d.value.length - 1; B >= 0; B--) { for (var D = 0, C = n.length; D < C; D++) { if (d.value[B] === n[D]) { return false } } } return true }, x = function (C) { try { C.focus(); if (C.selectionStart >= 0) { return C.selectionStart } if (document.selection) { var B = document.selection.createRange(); return -B.moveStart("character", -C.value.length) } return -1 } catch (D) { return -1 } }, b = function (C, E) { try { if (C.selectionStart) { C.focus(); C.setSelectionRange(E, E) } else { if (C.createTextRange) { var B = C.createTextRange(); B.move("character", E); B.select() } } } catch (D) { return false } return true }, m = function (D) { D = D || window.event; var C = "", E = D.which, B = D.type; if (E === undefined || E === null) { E = D.keyCode } if (E === undefined || E === null) { return "" } switch (E) { case 8: C = "bksp"; break; case 16: C = "shift"; break; case 0: case 9: case 13: C = "etc"; break; case 37: case 38: case 39: case 40: C = (!D.shiftKey && (D.charCode !== 39 && D.charCode !== undefined)) ? "etc" : String.fromCharCode(E); break; default: C = String.fromCharCode(E); break }return C }, v = function (B, C) { if (B.preventDefault) { B.preventDefault() } B.returnValue = C || false }, k = function (B) { var D = x(d), F = d.value, E = "", C = true; switch (C) { case (i.indexOf(B) !== -1): D = D + 1; if (D > s.length) { return false } while (p.indexOf(F.charAt(D - 1)) !== -1 && D <= s.length) { D = D + 1 } if (!h(B, D)) { c(B); return false } E = F.substr(0, D - 1) + B + F.substr(D); if (i.indexOf(F.charAt(D)) === -1 && n.indexOf(F.charAt(D)) === -1) { D = D + 1 } break; case (B === "bksp"): D = D - 1; if (D < 0) { return false } while (i.indexOf(F.charAt(D)) === -1 && n.indexOf(F.charAt(D)) === -1 && D > 1) { D = D - 1 } E = F.substr(0, D) + s.substr(D, 1) + F.substr(D + 1); break; case (B === "del"): if (D >= F.length) { return false } while (p.indexOf(F.charAt(D)) !== -1 && F.charAt(D) !== "") { D = D + 1 } E = F.substr(0, D) + s.substr(D, 1) + F.substr(D + 1); D = D + 1; break; case (B === "etc"): return true; default: return false }d.value = ""; d.value = E; b(d, D); return false }, g = function (B) { if (i.indexOf(B) === -1 && B !== "bksp" && B !== "del" && B !== "etc") { var C = x(d); y = true; c(B); setTimeout(function () { y = false; b(d, C) }, w); return false } return true }, z = function (C) { if (!l) { return true } C = C || event; if (y) { v(C); return false } var B = m(C); if ((C.metaKey || C.ctrlKey) && (B === "X" || B === "V")) { v(C); return false } if (C.metaKey || C.ctrlKey) { return true } if (d.value === "") { d.value = s; b(d, 0) } if (B === "bksp" || B === "del") { k(B); v(C); return false } return true }, e = function (C) { if (!l) { return true } C = C || event; if (y) { v(C); return false } var B = m(C); if (B === "etc" || C.metaKey || C.ctrlKey || C.altKey) { return true } if (B !== "bksp" && B !== "del" && B !== "shift") { if (!g(B)) { v(C); return false } if (k(B)) { if (u()) { q() } v(C, true); return true } if (u()) { q() } v(C); return false } return false }, r = function () { if (!d.tagName || (d.tagName.toUpperCase() !== "INPUT" && d.tagName.toUpperCase() !== "TEXTAREA")) { return null } if (!A || d.value === "") { d.value = s } j(d, "keydown", function (B) { z(B) }); j(d, "keypress", function (B) { e(B) }); j(d, "focus", function () { t = d.value }); j(d, "blur", function () { if (d.value !== t && d.onchange) { d.onchange() } }); return o }; o.resetField = function () { d.value = s }; o.setAllowed = function (B) { i = B; o.resetField() }; o.setFormat = function (B) { s = B; o.resetField() }; o.setSeparator = function (B) { p = B; o.resetField() }; o.setTypeon = function (B) { n = B; o.resetField() }; o.setEnabled = function (B) { l = B }; return r() } }(window));


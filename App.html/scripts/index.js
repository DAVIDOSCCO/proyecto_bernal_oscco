/*Este es el index.js*/

(function (cibertec) {
    cibertec.Index = {
        currentYear: function () {
            var today = new Date();
            return today.getFullYear();
        }
    }
    document.getElementById("date").innerHTML = cibertec.Index.currentYear();
})(window.cibertec = window.cibertec || {});

/*
window.onload = function(){
    var today = new Date().getFullYear();
    console.log(today);
    document.getElementById("date").innerHTML = today;
}
*/

/*
$(document).ready(function () {
    var today = new Date().getFullYear();
    console.log(today);
    $("#date").html(today);
})
*/

/*
function readyFunction() {
    var today = new Date().getFullYear();
    console.log(today);
    $("#date").html(today);
}

$(document).ready(readyFunction);
*/

/*
function readyFunction() {
    var today = new Date().getFullYear();
    console.log(today);
    $("#date").html(today);
}
$(window).on("load", readyFunction);
*/
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#formname").on("change", "input:checkbox", function () {
        $("#formname").submit();
    });
});


$('#bicycles')
    .load('/Home/BicycleList')

$('#search').change( function () {
    let searchText = $('#search').val()
    $('#bicycles')
        .load(`/Home/BicycleList?searchText=${ searchText }`)
})
document.getElementById('selectFile').onchange = function (evt) {
    var tgt = evt.target || window.event.srcElement,
        files = tgt.files;
    if (FileReader && files && files.length) {
        var fr = new FileReader();
        fr.onload = function () {


            document.getElementById("imgPreview").src = fr.result;
        }
        fr.readAsDataURL(files[0]);
    }
    else {

    }
}
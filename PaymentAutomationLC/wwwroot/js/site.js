﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    if ($("table").hasClass("no-pagination")) {
        $(".no-pagination").DataTable({
            "paging": false
        })
    }
    else {
        $("table").DataTable();
    }

    $('#new-file-upload').change((e) => {
        var fileName = e.target.files[0].name;
        $('.custom-file-label').html(fileName);
    })
});
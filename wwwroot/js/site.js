﻿//Set array of mod types
let ModTypesOld = [
    { value: "Performance", name: "Performance" },
    { value: "Interior", name: "Interior" },
    { value: "Exterior", name: "Exterior" }
];

$(document).ready(function () {
    var url = "/Mods/ModTypes";
    $.get(url, null, function (data) {
        console.log(data);
    });
});

console.log(data);

//Retain user search selections
let ModTypeSelection = document.getElementById("searchModType").getAttribute("value");

//Load Mod Type dropdown
new TomSelect('#searchModType', {
    options: [data],
    items: [ModTypeSelection],
    placeholder: 'Mod Type',
    labelField: 'name',
    searchField: ['name'],
    openOnFocus: true,
    highlight: false,
    hideSelected: true,
    selectOnTab: true,
    allowEmptyOption: true,
    diacritics: true
});

modPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            //Display popup content
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
            if (!$(".modal.in").length) {
                $(".modal-dialog").css({
                    top: 0,
                    left: 0
                });
            }

            //Load Mod Type dropdown
            var ModTypeCurrentValue = document.getElementById("ModTypeValue").value;
            new TomSelect('#select-ModType', {
                options: [ModTypes],
                items: [ModTypeCurrentValue],
                placeholder: 'Mod Type',
                labelField: 'name',
                searchField: ['name'],
                openOnFocus: true,
                highlight: false,
                hideSelected: true,
                selectOnTab: true
            });
        }
    });
}

invoicePopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            //Display popup content
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    });
}

deletePopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            //Display popup content
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    });
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#form-modal').modal('hide');
                $("#tableAJAX").load(location.href + " #tableAJAX");
                $("#totalSum").load(location.href + " #totalSum");
            },
            error: function (err) {
                console.log(err)
            }
        });
        //To prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#form-modal').modal('hide');
                $("#tableAJAX").load(location.href + " #tableAJAX");
                $("#totalSum").load(location.href + " #totalSum");
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    //Prevent default form submit event
    return false;
}
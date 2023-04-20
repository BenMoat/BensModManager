//Retain the Exclude obsolete mods selection 
$('#obsoleteCheckbox').click(function (e) {
    if (e.target.checked) {
        localStorage.setItem('checked', 'true');
    } else {
        localStorage.setItem('checked', 'false');
    }
});

$(document).ready(function () {
    document.querySelector('#obsoleteCheckbox').checked = (localStorage.getItem('checked') === 'true')
});

//Default the checkbox to unchecked when resetting search params
$('#resetSearch').click(function (e) {
    localStorage.setItem('checked', 'false');
});

//Get the static total price
var getPrice = $.ajax({
    url: "/Mods/TotalPrice",
    type: 'GET',
    success: function (data) {
        $('#totalPriceStatic').append(data);
    }
});

//Get the static amount of all mods
var getPrice = $.ajax({
    url: "/Mods/TotalMods",
    type: 'GET',
    success: function (data) {
        $('#totalModsStatic').append(data);
    }
});

//Get mod types
var ModTypes;
$.ajax({
    url: "/Mods/ModTypes",
    type: 'GET',
    async: false,
    success: function (data) {
        ModTypes = data;
    }
});

//Retain mod type selection
let ModTypeSelection = document.getElementById("searchModType").getAttribute("value");

//Load Mod Type dropdown
new TomSelect('#searchModType', {
    options: [ModTypes],
    items: [ModTypeSelection],
    placeholder: 'Mod Type',
    labelField: 'value',
    searchField: ['value'],
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
            $("#invoiceModal").modal('hide');
            $("#deleteModal").modal('hide');

            //Display popup content
            $('#addOrEditModal .modal-body').html(res);
            $('#addOrEditModal .modal-title').html(title);
            $('#addOrEditModal').modal('show');
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
            if (!$(".modal.in").length) {
                $(".modal-dialog").css({
                    top: 0,
                    left: 0
                });
            }

            //Automatically resize the notes field to show all content
            $('.modal').on('shown.bs.modal', function () {
                $(this).find('#dynamicNotes').each(function () {
                    $('#dynamicNotes').css({
                        overflow: 'hidden'
                    });
                    this.style.transition = 'all .4s';
                    this.style.height = (this.scrollHeight) + 'px';
                });
            });

            //Dynamically change the size of the text area upon addition or removal of a line
            $('#dynamicNotes').on('input', function () {
                this.style.height = 'auto';
                this.style.height = (this.scrollHeight) + 'px';
            });

            //Display the scrollbar if the textarea exceeds the maximum height
            $('#dynamicNotes').on('input', function () {
                var maxHeight = '500px';

                if (this.style.height >= maxHeight) {
                    $('#dynamicNotes').css({
                        overflow: 'visible'
                    })
                }
                else if (this.style.height < maxHeight) {
                    $('#dynamicNotes').css({
                        overflow: 'hidden'
                    })
                }
            });

            //Load Mod Type dropdown
            var ModTypeCurrentValue = document.getElementById("ModTypeValue").value;
            new TomSelect('#selectModType', {
                options: [ModTypes],
                items: [ModTypeCurrentValue],
                placeholder: 'Mod Type',
                labelField: 'value',
                searchField: ['value'],
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
            $("#addOrEditModal").modal('hide');
            $("#deleteModal").modal('hide');

            $('#invoiceModal .modal-body').html(res);
            $('#invoiceModal .modal-title').html(title);
            $('#invoiceModal').modal('show');

            $('#invoiceWidth .modal-dialog ').css('max-width', '700px');  
        }
    });
}

deletePopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#addOrEditModal").modal('hide');
            $("#invoiceModal").modal('hide');

            //Display popup content
            $('#deleteModal .modal-body').html(res);
            $('#deleteModal .modal-title').html(title);
            $('#deleteModal').modal('show');
        }
    });
}

jQueryAjaxPost = form => {
    try {
        $('#loader-wrapper').show();
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#addOrEditModal').modal('hide');
                $("#invoiceModal").modal('hide');
                $('#deleteModal').modal('hide');

                $("#tableAJAX").load(location.href + " #tableAJAX");
                $("#totalPrice").load(location.href + " #totalPrice");

                $.ajax({
                    url: "/Mods/TotalPrice",
                    type: 'GET',
                    success: function (data) {
                        //Refresh the total price
                        $('#totalPriceStatic').empty(data);
                        $('#totalPriceStatic').append(data);
                    }
                });

                $.ajax({
                    url: "/Mods/TotalMods",
                    type: 'GET',
                    success: function (data) {
                        //Refresh the total price
                        $('#totalModsStatic').empty(data);
                        $('#totalModsStatic').append(data);
                    }
                });
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
    $('#loader-wrapper').hide();
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
                $('#addOrEditModal').modal('hide');
                $('#invoiceModal').modal('hide');
                $('#deleteModal').modal('hide');
                $("#tableAJAX").load(location.href + " #tableAJAX");
                $("#totalPrice").load(location.href + " #totalPrice");

                $.ajax({
                    url: "/Mods/TotalPrice",
                    type: 'GET',
                    success: function (data) {
                        $('#totalPriceStatic').empty(data);
                        $('#totalPriceStatic').append(data);
                    }
                });

                $.ajax({
                    url: "/Mods/TotalMods",
                    type: 'GET',
                    success: function (data) {
                        //Refresh the total price
                        $('#totalModsStatic').empty(data);
                        $('#totalModsStatic').append(data);
                    }
                });
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
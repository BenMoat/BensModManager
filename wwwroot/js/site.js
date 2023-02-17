//Table row will enlarge and change colour based on mouse hover
$(document).ready(function () {
    $('tr').hover(function () {
        $('tr').removeClass('focusRow');
        $(this).addClass('focusRow');
    }, function () {
        $(this).removeClass('focusRow');
    });
});

//Initialise the dropdown arrays -- this is NOT a future-proof implementation. Needs to be a LINQ SELECT DISTINCT query in the controller

let ModTypes = [
    { value: "Performance", name: "Performance" },
    { value: "Interior", name: "Interior" },
    { value: "Exterior", name: "Exterior" }
];

//Retain user search selections
let ModTypeSelection = document.getElementById("searchModType").getAttribute("value");

//Load Paint Type dropdown
new TomSelect('#searchModType', {
    options: [ModTypes],
    items: [ModTypeSelection],
    placeholder: 'Paint Type',
    labelField: 'name',
    searchField: ['name'],
    openOnFocus: true,
    highlight: false,
    hideSelected: true,
    selectOnTab: true,
    allowEmptyOption: true,
    diacritics: true
});

//Set up and initialise the record popup function. 
showInPopup = (url, title) => {
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

            //Load Vehicle Make dropdown
            var makeCurrentValue = document.getElementById("makeValue").value;
            new TomSelect('#select-make', {
                options: [vehicleMakes],
                items: [makeCurrentValue],
                placeholder: 'Make',
                labelField: 'name',
                searchField: ['name'],
                openOnFocus: true,
                highlight: false,
                hideSelected: true,
                selectOnTab: true,
                diacritics: true
            });

            //Load Paint Type dropdown
            var ModTypeCurrentValue = document.getElementById("ModTypeValue").value;
            new TomSelect('#select-ModType', {
                options: [ModTypes],
                items: [ModTypeCurrentValue],
                placeholder: 'Paint Type',
                labelField: 'name',
                searchField: ['name'],
                openOnFocus: true,
                highlight: false,
                hideSelected: true,
                selectOnTab: true
            });

            //Alter the visibility settings dynamically via an onChange event
            $(document).ready(function custVis() {
                $('#select-ModType').on('change', function () {
                    //These Paint Types only have X values
                    if (this.value == 'Aluminium Polished' || this.value == 'Aluminium Semigloss' || this.value == 'Carbon Fibre Polished'
                        || this.value == 'Chrome' || this.value == 'Matte' || this.value == 'Normal' || this.value == 'Semigloss') {
                        document.getElementById('ModTypeXCondition').hidden = false;
                        document.getElementById('colourX').required = true;
                        document.getElementById('saturationX').required = true;
                        document.getElementById('brightnessX').required = true;

                        document.getElementById('ModTypeYCondition').hidden = true;
                        document.getElementById('colourY').value = '';
                        document.getElementById('colourY').required = false;
                        document.getElementById('saturationY').value = '';
                        document.getElementById('saturationY').required = false;
                        document.getElementById('brightnessY').value = '';
                        document.getElementById('brightnessY').required = false;
                    }
                    //Condition if a paint type isn't selected. Also an exception for this paint type that doesn't require an axis
                    else if (this.value == 'Prismacolour White' || this.value.length === 0) {
                        document.getElementById('ModTypeYCondition').hidden = true;
                        document.getElementById('ModTypeXCondition').hidden = true;
                        document.getElementById('colourX').value = '';
                        document.getElementById('colourX').required = false;
                        document.getElementById('saturationX').value = '';
                        document.getElementById('saturationX').required = false;
                        document.getElementById('brightnessX').value = '';
                        document.getElementById('brightnessX').required = false;
                        document.getElementById('colourY').value = '';
                        document.getElementById('colourY').required = false;
                        document.getElementById('saturationY').value = '';
                        document.getElementById('saturationY').required = false;
                        document.getElementById('brightnessY').value = '';
                        document.getElementById('brightnessY').required = false;

                    }
                    //All other paint types have X and Y values for both axes
                    else {
                        document.getElementById('ModTypeYCondition').hidden = false;
                        document.getElementById('ModTypeXCondition').hidden = false;
                    }
                });
                //Ensures the correct fields are showing on popup load
                $('#select-ModType').trigger('change');
            });
        }
    });
}

showInDeletePopup = (url, title) => {
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
            },
            error: function (err) {
                console.log(err)
            }
        });
        //to prevent default form submit event
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
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    //prevent default form submit event
    return false;
}
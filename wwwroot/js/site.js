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
let vehicleMakes = [
    { value: "3M", name: "3M" },
    { value: "Abarth", name: "Abarth" },
    { value: "AC Cars", name: "AC Cars" },
    { value: "AC Schnitzer", name: "AC Schnitzer" },
    { value: 'Acura', name: 'Acura' },
    { value: 'Alfa Romeo', name: 'Alfa Romeo' },
    { value: 'Alpina', name: 'Alpina' },
    { value: 'Alpine', name: 'Alpine' },
    { value: 'Alvis', name: 'Alvis' },
    { value: 'AMC', name: 'AMC' },
    { value: 'Amphicar', name: 'Amphicar' },
    { value: 'Apollo', name: 'Apollo' },
    { value: 'Ariel', name: 'Ariel' },
    { value: 'Arrinera', name: 'Arrinera' },
    { value: 'Art Morrison', name: 'Art Morrison' },
    { value: 'Ascari', name: 'Ascari' },
    { value: 'Aspark', name: 'Aspark' },
    { value: 'Aston Martin', name: 'Aston Martin' },
    { value: 'Auburn', name: 'Auburn' },
    { value: 'Audi', name: 'Audi' },
    { value: 'Austin', name: 'Austin' },
    { value: 'Austin-Healey', name: 'Austin-Healey' },
    { value: 'Autobacs', name: 'Autobacs' },
    { value: 'Autobianchi', name: 'Autobianchi' },
    { value: 'Autocar', name: 'Autocar' },
    { value: 'Avery', name: 'Avery' },
    { value: 'BAC', name: 'BAC' },
    { value: 'Bedford', name: 'Bedford' },
    { value: 'Bentley', name: 'Bentley' },
    { value: 'Bertone', name: 'Bertone' },
    { value: 'BMW', name: 'BMW' },
    { value: 'Bricklin', name: 'Bricklin' },
    { value: 'Bugatti', name: 'Bugatti' },
    { value: 'Buick', name: 'Buick' },
    { value: 'Cadillac', name: 'Cadillac' },
    { value: 'Callaway', name: 'Callaway' },
    { value: 'Can-Am', name: 'Can-Am' },
    { value: 'Caparo', name: 'Caparo' },
    { value: 'Caterham', name: 'Caterham' },
    { value: 'Caterpillar', name: 'Caterpillar' },
    { value: 'Chaparral', name: 'Chaparral' },
    { value: 'Checker', name: 'Checker' },
    { value: 'Chevrolet', name: 'Chevrolet' },
    { value: 'Chrysler', name: 'Chrysler' },
    { value: 'Citroën', name: 'Citroën' },
    { value: 'Cord', name: 'Cord' },
    { value: 'Crosley', name: 'Crosley' },
    { value: 'Daewoo', name: 'Daewoo' },
    { value: 'Daihatsu', name: 'Daihatsu' },
    { value: 'Datsun', name: 'Datsun' },
    { value: 'David Brown', name: 'David Brown' },
    { value: 'DeSoto', name: 'DeSoto' },
    { value: 'DMC', name: 'DMC' },
    { value: 'Dodge', name: 'Dodge' },
    { value: 'DS Automobiles', name: 'DS Automobiles' },
    { value: 'DuPont', name: 'DuPont' },
    { value: 'Eagle', name: 'Eagle' },
    { value: 'Edsel', name: 'Edsel' },
    { value: 'Farmall', name: 'Farmall' },
    { value: 'Ferrari', name: 'Ferrari' },
    { value: 'Fiat', name: 'Fiat' },
    { value: 'Fisker', name: 'Fisker' },
    { value: 'Fleet', name: 'Fleet' },
    { value: 'Ford', name: 'Ford' },
    { value: 'Forza', name: 'Forza' },
    { value: 'Gemballa', name: 'Gemballa' },
    { value: 'General Motors', name: 'General Motors' },
    { value: 'Genesis', name: 'Genesis' },
    { value: 'Geo', name: 'Geo' },
    { value: 'Ginetta', name: 'Ginetta' },
    { value: 'GMC', name: 'GMC' },
    { value: 'Gran Turismo', name: 'Gran Turismo' },
    { value: 'Hennessey', name: 'Hennessey' },
    { value: 'Hexis', name: 'Hexis' },
    { value: 'Hillman', name: 'Hillman' },
    { value: 'Hitachi', name: 'Hitachi' },
    { value: 'Holden', name: 'Holden' },
    { value: 'Honda', name: 'Honda' },
    { value: 'Hudson', name: 'Hudson' },
    { value: 'Hummer', name: 'Hummer' },
    { value: 'Hyundai', name: 'Hyundai' },
    { value: 'IH', name: 'IH' },
    { value: 'Infiniti', name: 'Infiniti' },
    { value: 'Inozetek', name: 'Inozetek' },
    { value: 'International', name: 'International' },
    { value: 'Isuzu', name: 'Isuzu' },
    { value: 'Italdesign', name: 'Italdesign' },
    { value: 'Jaguar', name: 'Jaguar' },
    { value: 'Jeep', name: 'Jeep' },
    { value: 'John Deere', name: 'John Deere' },
    { value: 'JR Motorsports', name: 'JR Motorsports' },
    { value: 'Karma', name: 'Karma' },
    { value: 'Kia', name: 'Kia' },
    { value: 'Koenigsegg', name: 'Koenigsegg' },
    { value: 'KTM', name: 'KTM' },
    { value: 'Lada', name: 'Lada' },
    { value: 'Lamborghini', name: 'Lamborghini' },
    { value: 'Land Rover', name: 'Land Rover' },
    { value: 'Lexus', name: 'Lexus' },
    { value: 'Light Car Company', name: 'Light Car Company' },
    { value: 'Lincoln', name: 'Lincoln' },
    { value: 'Lotus', name: 'Lotus' },
    { value: 'Maserati', name: 'Maserati' },
    { value: 'Massey Ferguson', name: 'Massey Ferguson' },
    { value: 'Mattel', name: 'Mattel' },
    { value: 'Mazda', name: 'Mazda' },
    { value: 'McLaren', name: 'McLaren' },
    { value: 'Mercedes-AMG', name: 'Mercedes-AMG' },
    { value: 'Mercedes-Benz', name: 'Mercedes-Benz' },
    { value: 'Mercury', name: 'Mercury' },
    { value: 'MG', name: 'MG' },
    { value: 'Mini', name: 'Mini' },
    { value: 'Mitsubishi', name: 'Mitsubishi' },
    { value: 'Moke', name: 'Moke' },
    { value: 'Morgan', name: 'Morgan' },
    { value: 'Morris', name: 'Morris' },
    { value: 'Mosler', name: 'Mosler' },
    { value: 'New Holland', name: 'New Holland' },
    { value: 'Nissan', name: 'Nissan' },
    { value: 'Noble', name: 'Noble' },
    { value: 'Oldsmobile', name: 'Oldsmobile' },
    { value: 'Opel', name: 'Opel' },
    { value: 'Packard', name: 'Packard' },
    { value: 'Pagani', name: 'Pagani' },
    { value: 'Peel', name: 'Peel' },
    { value: 'Peugeot', name: 'Peugeot' },
    { value: 'Piaggio', name: 'Piaggio' },
    { value: 'Plymouth', name: 'Plymouth' },
    { value: 'Polaris', name: 'Polaris' },
    { value: 'Pontiac', name: 'Pontiac' },
    { value: 'Porsche', name: 'Porsche' },
    { value: 'RAM', name: 'RAM' },
    { value: 'Reliant', name: 'Reliant' },
    { value: 'Renault', name: 'Renault' },
    { value: 'Rimac', name: 'Rimac' },
    { value: 'Rolls-Royce', name: 'Rolls-Royce' },
    { value: 'Rossion', name: 'Rossion' },
    { value: 'Rover', name: 'Rover' },
    { value: 'Ruf', name: 'Ruf' },
    { value: 'Saleen', name: 'Saleen' },
    { value: 'Saturn', name: 'Saturn' },
    { value: 'Scion', name: 'Scion' },
    { value: 'Seat', name: 'Seat' },
    { value: 'Shelby', name: 'Shelby' },
    { value: 'Singer', name: 'Singer' },
    { value: 'Skoda', name: 'Skoda' },
    { value: 'Spoon', name: 'Spoon' },
    { value: 'SRT', name: 'SRT' },
    { value: 'SsangYong', name: 'SsangYong' },
    { value: 'SSC', name: 'SSC' },
    { value: 'Standox', name: 'Standox' },
    { value: 'Studebaker', name: 'Studebaker' },
    { value: 'Subaru', name: 'Subaru' },
    { value: 'Suzuki', name: 'Suzuki' },
    { value: 'Tata', name: 'Tata' },
    { value: 'TeckWrap USA', name: 'TeckWrap USA' },
    { value: 'Terradyne', name: 'Terradyne' },
    { value: 'Tesla', name: 'Tesla' },
    { value: 'Top Secret', name: 'Top Secret' },
    { value: 'Toyota', name: 'Toyota' },
    { value: 'Trabant', name: 'Trabant' },
    { value: 'Triumph', name: 'Triumph' },
    { value: 'TVR', name: 'TVR' },
    { value: 'Ultima', name: 'Ultima' },
    { value: 'Vauxhall', name: 'Vauxhall' },
    { value: 'Volkswagen', name: 'Volkswagen' },
    { value: 'Volvo', name: 'Volvo' },
    { value: 'W Motors', name: 'W Motors' },
    { value: 'Wartburg', name: 'Wartburg' },
    { value: 'Wheego', name: 'Wheego' },
    { value: 'Willys', name: 'Willys' },
    { value: 'Yugo', name: 'Yugo' },
    { value: 'Zenvo', name: 'Zenvo' },
    { value: 'Zündapp', name: 'Zündapp' }
];
let wheelMakes = [
    { value: "5ZIGEN", name: "5ZIGEN" },
    { value: "ADV.1 Wheels", name: "ADV.1 Wheels" },
    { value: "Advan Wheels", name: "Advan Wheels" },
    { value: "Alpine", name: "Alpine" },
    { value: "Apollo", name: "Apollo" },
    { value: "BBS", name: "BBS" },
    { value: "BMW", name: "BMW" },
    { value: "Buddy Club", name: "Buddy Club" },
    { value: "Bugatti", name: "Bugatti" },
    { value: "Ferrari", name: "Ferrari" },
    { value: "Porsche", name: "Porsche" },
    { value: "RAYS Volk", name: "RAYS Volk" },
    { value: "Subaru", name: "Subaru" },
    { value: "Ultima", name: "Ultima" },
    { value: "Volvo", name: "Volvo" },
    { value: "Zenvo", name: "Zenvo" }
];
let ModTypes = [
    { value: "Aluminium Polished", name: "Aluminium Polished" },
    { value: "Aluminium Semigloss", name: "Aluminium Semigloss" },
    { value: "Carbon Fibre Polished", name: "Carbon Fibre Polished" },
    { value: "Chrome", name: "Chrome" },
    { value: "Matte", name: "Matte" },
    { value: "Metal Flake", name: "Metal Flake" },
    { value: "Normal", name: "Normal" },
    { value: "Prismacolour White", name: "Prismacolour White" },
    { value: "Semigloss", name: "Semigloss" },
    { value: "Two-Tone Matte", name: "Two-Tone Matte" },
    { value: "Two-Tone Polished", name: "Two-Tone Polished" },
    { value: "Two-Tone Semigloss", name: "Two-Tone Semigloss" }
];

//Retain user search selections
let makeSelection = document.getElementById("searchMake").getAttribute("value");
let ModTypeSelection = document.getElementById("searchModType").getAttribute("value");

//Load Paint Type dropdown
new TomSelect('#searchMake', {
    options: [vehicleMakes],
    items: [makeSelection],
    placeholder: 'Make',
    labelField: 'name',
    searchField: ['name'],
    openOnFocus: true,
    highlight: false,
    hideSelected: true,
    selectOnTab: true,
    allowEmptyOption: true,
    diacritics: true
});

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
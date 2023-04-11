﻿/* Buscar Inmuebles */
$(document).ready(function () {
    $("#InmuebleBusqueda").on("input", function () {
        var busqueda = $("#InmuebleBusqueda").val();
        $.getJSON("/Inmueble/BuscarInmuebles", { busqueda: busqueda }, function (resultados) {
            var options = '<option value="">Selecciona una opción</option>';
            $.each(resultados, function (index, resultado) {
                options += '<option value="' + resultado.id + '">' + resultado.text + '</option>';
            });
            $("#selectInmueble").html(options);
        });
    });
});
$(document).ready(function () {
    // Escondemos el select al cargar la página
    $("#selectInmueble").hide();

    // Cuando hacemos clic en el input, mostramos el select
    $("#InmuebleBusqueda").click(function () {
        $("#selectInmueble").show();
    });

    // Cuando seleccionamos una opción del select, la agregamos al input
    $("#selectInmueble").change(function () {
        var selectedOption = $("#selectInmueble option:selected").text();
        $("#InmuebleBusqueda").val(selectedOption);
    });
});

/* Buscar Inquilinos */
$(document).ready(function () {
    $("#InquilinoBusqueda").on("input", function () {
        var busqueda = $("#InquilinoBusqueda").val();
        $.getJSON("/Inquilino/BuscarInquilinos", { busqueda: busqueda }, function (resultados) {
            var options = '<option value="">Selecciona una opción</option>';
            $.each(resultados, function (index, resultado) {
                options += '<option value="' + resultado.id + '">' + resultado.text + '</option>';
            });
            $("#selectInquilino").html(options);
        });
    });
});
$(document).ready(function () {
    // Escondemos el select al cargar la página
    $("#selectInquilino").hide();

    // Cuando hacemos clic en el input, mostramos el select
    $("#InmuebleBusqueda").click(function () {
        $("#selectInmueble").show();
    });

    // Cuando seleccionamos una opción del select, la agregamos al input
    $("#selectInmueble").change(function () {
        var selectedOption = $("#selectInmueble option:selected").text();
        $("#InmuebleBusqueda").val(selectedOption);
    });
});


/* Buscar Propietarios */
$(document).ready(function () {
    $("#PropietarioBusqueda").on("input", function () {
        var busqueda = $("#PropietarioBusqueda").val();
        $.getJSON("/Inmueble/BuscarPropietarios", { busqueda: busqueda }, function (resultados) {
            var options = '<option value="">Selecciona una opción</option>';
            $.each(resultados, function (index, resultado) {
                options += '<option value="' + resultado.id + '">' + resultado.text + '</option>';
            });
            $("#selectPropietario").html(options);
        });
    });
});
$(document).ready(function () {
    // Escondemos el select al cargar la página
    $("#selectPropietario").hide();

    // Cuando hacemos clic en el input, mostramos el select
    $("#PropietarioBusqueda").click(function () {
        $("#selectPropietario").show();
    });

    // Cuando seleccionamos una opción del select, la agregamos al input
    $("#selectPropietario").change(function () {
        var selectedOption = $("#selectPropietario option:selected").text();
        $("#PropietarioBusqueda").val(selectedOption);
    });
});

/* Mostrar modales de mensajes */
$(document).ready(function () {
    $('#modalOK').modal('show');
    setTimeout(function () {
        $('#modalOK').modal('hide');
    }, 6000);
});

$(document).ready(function () {
    $('#modalOK').on('hidden.bs.modal', function () {
        $(this).remove();
    });

    $('.close').click(function () {
        $('#modalOK').modal('hide');
    });
});

$(document).ready(function () {
    $('#modalMessage').modal('show');
    setTimeout(function () {
        $('#modalMessage').modal('hide');
    }, 6000);
});

$(document).ready(function () {
    $('#modalMessage').on('hidden.bs.modal', function () {
        $(this).remove();
    });

    $('.close').click(function () {
        $('#modalMessage').modal('hide');
    });
});


$(document).ready(function () {
    $('#modalError').modal('show');
    setTimeout(function () {
        $('#modalError').modal('hide');
    }, 6000);
});

$(document).ready(function () {
    $('#modalError').on('hidden.bs.modal', function () {
        $(this).remove();
    });

    $('.close').click(function () {
        $('#modalError').modal('hide');
    });
});
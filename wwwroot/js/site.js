﻿$(document).ready(function () {
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
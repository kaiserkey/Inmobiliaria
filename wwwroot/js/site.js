/* Abrir Modal de buscar propietario */
function abrirModal() {
    $('#modalBuscarPropietarios').modal('show');
    $('#tblPropietarios').empty();
}

function buscarPropietarios() {
    var nombre = $('#txtBuscar').val();
    $.getJSON('/Inmueble/BuscarPropietarios', { busqueda: nombre }, function (resultados) {
        $('#tblPropietarios').empty();
        $.each(resultados, function (index, resultado) {
            var fila = '<tr><td>' + resultado.idPropietario +
                '</td><td>' + resultado.nombre +
                '</td><td>' + resultado.apellido +
                '</td><td>' + resultado.telefono +
                '</td><td>' + resultado.email +
                '</td><td><button type="button" id="boton-general" class="btn btn-primary" onclick="seleccionarPropietario(' + resultado.idPropietario + ')">Seleccionar</button></td></tr>';
            $('#tblPropietarios').append(fila);
        });
    });
}

function seleccionarPropietario(idPropietario) {
    $('#IdPropietario').val(idPropietario);
    $('#modalBuscarPropietarios').modal('hide');
}

$(document).ready(function () {
    $('#txtBuscar').on('input', function () {
        buscarPropietarios();
    });
});

// Función para cerrar el modal al hacer clic en el botón
function cerrarModal() {
    $('#modalBuscarPropietarios').modal('hide');
} 

/* Abrir Modal de buscar inquilino */
function abrirModalInquilino() {
    $('#modalBuscarInquilinos').modal('show');
    $('#tblInquilinos').empty();
}

function buscarInquilino() {
    var nombre = $('#txtBuscar').val();
    $.getJSON('/Contrato/BuscarInquilinos', { busqueda: nombre }, function (resultados) {
        $('#tblInquilinos').empty();
        $.each(resultados, function (index, resultado) {
            var fila = '<tr><td>' + resultado.idInquilino +
                '</td><td>' + resultado.nombre +
                '</td><td>' + resultado.apellido +
                '</td><td>' + resultado.telefono +
                '</td><td>' + resultado.email +
                '</td><td><button type="button" id="boton-general" class="btn btn-primary" onclick="seleccionarInquilino(' + resultado.idInquilino + ')">Seleccionar</button></td></tr>';
            $('#tblInquilinos').append(fila);
        });
    });
}

function seleccionarInquilino(idInquilino) {
    $('#IdInquilino').val(idInquilino);
    $('#modalBuscarInquilinos').modal('hide');
}

$(document).ready(function () {
    $('#txtBuscar').on('input', function () {
        buscarInquilino();
    });
});

// Función para cerrar el modal al hacer clic en el botón
function cerrarModalInquilino() {
    $('#modalBuscarInquilinos').modal('hide');
} 

/* Modal  */

/* 
//buscar propietarios
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
}); */

/* 
$(document).ready(function () {
    $("#InmuebleBusqueda").on("input", function () {
        var busqueda = $("#InmuebleBusqueda").val();
        $.getJSON("/Contrato/BuscarInmuebles", { busqueda: busqueda }, function (resultados) {
            var options = '<option value="">Selecciona una opción</option>';
            $.each(resultados, function (index, resultado) {
                options += '<option value="' + resultado.id + '">' + resultado.id + '</option>';
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

$(document).ready(function () {
    $("#InquilinoBusqueda").on("input", function () {
        var busqueda = $("#InquilinoBusqueda").val();
        $.getJSON("/Contrato/BuscarInquilinos", { busqueda: busqueda }, function (resultados) {
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
    $("#InquilinoBusqueda").click(function () {
        $("#selectInquilino").show();
    });

    // Cuando seleccionamos una opción del select, la agregamos al input
    $("#selectInquilino").change(function () {
        var selectedOption = $("#selectInquilino option:selected").text();
        $("#InquilinoBusqueda").val(selectedOption);
    });
}); */

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
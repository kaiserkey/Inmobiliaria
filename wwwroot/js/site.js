/* Abrir Modal de buscar contrato */
function abrirModalContrato() {
    $('#modalBuscarContrato').modal('show');
    $('#tblContrato').empty();
}

function buscarContratos() {
    var busqueda = $('#txtBuscar').val();
    var opcion = $('#buscarContratoPor').val();
    $.getJSON('/Pago/BuscarContratos', { busqueda: busqueda, opcion: opcion }, function (resultados) {
        $('#tblContrato').empty();
        $.each(resultados, function (index, resultado) {
            var fila = '<tr><td>' + resultado.idContrato +
                '</td><td>' + resultado.idInmueble +
                '</td><td>' + resultado.nombre + resultado.apellido + 
                '</td><td>' + resultado.dni +
                '</td><td>' + resultado.fechaInicio +
                '</td><td>' + resultado.fechaFin +
                '</td><td><button type="button" id="boton-general" class="btn btn-primary" onclick="seleccionarContrato(' + resultado.idContrato + ')">Seleccionar</button></td></tr>';
            $('#tblContrato').append(fila);
        });
    });
}

function seleccionarContrato(idContrato) {
    $('#IdContrato').val(idContrato);
    $('#modalBuscarContrato').modal('hide');
}

// Función para cerrar el modal al hacer clic en el botón
function cerrarModalContrato() {
    $('#modalBuscarContrato').modal('hide');
}

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

// Función para cerrar el modal al hacer clic en el botón
function cerrarModalInquilino() {
    $('#modalBuscarInquilinos').modal('hide');
}

/* Abrir Modal de buscar inmuebles */
function abrirModalInmueble() {
    $('#modalBuscarInmuebles').modal('show');
    $('#tblInmuebles').empty();
}

function buscarInmueble() {
    var busqueda = $('#txtBuscar').val();
    var opcion = $('#buscarInmueblePor').val();
    $.getJSON('/Contrato/BuscarInmuebles', { busqueda: busqueda, opcion: opcion }, function (resultados) {
        $('#tblInmuebles').empty();
        $.each(resultados, function (index, resultado) {
            var fila = '<tr><td>' + resultado.idInmueble +
                '</td><td>' + resultado.tipo +
                '</td><td>' + resultado.coordenadas +
                '</td><td>' + resultado.precio +
                '</td><td>' + resultado.ambientes +
                '</td><td>' + resultado.uso +
                '</td><td><button type="button" id="boton-general" class="btn btn-primary" onclick="seleccionarInmueble(' + resultado.idInmueble + ')">Seleccionar</button></td></tr>';
            $('#tblInmuebles').append(fila);
        });
    });
}

function seleccionarInmueble(idInmueble) {
    $('#IdInmueble').val(idInmueble);
    $('#modalBuscarInmuebles').modal('hide');
}

// Función para cerrar el modal al hacer clic en el botón
function cerrarModalInmueble() {
    $('#modalBuscarInmuebles').modal('hide');
}


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
(function (appVentas) {

    appVentas.getModal = getModalContent;
    appVentas.closeModal = closeModal;
    appVentas.actualizarDataTable = actualizarDataTable; //método estandar para actualizar Data Datable

    return appVentas;

    function getModalContent(url, tamanio) {
        $.get(url, function (data) {
            $('#modal-dialog').removeClass('modal-xl');
            $('#modal-dialog').removeClass('modal-lg');
            $('#modal-dialog').removeClass('modal-sm');
            $('#modal-dialog').removeClass('modal-fullscreen');
            $('#modal-dialog').addClass(tamanio);
            $('.modal-body').html(data);
        })
    }

    function closeModal(option, data) {
        $("button[data-dismiss='modal'").click();
        $('.modal-body').html("");
        MostrarAlerta(option, data);
    }

    function MostrarAlerta(option, data) {
        if (option === 'create') {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Se creó el registro satisfactoriamente con el ID = ' + data + '!',
                showConfirmButton: true,
                showCloseButton: true,
                timer: 5000 //milisegundos
            })
        }
        else if (option === 'edit') {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Se editó el registro satisfactoriamente con el ID = ' + data.id , // + ' y nombre = ' + data.Modelo + '!',
                showConfirmButton: true,
                showCloseButton: true,
                timer: 5000 //milisegundos
            })
        }
        else if (option === 'delete') {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Se eliminó el registro satisfactoriamente', //con el ID = ' + data.id ,
                showConfirmButton: true,
                showCloseButton: true,
                timer: 5000 //milisegundos
            })
        }
        else if (option === 'compra') {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Se realizó la Transacción satisfactoriamente',
                showConfirmButton: true,
                showCloseButton: true,
                timer: 5000 //milisegundos
            })
        }
    }

    function actualizarDataTable(url, idDataTable){
        $.get(url, function (data) {
            $(idDataTable).html(data);
        })
    }
})(window.appVentas = window.appVentas || {});
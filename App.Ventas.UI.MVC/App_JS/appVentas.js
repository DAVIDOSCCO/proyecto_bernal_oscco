(function (appVentas) {

    appVentas.getModal = getModalContent;
    appVentas.closeModal = closeModal;

    return appVentas;

    function getModalContent(url) {
        $.get(url, function (data) {
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
                title: 'Se editó el registro satisfactoriamente..... ', 
                showConfirmButton: true,
                showCloseButton: true,
                timer: 5000 //milisegundos
            })
        }
        else if (option === 'delete') {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Se inactivó el registro satisfactoriamente con el ID = ' + data + '!',
                showConfirmButton: true,
                showCloseButton: true,
                timer: 5000 //milisegundos
            })
        }
    }

    function actualizarDataTable(url, idDataTable) {
        $.get(url, function (data) {
            $(idDataTable).html(data);
        })
    }
})(window.appVentas = window.appVentas || {});
(function (marca) {
    marca.success = successReload;

    $("#marcaTable").DataTable({
        "paging": true,
        "lengthChange": true,
        "seacrhing": true,
        "ordering": true,
        "autoWidth": true,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf"]
    })/*.buttons().container().appendTo("#categoriaTableContainer")*/;

    return marca;

    function successReload(data, option) {
        if (data.includes != null && (data.includes("createForm") || data.includes("editForm") || data.includes("deleteForm"))) {
            $(".modal-body").html(data);
        }
        else {
            appVentas.closeModal(option, data);
        }
    }

})(window.marca = window.marca || {});
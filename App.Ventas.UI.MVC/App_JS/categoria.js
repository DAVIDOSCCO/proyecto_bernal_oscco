(function (categoria) {
    categoria.success = successReload;
    categoria.searchByFilter = searchByFilter;

    $("#CreationDate").daterangepicker();
    $('#CreationDate').daterangepicker({
        //timePicker: true,
        //timePickerIncrement: 30,
        locale: {
            format: 'MM/DD/YYYY'
        }
    })

    $('.select2').select2()

    initPaginacion();

    return categoria;

    function successReload(data, option) {
        if (data.includes != null && (data.includes("createForm") || data.includes("editForm") || data.includes("deleteForm"))) {
            $(".modal-body").html(data);
        }
        else {
            appVentas.closeModal(option, data);
            getCategorias();
            //appVentas.actualizarDataTable('/Categoria/List', '#categoriaList');
        }
    }

    function initPaginacion() {
        //$("#categoriaTable").dataTable().fnDestroy(); //en caso de haber conflictos por intentar paginar varias veces, descomentar esta instrucción

        $("#categoriaTable").DataTable({
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "ordering": true,
            "autoWidth": true,
            "responsive": true,
            "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
        }).buttons().container().prependTo("#categoriaTableContainer");
    }

    function getCategorias() {
        var url = '/Categoria/List';
        console.log(url);
        $.get(url, function (data) {
            $('#categoriaList').html(data);
            initPaginacion();
        })
    }

    function searchByFilter() {
        var categoriaId = document.getElementById("categoriaId").value;
        var categoriaName = $("#categoriaName").val();
        var categoriaCreationDate = $("#CreationDate").val();
        var tipoCategoriaViewData = document.getElementById("TipoCategoriaViewData").value;
        var tipoCategoriaViewBag = $("#TipoCategoriaViewBag").val();

        console.log(categoriaId);
        console.log(categoriaName);
        console.log(categoriaCreationDate);
        console.log(tipoCategoriaViewData);
        console.log(tipoCategoriaViewBag);

        if (categoriaId == '') categoriaId = '-';
        if (categoriaName == '') categoriaName = '-';

        //var url = '/Categoria/ListByFilters?categoriaId=' + categoriaId + '&&categoriaName=' + categoriaName; //opcion 1: tradicional
        var url = '/Categoria/ListByFilters/' + categoriaId + '/' + categoriaName;
        console.log(url);

        $.get(url, function (data) {
            $('#categoriaList').html(data);
            initPaginacion();
        })

    }
})(window.categoria = window.categoria || {});
(function (marca) {
    marca.success = successReload;
    marca.searchByFilter = searchByFilter;

    $('.select2').select2()

    initPaginacion();

    return marca;

    function successReload(data, option) {
        if (data.includes != null && (data.includes("createForm") || data.includes("editForm") || data.includes("deleteForm"))) {
            $(".modal-body").html(data);
        }
        else {
            appVentas.closeModal(option, data);
            getMarcas();
        }
    }

    function initPaginacion() {
        //$("#categoriaTable").dataTable().fnDestroy(); //en caso de haber conflictos por intentar paginar varias veces, descomentar esta instrucción

        $("#marcaTable").DataTable({
            "paging": true,
            "lengthChange": true,
            "seacrhing": true,
            "ordering": true,
            "autoWidth": true,
            "responsive": true,
            "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
        }).buttons().container().prependTo("#marcaTableContainer");
    }
    function getMarcas() {
        var url = '/Marca/List';
        console.log(url);
        $.get(url, function (data) {
            $('#marcaList').html(data);
            initPaginacion();
        })
    }

    function searchByFilter() {
        var marcaId = document.getElementById("marcaId").value;
        var marcaName = $("#marcaName").val();
        //var marcaCreationDate = $("#CreationDate").val();
        //var tipoCategoriaViewData = document.getElementById("TipoCategoriaViewData").value;
        //var tipoCategoriaViewBag = $("#TipoCategoriaViewBag").val();

        console.log(marcaId);
        console.log(marcaName);
        //console.log(categoriaCreationDate);
        //console.log(tipoCategoriaViewData);
        //console.log(tipoCategoriaViewBag);

        if (marcaId == '') marcaId = '-';
        if (marcaName == '') marcaName = '-';

        //var url = '/Categoria/ListByFilters?categoriaId=' + categoriaId + '&&categoriaName=' + categoriaName; //opcion 1: tradicional
        var url = '/Marca/ListByFilters/' + marcaId + '/' + marcaName;
        console.log(url);

        $.get(url, function (data) {
            $('#marcaList').html(data);
            initPaginacion();
        })

    }
})(window.marca = window.marca || {});
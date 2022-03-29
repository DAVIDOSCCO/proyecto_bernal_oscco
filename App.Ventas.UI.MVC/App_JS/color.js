(function (color) {
    color.success = successReload;
    color.searchByFilter = searchByFilter;

    $('.select2').select2()

    initPaginacion();

    return color;

    function successReload(data, option) {
        if (data.includes != null && (data.includes("createForm") || data.includes("editForm") || data.includes("deleteForm"))) {
            $(".modal-body").html(data);
        }
        else {
            appVentas.closeModal(option, data);
            getColores();
        }
    }

    function initPaginacion() {
        $("#colorTable").dataTable().fnDestroy();

        //en caso de haber conflictos por intentar paginar varias veces, descomentar esta instrucción

        $("#colorTable").DataTable({
            "paging": true,
            "lengthChange": true,
            "seacrhing": true,
            "ordering": true,
            "autoWidth": true,
            "responsive": true,
            "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
        }).buttons().container().prependTo("#colorTableContainer");
    }
    function getColores() {
        var url = '/Color/List';
        console.log(url);
        $.get(url, function (data) {
            $('#colorList').html(data);
            initPaginacion();
        })
    }

    function searchByFilter() {
        var colorId = document.getElementById("colorId").value;
        var colorName = $("#colorName").val();
        //var colorCreationDate = $("#CreationDate").val();
        //var tipoCategoriaViewData = document.getElementById("TipoCategoriaViewData").value;
        //var tipoCategoriaViewBag = $("#TipoCategoriaViewBag").val();

        console.log(colorId);
        console.log(colorName);
        //console.log(categoriaCreationDate);
        //console.log(tipoCategoriaViewData);
        //console.log(tipoCategoriaViewBag);

        if (colorId == '') colorId = '-';
        if (colorName == '') colorName = '-';

        //var url = '/Categoria/ListByFilters?categoriaId=' + categoriaId + '&&categoriaName=' + categoriaName; //opcion 1: tradicional
        var url = '/Color/ListByFilters/' + colorId + '/' + colorName;
        console.log(url);

        $.get(url, function (data) {
            $('#colorList').html(data);
            initPaginacion();
        })

    }
})(window.color = window.color || {});
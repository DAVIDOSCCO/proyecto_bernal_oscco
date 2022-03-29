(function (talla) {
    talla.success = successReload;
    talla.searchByFilter = searchByFilter;

    $('.select2').select2()

    initPaginacion();

    return talla;

    function successReload(data, option) {
        if (data.includes != null && (data.includes("createForm") || data.includes("editForm") || data.includes("deleteForm"))) {
            $(".modal-body").html(data);
        }
        else {
            appVentas.closeModal(option, data);
            gettallas();
        }
    }

    function initPaginacion() {
        $("#tallaTable").dataTable().fnDestroy();

        //en caso de haber conflictos por intentar paginar varias veces, descomentar esta instrucción

        $("#tallaTable").DataTable({
            "paging": true,
            "lengthChange": true,
            "seacrhing": true,
            "ordering": true,
            "autoWidth": true,
            "responsive": true,
            "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
        }).buttons().container().prependTo("#tallaTableContainer");
    }
    function gettallas() {
        var url = '/talla/List';
        console.log(url);
        $.get(url, function (data) {
            $('#tallaList').html(data);
            initPaginacion();
        })
    }

    function searchByFilter() {
        var tallaId = document.getElementById("tallaId").value;
        var tallaName = $("#tallaName").val();
        //var tallaCreationDate = $("#CreationDate").val();
        //var tipoCategoriaViewData = document.getElementById("TipoCategoriaViewData").value;
        //var tipoCategoriaViewBag = $("#TipoCategoriaViewBag").val();

        console.log(tallaId);
        console.log(tallaName);
        //console.log(categoriaCreationDate);
        //console.log(tipoCategoriaViewData);
        //console.log(tipoCategoriaViewBag);

        if (tallaId == '') tallaId = '-';
        if (tallaName == '') tallaName = '-';

        //var url = '/Categoria/ListByFilters?categoriaId=' + categoriaId + '&&categoriaName=' + categoriaName; //opcion 1: tradicional
        var url = '/talla/ListByFilters/' + tallaId + '/' + tallaName;
        console.log(url);

        $.get(url, function (data) {
            $('#tallaList').html(data);
            initPaginacion();
        })

    }
})(window.talla = window.talla || {});
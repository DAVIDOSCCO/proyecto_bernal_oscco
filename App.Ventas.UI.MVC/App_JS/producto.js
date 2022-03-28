(function (producto) {
    producto.success = successReload;
    producto.searchByFilter = searchByFilter;
    producto.searchByFilter2 = searchByFilter2;

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
      

    return producto;

    function successReload(data, option) {
        if (data.includes != null && (data.includes("createForm") || data.includes("editForm") || data.includes("deleteForm") || data.includes("compraForm"))) {
            $(".modal-body").html(data);
        }
        else {
            appVentas.closeModal(option, data);
        }
    }

    function initPaginacion() {
        $("#ProductoTable").dataTable().fnDestroy(); //en caso de haber conflictos por intentar paginar varias veces, descomentar esta instrucción

        $("#ProductoTable").DataTable({
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "ordering": true,
            "autoWidth": true,
            "responsive": true,
            "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
        }).buttons().container().prependTo("#productoTableContainer");
    }

    function getProductos() {
        var url = '/Producto/List';
        console.log(url);
        $.get(url, function (data) {
            $('#productoList').html(data);
            initPaginacion();
        })
    }

    function searchByFilter() {
        var productoId = document.getElementById("productoId").value;
        var productoName = $("#productoName").val();
        var productoCreationDate = $("#CreationDate").val();
        var marcaViewData = document.getElementById("marcaViewData").value;
        var marcaViewBag = $("#marcaViewBag").val();

        console.log(productoId);
        console.log(productoName);
        console.log(productoCreationDate);
        console.log(marcaViewData);
        console.log(marcaViewBag);

        if (productoId == '') productoId = '-';
        if (productoName == '') productoName = '-';

        //var url = '/Categoria/ListByFilters?categoriaId=' + categoriaId + '&&categoriaName=' + categoriaName; //opcion 1: tradicional
        var url = '/Producto/ListByFilters/' + productoId + '/' + productoName;
        console.log(url);

        $.get(url, function (data) {
            $('#productoList').html(data);
            initPaginacion();
        })

    }

    function searchByFilter2() {
        var productoId = document.getElementById("productoId").value;
        var productoName = $("#productoName").val();
        var productoCreationDate = $("#CreationDate").val();
        var marcaViewData = document.getElementById("marcaViewData").value;
        var marcaViewBag = $("#marcaViewBag").val();

        console.log(productoId);
        console.log(productoName);
        console.log(productoCreationDate);
        console.log(marcaViewData);
        console.log(marcaViewBag);

        if (productoId == '') productoId = '-';
        if (productoName == '') productoName = '-';

        //var url = '/Categoria/ListByFilters?categoriaId=' + categoriaId + '&&categoriaName=' + categoriaName; //opcion 1: tradicional
        var url = '/Venta/ListByFilters/' + productoId + '/' + productoName;
        console.log(url);

        $.get(url, function (data) {
            $('#productoList').html(data);
            initPaginacion();
        })

    }

})(window.producto = window.producto || {});
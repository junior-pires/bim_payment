

function informarErro(texto) {
   
    $.alert({
        icon: 'fa fa-warning',
        type: 'red',
        title: 'Erro!',
        typeAnimated: true,
        content: texto,
    });

};

function informarSucesso(texto) {
    //var txt = "Operação realizada com sucesso!";
    //if (txt != undefined) {
    //    txt = texto;
    //} 

    $.alert({
        icon: 'fa fa-check',
        title: 'Sucesso!',       
        type: 'green',
        typeAnimated: true,
        content: texto,
    });
   
}

function confirmar(texto) {
    $.confirm({
        icon: 'fa fa-question',
        title: 'Confirmar!',
        type:'blue',
        content: texto,
        buttons: {
            SIM: function () {
                $.alert('Confirmed!');
            },
            NÃO: function () {
                $.alert('Canceled!');
            }
            
        }
    });
}

//$('#hospede').keypress(function (event) {
//    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
//        event.preventDefault();
//    }
//})

//$.LoadingOverlay("show");
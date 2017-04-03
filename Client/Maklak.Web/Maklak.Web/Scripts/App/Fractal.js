function Fractal() { }

Fractal.RefreshControl = function(elem) {   

    
    var url = 'Fractal/FractalControl';
    //var form = $("#Control_" + control_key);
    
    var element = $(elem);
    var form = element.closest('form');
    var keyValue = form.find("[name='Key']").val();

    var formValue = form.serialize();

    $.post(url, formValue, fillForm);

    function fillForm(data) {
        
        form.replaceWith(data);
        //$.validator.unobtrusive.parse(form);
    }
}
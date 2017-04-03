function Fractal() { }

Fractal.RefreshControl = function(control_key) {   

    
    var url = 'Fractal/FractalControl';
    var form = $("#Control_" + control_key);
    

    var formValue = form.serialize();

    $.post(url, formValue, fillForm);

    function fillForm(data) {
        
        form.replaceWith(data);
        //$.validator.unobtrusive.parse(form);
    }
}
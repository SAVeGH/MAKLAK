function Fractal() { }

Fractal.RefreshControl = function(control_key) {
    

    var url = GetURL() + '/Fractal/FractalControl';
    var sidValue = $("#SID").val();
    var form = element.closest('form');
    var formValue = 'Key=' + control_key + '&SID=' + sidValue;
    $.post(url, formValue);
}
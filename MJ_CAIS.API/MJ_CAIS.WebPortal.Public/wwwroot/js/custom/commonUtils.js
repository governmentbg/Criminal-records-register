$(document).ready(function () {
    var form = $('form');
    if (form.length > 0) {

        var labels = form.find('label');
        $(labels).each(function (idx, obj) {
            var label = $(obj);
            var labelFor = label.attr('for');

            var input = form.find('input[name=' + labelFor + ']');
            var hasValidation = input.attr('data-val');
            var isRequired =  input.attr('data-val-required');
            if (hasValidation && isRequired) {
                label.addClass('required');
            }
        });
    }
});
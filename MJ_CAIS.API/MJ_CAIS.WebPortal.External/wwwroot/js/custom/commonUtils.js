$(document).ready(function () {
    var form = $('form');
    if (form.length > 0) {

        var labels = form.find('label');
        $(labels).each(function (idx, obj) {
            var label = $(obj);
            var labelFor = label.attr('for');

            var input = form.find('input[name=' + labelFor + ']');
            if (input.length > 0) {

                var shouldAddRequired = false;

                if (input.hasClass('ui-igcombo-hidden-field')) {
                    // igCombo element
                    var comboWrapper = input.parents('.ui-igcombo-wrapper');
                    var comboId = comboWrapper.attr('id');
                    var validatorOptions = $('#' + comboId).igCombo('option', 'validatorOptions');
                    if (validatorOptions.required) {
                        shouldAddRequired = true;
                    }
                }
                else {
                    // Normal input element
                    var hasValidation = input.attr('data-val');
                    var isRequired = input.attr('data-val-required');
                    if (hasValidation && isRequired) {
                        shouldAddRequired = true;
                    }
                }

                if (shouldAddRequired) {
                    label.addClass('required');
                }
            }
        });
    }
});
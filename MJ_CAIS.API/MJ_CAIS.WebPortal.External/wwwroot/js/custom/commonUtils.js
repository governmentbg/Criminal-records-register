var cais = cais || {};

cais.configs = (function () {

    toasterSuccess = {
        "positionClass": "toast-bottom-center",
        "showDuration": "500",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "2000"
    };

    toasterError = {
        "positionClass": "toast-bottom-center",
        "showDuration": 500,
        "hideDuration": 0,
        "timeOut": 0,
        "extendedTimeOut": 0,
        "closeButton": true,
        "tapToDismiss": false
    }

    toasterWarning = {
        "positionClass": "toast-bottom-center",
        "showDuration": "500",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "2000",
        "closeButton": true,
        "tapToDismiss": false
    }

    return {
        toasterSuccess: toasterSuccess,
        toasterError: toasterError,
        toasterWarning: toasterWarning,
    };

})();

cais.commonUtils = (function () {

    var showSuccessMessageInToast = function (message) {
        toastr.options = cais.configs.toasterSuccess;

        toastr["success"](message).attr('style', 'max-width: 450px !important; width: 450px !important');
        $(".toast-message").attr('style', 'font-size: 17px !important; text-align: center;');
    };

    var showWarningMessageInToast = function (message, duration) {
        toastr.options = cais.configs.toasterWarning;
        if (duration) {
            toastr.options.timeOut = duration;
        }

        toastr["warning"](message).attr('style', 'max-width: 600px !important; width: 600px !important');
        $(".toast-message").attr('style', 'font-size: 17px !important; text-align: center;');
    };

    var showErrorMessageInToast = function (message) {
        toastr.options = cais.configs.toasterError;
        toastr["error"](message).attr('style', 'max-width: 600px !important; width: 600px !important');
        $(".toast-message").attr('style', 'font-size: 17px !important; text-align: center;');
    }

    return {
        showSuccessMessageInToast: showSuccessMessageInToast,
        showWarningMessageInToast: showWarningMessageInToast,
        showErrorMessageInToast: showErrorMessageInToast
    }
})();

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
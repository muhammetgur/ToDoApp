Polar = function () { };

Polar.ShowErrorMessage = function (message, title) {
    $("#pErrorMessage").html(message);
    $("#errorModalTitle").html(title === "" ? "Hata" : title);
    $("#errorModal").modal();
}

Polar.ShowSuccessMessage = function (message, title) {
    $("#pSuccessMessage").html(message);
    $("#successModalTitle").html(title === "" ? "İşlem Başarılı" : title);
    $("#successModal").modal();
}

jQuery(document).ready(function () {

    // Basic Form Validation
    jQuery(".validate-form").validate({
        highlight: function (element) {
            jQuery(element).closest(".form-group").removeClass("has-success").addClass("has-error");
        },
        success: function (element) {
            jQuery(element).closest(".form-group").removeClass("has-error");
        }
    });

    // Form Toggles
    jQuery(".toggles").toggles();
    jQuery(".toggle").toggles({
        text: {
            on: "",
            off: ""
        }
    });

    $.datepicker.setDefaults($.datepicker.regional["tr"]);
    jQuery(".datepicker").datepicker();
});

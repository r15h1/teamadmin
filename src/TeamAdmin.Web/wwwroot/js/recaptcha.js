$(document).ready(function () {
    $("#myform").submit(function (e) {
        $("#captchavalidation").hide();
        $('input[type=submit]').prop('disabled', true);

        var result = $("#myform")[0].checkValidity();
        if (result === false) { return cancelSubmit(e); }

        $('.validation-summary-errors > ul').empty();
        var recaptcha = $("#g-recaptcha-response").val();
        if (recaptcha === "") {
            $("#captchavalidation").show();
            cancelSubmit(e);
        } else {
            $.ajax({
                async: false,
                url: "/api/recaptcha",
                method: "POST",
                data: { "captchaResponse": recaptcha },

                success: function (response) {
                    if (response.success !== true) {
                        $("#captchavalidation").show();
                        cancelSubmit(e);
                    }
                },

                error: function (jqXHR, textStatus, errorThrown) {
                    $("#captchavalidation").show();
                    cancelSubmit(e);
                }
            });
        }

        function cancelSubmit(evt) {
            console.log('cancelling submit...');
            $('input[type=submit]').prop('disabled', false);
            evt.preventDefault();
            return false;
        }

    });
});

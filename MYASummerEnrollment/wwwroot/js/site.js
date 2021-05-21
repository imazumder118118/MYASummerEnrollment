// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// The maximum characters can be entered into a text field
function MaxText(field, maxText) {
    if (field.value.length > maxText)
        field.value = field.value.substring(0, maxText);
}

//Datepicker
$(function () {
    $("#DateOfBirth").mask("99/99/9999", { placeholder: " " });

    $('#ApplicantSignatureDate').datepicker({
        inline: true
    });
});


//Mask for Phone Numbers
$(function () {
    $("#HomePhone").mask("(999)999-9999", { placeholder: " " });
    $("#CellPhone").mask("(999)999-9999", { placeholder: " " });
    $("#ParentPhone").mask("(999)999-9999", { placeholder: " " });
    $("#ParentCell").mask("(999)999-9999", { placeholder: " " });
});


//Mask for SSN
$(function () {
    $("#SSN").mask("9999", { placeholder: " " });
});




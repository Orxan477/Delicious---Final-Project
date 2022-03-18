$(document).ready(function () {
    $(document).on("click", "#teamModal", function (ev) {
        var personId = ev.target.nextElementSibling.value;
        $.ajax({
            url: "/Team/ModalPartial",
            data: {
                id: personId
            },
            type: "GET",
            success: function (result) {
                $(".team-modal-append").append(result)
            }
        })
    })

    $(document).on("click", "#close-modal-partial", function (ev) {
        console.log(ev);
        $(".team-modal-append").children().remove();
    })
})
// ---------------------------------------------------------------
// AJAX: Trainer dropdown filter on the Classes page.
// When the dropdown changes, fetch the matching classes partial
// view from the server and swap only the #classesContainer content.
// ---------------------------------------------------------------
$(function () {
    $('#trainerFilter').on('change', function () {
        var trainerId = $(this).val();

        $.ajax({
            url: '/Classes/FilterByTrainer',
            type: 'GET',
            data: { trainerId: trainerId },
            beforeSend: function () {
                $('#classesContainer').css('opacity', '0.5');
            },
            success: function (result) {
                $('#classesContainer').html(result);
            },
            error: function () {
                $('#classesContainer').html('<div class="alert alert-danger">Could not load classes. Please try again.</div>');
            },
            complete: function () {
                $('#classesContainer').css('opacity', '1');
            }
        });
    });
});

// ---------------------------------------------------------------
// SweetAlert2 delete confirmation.
// Any form/link with class "js-delete-confirm" will show a
// SweetAlert confirmation dialog before submitting/navigating.
// ---------------------------------------------------------------
$(function () {
    $(document).on('submit', 'form.js-delete-confirm', function (e) {
        var form = this;

        if (form.dataset.confirmed === 'true') {
            return true;
        }

        e.preventDefault();

        var itemName = $(form).data('item-name') || 'this item';

        Swal.fire({
            title: 'Are you sure?',
            text: 'This will permanently delete ' + itemName + '.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it',
            cancelButtonText: 'Cancel',
            confirmButtonColor: '#d33'
        }).then(function (result) {
            if (result.isConfirmed) {
                form.dataset.confirmed = 'true';
                form.submit();
            }
        });
    });
});

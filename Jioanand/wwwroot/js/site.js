// Common JavaScript functions for the application

// Initialize all tooltips
const initTooltips = () => {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
};

// Initialize all popovers
const initPopovers = () => {
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
};

// Initialize DataTables
const initDataTables = () => {
    $('.datatable').DataTable({
        responsive: true,
        language: {
            search: "",
            searchPlaceholder: "Search..."
        },
        dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6"f>>' +
             '<"row"<"col-sm-12"tr>>' +
             '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
    });
};

// Format currency
const formatCurrency = (amount) => {
    return new Intl.NumberFormat('en-IN', {
        style: 'currency',
        currency: 'INR',
        minimumFractionDigits: 2
    }).format(amount);
};

// Format date
const formatDate = (date) => {
    return new Date(date).toLocaleDateString('en-IN', {
        day: '2-digit',
        month: 'short',
        year: 'numeric'
    });
};

// Show loading spinner
const showLoading = () => {
    const spinner = document.createElement('div');
    spinner.className = 'position-fixed top-50 start-50 translate-middle loading-spinner';
    spinner.innerHTML = `
        <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Loading...</span>
        </div>
    `;
    document.body.appendChild(spinner);
};

// Hide loading spinner
const hideLoading = () => {
    const spinner = document.querySelector('.loading-spinner');
    if (spinner) {
        spinner.remove();
    }
};

// Handle form submission with AJAX
const handleAjaxForm = (formSelector, successCallback) => {
    $(formSelector).on('submit', function (e) {
        e.preventDefault();
        const form = $(this);
        const url = form.attr('action');
        const method = form.attr('method') || 'POST';
        const data = new FormData(form[0]);

        showLoading();

        $.ajax({
            url: url,
            method: method,
            data: data,
            processData: false,
            contentType: false,
            success: function (response) {
                hideLoading();
                if (typeof successCallback === 'function') {
                    successCallback(response);
                }
            },
            error: function (xhr) {
                hideLoading();
                const message = xhr.responseJSON?.message || 'An error occurred. Please try again.';
                showAlert('error', message);
            }
        });
    });
};

// Show alert message
const showAlert = (type, message) => {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3`;
    alertDiv.setAttribute('role', 'alert');
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;
    document.body.appendChild(alertDiv);

    // Auto dismiss after 5 seconds
    setTimeout(() => {
        const alert = bootstrap.Alert.getOrCreateInstance(alertDiv);
        alert.close();
    }, 5000);
};

// Initialize on document ready
$(document).ready(function () {
    initTooltips();
    initPopovers();
    initDataTables();

    // Handle mobile sidebar toggle
    $('.navbar-toggler').on('click', function () {
        $('.sidebar').toggleClass('show');
    });

    // Close sidebar on mobile when clicking outside
    $(document).on('click', function (e) {
        if (!$(e.target).closest('.sidebar, .navbar-toggler').length) {
            $('.sidebar').removeClass('show');
        }
    });
});

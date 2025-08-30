// Room Management JavaScript

// Show loading overlay during AJAX requests
function showLoading() {
    const overlay = document.createElement('div');
    overlay.className = 'loading-overlay';
    overlay.innerHTML = '<div class="loading-spinner"></div>';
    document.body.appendChild(overlay);
}

function hideLoading() {
    const overlay = document.querySelector('.loading-overlay');
    if (overlay) {
        overlay.remove();
    }
}

// Handle room availability check
function checkAvailability(date) {
    showLoading();
    window.location.href = `/Room/Availability?date=${date}`;
}

// Handle room booking initiation
function initiateBooking(roomId, date) {
    showLoading();
    window.location.href = `/Booking/Create?roomId=${roomId}&date=${date}`;
}

// Handle room status updates
async function updateRoomStatus(roomId, status) {
    try {
        showLoading();
        const response = await fetch('/api/rooms/status', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ roomId, status })
        });

        if (!response.ok) {
            throw new Error('Failed to update room status');
        }

        // Update UI without page reload
        const statusBadge = document.querySelector(`#room-${roomId} .status-badge`);
        if (statusBadge) {
            statusBadge.className = `status-badge status-badge--${status.toLowerCase()}`;
            statusBadge.textContent = status;
        }
    } catch (error) {
        console.error('Error updating room status:', error);
        alert('Failed to update room status. Please try again.');
    } finally {
        hideLoading();
    }
}

// Mobile-friendly date picker initialization
function initializeDatePicker() {
    const dateInput = document.querySelector('.date-picker');
    if (dateInput) {
        // Add touch-friendly date picker for mobile
        dateInput.addEventListener('touchstart', function(e) {
            e.preventDefault();
            this.showPicker();
        });

        // Handle date changes
        dateInput.addEventListener('change', function() {
            checkAvailability(this.value);
        });
    }
}

// Initialize room grid layout
function initializeRoomGrid() {
    const grid = document.querySelector('.room-grid');
    if (grid) {
        // Add touch swipe detection
        let touchStartX = 0;
        let touchEndX = 0;

        grid.addEventListener('touchstart', e => {
            touchStartX = e.changedTouches[0].screenX;
        }, false);

        grid.addEventListener('touchend', e => {
            touchEndX = e.changedTouches[0].screenX;
            handleSwipe();
        }, false);

        function handleSwipe() {
            const swipeThreshold = 50;
            const diff = touchEndX - touchStartX;

            if (Math.abs(diff) > swipeThreshold) {
                // Implement swipe actions if needed
                console.log('Swiped:', diff > 0 ? 'right' : 'left');
            }
        }
    }
}

// Handle room filtering
function filterRooms() {
    const searchInput = document.querySelector('#roomSearch');
    const typeFilter = document.querySelector('#typeFilter');
    const cards = document.querySelectorAll('.room-card');

    const searchTerm = searchInput.value.toLowerCase();
    const selectedType = typeFilter.value;

    cards.forEach(card => {
        const roomNumber = card.querySelector('.room-number').textContent.toLowerCase();
        const roomType = card.querySelector('.room-type').textContent;
        
        const matchesSearch = roomNumber.includes(searchTerm);
        const matchesType = selectedType === '' || roomType === selectedType;

        card.style.display = matchesSearch && matchesType ? 'block' : 'none';
    });
}

// Initialize tooltips
function initializeTooltips() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

// Document ready handler
document.addEventListener('DOMContentLoaded', function() {
    initializeDatePicker();
    initializeRoomGrid();
    initializeTooltips();

    // Add scroll to top button for mobile
    const scrollButton = document.createElement('button');
    scrollButton.className = 'scroll-top-button';
    scrollButton.innerHTML = '↑';
    document.body.appendChild(scrollButton);

    scrollButton.addEventListener('click', () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });

    // Show/hide scroll button based on scroll position
    window.addEventListener('scroll', () => {
        if (window.pageYOffset > 100) {
            scrollButton.classList.add('visible');
        } else {
            scrollButton.classList.remove('visible');
        }
    });
});

// Wait for document ready
document.addEventListener('DOMContentLoaded', function () {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Add animation class to newly completed tasks
    const checkboxes = document.querySelectorAll('.task-complete-checkbox');
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            if (this.checked) {
                const taskCard = this.closest('.card');
                taskCard.classList.add('task-complete-animation');

                // Change card appearance
                taskCard.classList.add('border-success');
                const cardHeader = taskCard.querySelector('.card-header');
                if (cardHeader) {
                    cardHeader.classList.add('bg-success', 'text-white');
                }
            }
        });
    });

    // Filter tasks functionality
    const filterButtons = document.querySelectorAll('.task-filter-btn');
    if (filterButtons.length > 0) {
        filterButtons.forEach(button => {
            button.addEventListener('click', function () {
                // Remove active class from all buttons
                filterButtons.forEach(btn => btn.classList.remove('active'));

                // Add active class to clicked button
                this.classList.add('active');

                // Get filter value
                const filterValue = this.getAttribute('data-filter');

                // Get all task cards
                const taskCards = document.querySelectorAll('.task-card');

                // Filter tasks
                taskCards.forEach(card => {
                    if (filterValue === 'all') {
                        card.style.display = 'block';
                    } else if (filterValue === 'complete') {
                        card.style.display = card.classList.contains('task-complete') ? 'block' : 'none';
                    } else if (filterValue === 'incomplete') {
                        card.style.display = !card.classList.contains('task-complete') ? 'block' : 'none';
                    }
                });
            });
        });
    }

    // Due date warning highlight
    function highlightUpcomingDueDates() {
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        const dueDateElements = document.querySelectorAll('[data-due-date]');
        dueDateElements.forEach(element => {
            const dueDateStr = element.getAttribute('data-due-date');
            if (dueDateStr) {
                const dueDate = new Date(dueDateStr);
                const timeDiff = dueDate.getTime() - today.getTime();
                const daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));

                // If due date is within 2 days and task is not complete
                const taskCard = element.closest('.card');
                if (daysDiff <= 2 && daysDiff >= 0 && !taskCard.classList.contains('task-complete')) {
                    element.classList.add('text-danger', 'fw-bold');
                    element.innerHTML = `<i class="bi bi-exclamation-circle"></i> ${element.innerHTML}`;
                }
            }
        });
    }

    highlightUpcomingDueDates();
});
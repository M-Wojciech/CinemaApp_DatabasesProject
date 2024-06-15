document.addEventListener('DOMContentLoaded', () => {
    const days = ['Nd', 'Pn', 'Wt', 'Śr', 'Cz', 'Pt', 'Sb'];
    const days2 = ['Niedziela', 'Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota'];
    const months = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
    const todayDate = new Date();
    const today = todayDate.getDay(); // Current day of the week (0 - Sunday, 6 - Saturday)

    const links = document.querySelectorAll('#weekdays .day');
    const dateLabel = document.getElementById('dzisiaj');

    // Function to get the next date for a specific day
    const getNextDate = (currentDay, targetDay) => {
        const resultDate = new Date(todayDate);
        resultDate.setDate(todayDate.getDate() + ((targetDay - currentDay + 7) % 7));
        return resultDate;
    };

    // Function to update the display text and style
    const updateDisplay = (dayIndex, isToday = false) => {
        const nextDate = getNextDate(today, dayIndex);
        const date = nextDate.getDate();
        const month = nextDate.getMonth();
        dateLabel.textContent = `${days2[dayIndex]} ${date}.${months[month]}`;

        // Remove previous selections
        links.forEach(link => link.classList.remove('selected'));
        links.forEach(link => link.classList.remove('active'));

        // Calculate the index of the link to add the 'selected' class
        const linkIndex = (dayIndex - today + 7) % 7;

        // Add selection for the chosen day
        links[linkIndex].classList.add('selected');
    };

    // Initialization
    links.forEach((link, index) => {
        if (index === 0) {
            link.textContent = 'Dziś'; // First link is always "Today"
            link.classList.add('active');
        } else {
            link.textContent = days[(today + index) % 7];
        }

        // Add click handler
        link.addEventListener('click', (event) => {
            event.preventDefault();
            const dayIndex = (today + index) % 7;
            updateDisplay(dayIndex, index === 0);
        });
    });

    // Initial update on load
    updateDisplay(today, true);
});

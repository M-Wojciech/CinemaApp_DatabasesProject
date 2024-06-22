const d = new Date();
const dd = d.getDay();
const allButtons = document.querySelectorAll('.showtimes button');
console.log(allButtons)
allButtons.forEach(button => {
    const terminDate = new Date(button.dataset.termin);
    console.log(terminDate)
    const screeningDayIndex = terminDate.getDay(); // 0 - воскресенье, 6 - суббота
    console.log(screeningDayIndex)
    if (screeningDayIndex == dd - 1) {
        button.parentElement.style.display = '';
    } else {
        button.parentElement.style.display = 'none';
    }
});
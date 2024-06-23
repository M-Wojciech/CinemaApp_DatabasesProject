document.addEventListener('DOMContentLoaded', () => {
    // Найдем элементы по id
    const decreaseBtn = document.getElementById('decrease');
    const increaseBtn = document.getElementById('increase');
    const quantityInput = document.getElementById('quantity');
    const nextBtn = document.getElementById('nextBtn');

    // Обработчик нажатия на кнопку уменьшения
    decreaseBtn.addEventListener('click', () => {
        let currentValue = parseInt(quantityInput.value);
        if (!isNaN(currentValue) && currentValue > 0) {
            quantityInput.value = currentValue - 1;
        }
    });

    // Обработчик нажатия на кнопку увеличения
    increaseBtn.addEventListener('click', () => {
        let currentValue = parseInt(quantityInput.value);
        if (!isNaN(currentValue) && currentValue < 100) {
            quantityInput.value = currentValue + 1;
        }
    });

    // Проверка значения ввода на изменение
    quantityInput.addEventListener('input', () => {
        let value = parseInt(quantityInput.value);
        if (isNaN(value) || value < 0) {
            quantityInput.value = 0;
        } else if (value > 100) {
            quantityInput.value = 100;
        }
    });

    nextBtn.addEventListener('click', () => {
        let currentValue = parseInt(quantityInput.value);
        if (currentValue > 0) {
            // Замените 'your-next-page.html' на URL вашей следующей страницы
            window.location.href = '/Transakcja/Index';
        } else {
            alert('Nie wybrano ilości biletów.');
        }
    });
});
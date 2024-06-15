function submitForm(action, checkValidity) {
    const form = document.getElementById('myForm');
    if (checkValidity && !form.checkValidity()) {
        form.reportValidity();
        return;
    }
    form.action = action;
    form.submit();
}
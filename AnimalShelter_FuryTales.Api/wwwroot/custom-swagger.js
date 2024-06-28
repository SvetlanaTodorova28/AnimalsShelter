window.onload = function() {
    document.querySelector('.auth-container .authorize').addEventListener('click', function() {
        const authInput = document.querySelector('.auth-container input[type="text"]');
        if (authInput && !authInput.value.startsWith('Bearer ')) {
            authInput.value = `Bearer ${authInput.value}`;
        }
    });
};


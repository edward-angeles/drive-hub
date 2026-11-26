import {auth, signInWithEmailAndPassword} from '/js/firebase.js';
const loginForm = document.getElementById('login-form');
loginForm.addEventListener('submit', async (event) => {
    event.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const messageDiv = document.getElementById('message');
    try{
        await signInWithEmailAndPassword(auth, email, password);
        messageDiv.textContent = 'Login sucessful';
        window.location.href = '/';
    }catch (error){
        messageDiv.textContent = error.message;
    }
});    
const { JSDOM } = require('jsdom');
const fs = require('fs');
const path = require('path');

const html = fs.readFileSync(path.resolve(__dirname, '../index.html'), 'utf8');

let dom;
let document;

describe('Login Page', () => {
    beforeEach(() => {
        dom = new JSDOM(html, { runScripts: "dangerously", resources: "usable" });
        document = dom.window.document;

        const scriptEl = document.createElement("script");
        scriptEl.textContent = fs.readFileSync(path.resolve(__dirname, '../login.js'), 'utf8');
        document.body.appendChild(scriptEl);
    });

    test('"Login successful" for valid credentials', () => {
        const usernameInput = document.getElementById('username');
        const passwordInput = document.getElementById('password');
        const loginForm = document.getElementById('login-form');
        const loginMessage = document.getElementById('login-message');

        usernameInput.value = 'user1';
        passwordInput.value = 'password';

        const event = new dom.window.Event('submit');
        loginForm.dispatchEvent(event);

        expect(loginMessage.textContent).toBe('Login successful!');
        expect(loginMessage.style.color).toBe('green');
    });

    test('"Invalid username or password" for invalid credentials', () => {
        const usernameInput = document.getElementById('username');
        const passwordInput = document.getElementById('password');
        const loginForm = document.getElementById('login-form');
        const loginMessage = document.getElementById('login-message');

        usernameInput.value = 'user1';
        passwordInput.value = 'wrongpassword';

        const event = new dom.window.Event('submit');
        loginForm.dispatchEvent(event);

        expect(loginMessage.textContent).toBe('Invalid username or password.');
        expect(loginMessage.style.color).toBe('red');
    });
});

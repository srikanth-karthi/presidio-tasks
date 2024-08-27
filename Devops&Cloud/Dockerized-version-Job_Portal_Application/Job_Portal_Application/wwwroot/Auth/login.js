import { showToast } from "../Package/toaster.js";
import { fetchData } from "../Package/api.js";

const loginForm = document.getElementById('loginForm');
const emailInput = document.getElementById('email');
const passwordInput = document.getElementById('password');
const userTypeInputs = document.querySelectorAll('input[name="user-type"]');

function getQueryParam(param) {
  const urlParams = new URLSearchParams(window.location.search);
  return urlParams.get(param);
}

function removeQueryParam(param) {
  const url = new URL(window.location);
  const urlParams = new URLSearchParams(url.search);
  urlParams.delete(param);
  url.search = urlParams.toString();
  window.history.replaceState({}, document.title, url.toString());
}

const authid = getQueryParam('authid');
if (authid) {
    if (authid === '2') {
        showToast('success', 'Success', 'Logout Successful.');
    } else if (authid === '3') {
        showToast('warning', 'Warning', 'Please Login.');
    }
    removeQueryParam('authid');
}

loginForm.addEventListener('submit', async (event) => {
    event.preventDefault();

    const email = emailInput.value.trim().toLowerCase();
    const password = passwordInput.value.trim();
    const userType = Array.from(userTypeInputs).find(input => input.checked)?.id;

    if (!email || !password) {
        showToast('warning', 'Warning', 'Please fill in all required fields.');
        return;
    }

    const loginData = { email, password };
    const loginUrl = userType === 'job-seeker' ? 'api/User/login' : 'api/Company/login';

    try {
        const response = await fetchData(loginUrl, 'POST', loginData, false, true);
        localStorage.setItem("authToken", response.token);
        if(loginData.email=="admin@jobportal.com") 
            {
                window.location.href="../Admin/Admin.html?authid=1" 
            }
            else 
            {window.location.href = userType === 'job-seeker' ? "../dashboard/dashboard.html?authid=1" : "/Jobposter/dashboard/dashboard.html?authid=1";
            }
    } catch (error) {
        console.error("Error updating profile details:", error);
        const errorMessage = error.message.includes("401") || error.message.includes("400") ? 'Invalid credentials.' : 'Please login again later.';
        showToast('error', 'Login Failed', errorMessage);
    }
});

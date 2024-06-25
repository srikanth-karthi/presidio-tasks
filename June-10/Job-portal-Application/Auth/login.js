import { showToast } from "../Pakage/toster.js";
import { fetchData } from "../Pakage/api.js";
document.addEventListener('DOMContentLoaded', (event) => {

  function getQueryParam(param) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(param);
  }


  const authid = getQueryParam('authid');

if(authid==2)
  {
    showToast('success', 'Success', 'Logout Sucessfull.');
  }
  else if(authid==3)
    {
      showToast('warning', 'Warning', 'Please Login.');
    }
    const loginForm = document.getElementById('loginForm');

    loginForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

        const userType = document.querySelector('input[name="user-type"]:checked').id;

        if (!email.trim() || !password.trim()) {
          showToast('warning', 'Warning', 'Please fill in all required fields.');

            return;
        }

        const loginData = {
            email: email,
            password: password
        };

        let loginUrl;
        if (userType === 'job-seeker') {
            loginUrl = 'api/User/login';
        } else if (userType === 'company') {
            loginUrl = 'api/Company/login';
        }

        try {
            const response = await fetchData(loginUrl, 'POST', loginData);
            localStorage.setItem("authToken", response.token);
            window.location.href = "../dashboard/dashboard.html?authid=1";
        } catch (error) {
            console.error("Error updating profile details:", error);
        
            if (error.message.includes("401") || error.message.includes("400")) {
              showToast('error', 'Login Failed', 'Invalid credentials.');
            } 
            else {
              showToast('error', 'An error occurred', ' Please login again Later');
            }
          }
        
    });
});


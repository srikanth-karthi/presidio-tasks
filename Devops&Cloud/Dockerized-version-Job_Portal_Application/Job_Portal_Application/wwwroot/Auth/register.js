import { showToast } from "../Package/toaster.js";
import { fetchData } from "../Package/api.js";

document.addEventListener('DOMContentLoaded', () => {
    const jobSeekerTab = document.getElementById('job-seeker');
    const companyTab = document.getElementById('company');
    const jobSeekerForm = document.getElementById('job-seeker-form');
    const companyForm = document.getElementById('company-form');
    const form = document.getElementById('registration-form');

    companyForm.style.display = 'none';

    jobSeekerTab.addEventListener('change', () => {
        if (jobSeekerTab.checked) {
            jobSeekerForm.style.display = 'block';
            companyForm.style.display = 'none';
        }
    });

    companyTab.addEventListener('change', () => {
        if (companyTab.checked) {
            jobSeekerForm.style.display = 'none';
            companyForm.style.display = 'block';
        }
    });

    form.addEventListener('submit', async (event) => {
        event.preventDefault();

        let isValid = true;
        let data;
        let url = '';

        const validateEmail = (email) => {
            return String(email)
                .toLowerCase()
                .match(/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        };

        const validateField = (fieldId, errorMessage) => {
            const fieldValue = document.getElementById(fieldId).value.trim();
            if (!fieldValue) {
                showToast('warning', 'Warning', errorMessage);
                isValid = false;
                return false;
            }
            return true;
        };

        const validateCommonFields = (fields) => {
            return fields.every(([fieldId, errorMessage]) => validateField(fieldId, errorMessage));
        };

        const validatePasswordLength = (passwordId) => {
            const passwordLength = document.getElementById(passwordId).value.length;
            if (passwordLength < 8) {
                showToast('warning', 'Warning', 'The Password must contain at least 8 characters.');
                isValid = false;
                return false;
            }
            return true;
        };

        if (jobSeekerTab.checked) {
            url = 'api/User/register';
            data = {
                email: document.getElementById('email').value.toLowerCase(),
                name: document.getElementById('name').value,
                password: document.getElementById('password').value,
                dob: document.getElementById('dob').value,
                city: document.getElementById('city').value,
            };

            const jobSeekerFields = [
                ['email', 'Please fill in the Email field.'],
                ['name', 'Please fill in the Name field.'],
                ['password', 'Please fill in the Password field.'],
                ['dob', 'Please fill in the Date of Birth field.'],
                ['city', 'Please fill in the City field.'],
            ];

            if (!validateCommonFields(jobSeekerFields)) return;
            if (!validateEmail(data.email)) {
                showToast('warning', 'Warning', 'Enter a valid Email.');
                isValid = false;
                return false;
            }
            if (!validatePasswordLength('password')) return;

        } else if (companyTab.checked) {
            url = 'api/Company/register';
            data = {
                companyName: document.getElementById('companyName').value,
                email: document.getElementById('companyEmail').value.toLowerCase(),
                password: document.getElementById('companyPassword').value,
                companyAddress: document.getElementById('companyAddress').value,
                city: document.getElementById('companyCity').value,
            };

            const companyFields = [
                ['companyName', 'Please fill in the Company Name field.'],
                ['companyEmail', 'Please fill in the Company Email field.'],
                ['companyPassword', 'Please fill in the Company Password field.'],
                ['companyAddress', 'Please fill in the Company Address field.'],
                ['companyCity', 'Please fill in the City field.'],
            ];

            if (!validateCommonFields(companyFields)) return;
            if (!validateEmail(data.email)) {
                showToast('warning', 'Warning', 'Enter a valid Email.');
                isValid = false;
                return false;
            }
            if (data.companyAddress.length > 200) {
                showToast('warning', 'Warning', 'The Company Address cannot exceed 200 characters.');
                isValid = false;
                return false;
            }
            if (!validatePasswordLength('companyPassword')) return;
        }

        if (isValid) {
            try {
                await fetchData(url, 'POST', data);
                showToast('success', 'Success', 'Registration Successful.');
                showToast('success', 'Success', 'Please login.');
            } catch (error) {
                console.error("Error updating profile details:", error);
                if (error.message.includes("401") || error.message.includes("400")) {
                    showToast('error', 'Registration Failed', 'Please register again.');
                } else if (error.message.includes("409")) {
                    showToast('warning', 'Registration Failed', 'Email Already Registered.');
                } else {
                    showToast('error', 'An error occurred', 'Please register again later.');
                }
            }
        }
    });
});

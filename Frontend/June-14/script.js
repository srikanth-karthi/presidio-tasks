const professions = ['Engineer', 'Doctor', 'Teacher', 'Lawyer', 'Artist'];
const professionInput = document.getElementById('profession');
const professionList = document.getElementById('professionList');

professions.forEach(profession => {
    const option = document.createElement('option');
    option.value = profession;
    professionList.appendChild(option);
});

function calculateAge(dob) {
    const birthDate = new Date(dob);
    const difference = Date.now() - birthDate.getTime();
    const ageDate = new Date(difference);
    return Math.abs(ageDate.getUTCFullYear() - 1970);
}

document.getElementById('dob').addEventListener('change', function() {
    const age = calculateAge(this.value);
    document.getElementById('age').value = age;
});

document.getElementById('registrationForm').addEventListener('submit', function(event) {
    event.preventDefault();
    let valid = true;

    const name = document.getElementById('name').value.trim();
    if (name === '') {
        valid = false;
        document.getElementById('nameError').classList.remove('hidden');
    } else {
        document.getElementById('nameError').classList.add('hidden');
    }

    const phone = document.getElementById('phone').value.trim();
    const phonePattern = /^[0-9]{10}$/;
    if (!phonePattern.test(phone)) {
        valid = false;
        document.getElementById('phoneError').classList.remove('hidden');
    } else {
        document.getElementById('phoneError').classList.add('hidden');
    }

    const dob = document.getElementById('dob').value.trim();
    if (dob === '') {
        valid = false;
        document.getElementById('dobError').classList.remove('hidden');
    } else {
        document.getElementById('dobError').classList.add('hidden');
    }

    const email = document.getElementById('email').value.trim();
    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    if (!emailPattern.test(email)) {
        valid = false;
        document.getElementById('emailError').classList.remove('hidden');
    } else {
        document.getElementById('emailError').classList.add('hidden');
    }

    const gender = document.querySelector('input[name="gender"]:checked');
    if (!gender) {
        valid = false;
        document.getElementById('genderError').classList.remove('hidden');
    } else {
        document.getElementById('genderError').classList.add('hidden');
    }

    const qualification = document.querySelectorAll('input[type="checkbox"]:checked');
    if (qualification.length === 0) {
        valid = false;
        document.getElementById('qualificationError').classList.remove('hidden');
    } else {
        document.getElementById('qualificationError').classList.add('hidden');
    }

    const profession = professionInput.value.trim();
    if (profession === '') {
        valid = false;
        document.getElementById('professionError').classList.remove('hidden');
    } else {
        document.getElementById('professionError').classList.add('hidden');
        if (!professions.includes(profession)) {
            professions.push(profession);
            const newOption = document.createElement('option');
            newOption.value = profession;
            professionList.appendChild(newOption);
        }
    }

    if (valid) {
        alert('Registration successful!');

    }
});
import { showToast } from "../Package/toaster.js";
import { fetchData } from "../Package/api.js";
if (!localStorage.getItem('authToken')) {
    window.location.href = "/Auth/login.html?authid=3";
  }

  handleAuthId();
function handleAuthId() {
    const authid = getQueryParam('authid');
    if (authid == 1) {
      showToast('success', 'Success', 'Login Successful.');
      removeQueryParam('authid');
    }
  }
  
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
  

let skills = [];
let titles = [];

fetchTitles();
fetchSkills();
document.getElementById('skills-link').addEventListener('click', () => {
    document.getElementById('skills-page').style.display = 'block';
    document.getElementById('titles-page').style.display = 'none';
});

document.getElementById('titles-link').addEventListener('click', () => {
    document.getElementById('skills-page').style.display = 'none';
    document.getElementById('titles-page').style.display = 'block';
});

document.getElementById('add-skill-btn').addEventListener('click', addSkill);
document.getElementById('add-title-btn').addEventListener('click', addTitle);

async function fetchSkills() {
    try {
        skills=  await fetchData("api/Skill");
        renderSkills();
    } catch (error) {
        console.error('Error fetching skills:', error);
    }
}

async function fetchTitles() {
    try {
        titles = await fetchData("api/Title");
        renderTitles();
    } catch (error) {
        console.error('Error fetching titles:', error);
    }
}

async function addSkill() {
    const skillInput = document.getElementById('skill-input');
    if (skillInput.value) {
        try {
      
       skills.unshift( await fetchData("api/Admin/skills","POST",skillInput.value))
            renderSkills();
            showToast('success', 'Success', 'Skill added Successful.');
        } catch (error) {
            if (error.message.includes("409")) {
                showToast('warning', 'Warning', 'Skill already exist');
             }
             else            console.error('Error fetching skills:', error);
        }
        skillInput.value = '';
        renderSkills();
    }
}

async function addTitle() {
    const titleInput = document.getElementById('title-input');
    if (titleInput.value) {
        try {
      
            titles.unshift( await fetchData("api/Admin/titles","POST",titleInput.value))
            showToast('success', 'Success', 'Title added Successful.');
            renderTitles();
             } catch (error) {
                 if (error.message.includes("409")) {
                    showToast('warning', 'Warning', 'Titile already exist');
                 }
                 else                 console.error('Error fetching skills:', error);
             }
        titleInput.value = '';
    }
}

function renderSkills() {
    const skillsList = document.getElementById('skills-list');
    skillsList.innerHTML = '';
    console.log(skills)
    skills.forEach(skill => {
        const li = document.createElement('li');
        li.innerHTML = `${skill.skillName} <i class="ion-ios-trash" onclick="deleteskills( '${skill.skillId}')"></i>`;

        skillsList.appendChild(li);
    });
}

function renderTitles() {
    const titlesList = document.getElementById('titles-list');
    titlesList.innerHTML = '';
    titles.forEach(title => {
        const li = document.createElement('li');
        li.textContent = title.titleName;
        li.innerHTML = `${title.titleName} <i class="ion-ios-trash" onclick="deletetitle( '${title.titleId}')"></i>`;

        titlesList.appendChild(li);
    });
}
window.searchSkills=searchSkills
window.searchTitles=searchTitles
window.deleteskills=deleteskills    
window.deletetitle=deletetitle    
function searchSkills() {
    const searchValue = document.getElementById('skill-search').value.toLowerCase();
    const filteredSkills = skills.filter(skill => skill.skillName.toLowerCase().includes(searchValue));
    const skillsList = document.getElementById('skills-list');
    skillsList.innerHTML = '';
    filteredSkills.forEach(skill => {
        const li = document.createElement('li');

        li.innerHTML = `${skill.skillName} <i class="ion-ios-trash" onclick="deleteskills('${skill.skillId}')"></i>`;

        skillsList.appendChild(li);
    });
}

function searchTitles() {
    const searchValue = document.getElementById('title-search').value.toLowerCase();
    const filteredTitles = titles.filter(title => title.titleName.toLowerCase().includes(searchValue));
    const titlesList = document.getElementById('titles-list');
    titlesList.innerHTML = '';
    filteredTitles.forEach(title => {
        const li = document.createElement('li');
        li.innerHTML = `${title.titleName} <i class="ion-ios-trash" onclick="deletetitle('${title.titleId}')"></i>`;
        titlesList.appendChild(li);
    });
}

async function deleteskills(skillid)
{
    try {
   await fetchData(`api/Admin/skills/${skillid}`,"DELETE")
      skills=skills.filter(skill => skill.skillId!=skillid)
        renderSkills();
        showToast('success', 'Success', 'Skill Deleted Successful.');
         } catch (error) {
             console.error('Error fetching skills:', error);
         }
}

async function deletetitle(titleid)
{
    try {   await fetchData(`api/Admin/titles/${titleid}`,"DELETE")
      titles=titles.filter(title => title.titleId!=titleid)
      showToast('success', 'Success', 'Title Deleted Successful.');
        renderTitles();
         } catch (error) {
             console.error('Error fetching skills:', error);
         }
}
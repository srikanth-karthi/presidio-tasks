document.addEventListener("DOMContentLoaded", function() {
    const canvas = document.getElementById("donutChart");
    const ctx = canvas.getContext("2d");

    const data = {
        unsuitable: 60,
        interviewed: 40
    };

    function drawSegment(startAngle, endAngle, color) {
        ctx.beginPath();
        ctx.arc(100, 100, 70, startAngle, endAngle);
        ctx.arc(100, 100, 50, endAngle, startAngle, true);
        ctx.fillStyle = color;
        ctx.fill();
    }

    function animateChart(duration) {
        const total = data.unsuitable + data.interviewed;
        const unsuitableAngle = (data.unsuitable / total) * 2 * Math.PI;
        const interviewedAngle = (data.interviewed / total) * 2 * Math.PI;

        let start = null;

        function animate(time) {
            if (!start) start = time;
            const progress = Math.min((time - start) / duration, 1);

            ctx.clearRect(0, 0, canvas.width, canvas.height);

            drawSegment(0, unsuitableAngle * progress, "#4e44ce");
            drawSegment(unsuitableAngle * progress, unsuitableAngle * progress + interviewedAngle * progress, "#e0e7ff");

            if (progress < 1) {
                requestAnimationFrame(animate);
            }
        }

        requestAnimationFrame(animate);
    }

    animateChart(2000); 


    document.querySelector(".chart-legend .element:nth-child(1) .legend-text .percentage").textContent = `${data.unsuitable}%`;
    document.querySelector(".chart-legend .element:nth-child(2) .legend-text .percentage").textContent = `${data.interviewed}%`;
});

 document.getElementById('cross').style.display='none';

    

document.getElementById('menuButton').addEventListener('click', function() {
    document.getElementById('sidebar').classList.toggle('hidden');
 document.getElementById('company-logo').style.display = 'none';

    document.getElementById('cross').style.display='block';
    document.querySelector('.main-content').classList.toggle('expanded');
});

document.getElementById('cross').addEventListener('click', function() {
    const sidebar = document.getElementById('sidebar');
    const companyLogo = document.getElementById('company-logo');
    const crossButton = document.getElementById('cross');
    const mainContent = document.querySelector('.main-content');

    sidebar.classList.toggle('hidden');
    mainContent.classList.toggle('expanded');

    if (companyLogo.style.display === 'none' || companyLogo.style.display === '') {
        companyLogo.style.display = 'block';
        crossButton.style.display = 'none';
    } else {
        companyLogo.style.display = 'none';
        crossButton.style.display = 'block';
    }
});

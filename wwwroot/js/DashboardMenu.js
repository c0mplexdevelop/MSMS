const hamMenu = document.querySelector('.ham-menu');
const hamDropDown = document.querySelector('.ham-dropdown');
const hamContainer = document.querySelector('.ham-container');

hamMenu.addEventListener('click', () => {

    hamMenu.classList.toggle('active');
    hamDropDown.classList.toggle('active');
    hamContainer.classList.toggle('active');
});
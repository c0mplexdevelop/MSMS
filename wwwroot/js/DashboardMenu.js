//document.addEventListener('DOMContentLoaded', () => {
//    const menuItems = document.querySelectorAll('.menu-item');

//    const currentActiveItem = sessionStorage.getItem('activeMenuItem');
//    if (currentActiveItem) {
//        menuItems.forEach(item => {
//            if (item.getAttribute('data-ref') == currentActiveItem) {
//                item.classList.add('active');
//            } else {
//                item.classList.remove('active');
//            }
//        });
//    }

//    menuItems.forEach(item => {
//        item.addEventListener('click', () => {
//            sessionStorage.setItem('activeMenuItem', item.getAttribute('data-ref'));
//            menuItems.forEach(item => item.classList.remove('active'));
//            item.classList.add('active');
//        })
//    })
//});
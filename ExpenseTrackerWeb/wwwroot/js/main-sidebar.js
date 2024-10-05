document.getElementById('burger').addEventListener('click', function (event) {
  event.stopPropagation();
  
  let sidebar = document.getElementById('sidebar');
  if (sidebar.classList.contains('hidden')) {
    sidebar.classList.remove('hidden')
    sidebar.classList.add('show');
  } else {
    sidebar.classList.remove('show');
    sidebar.classList.add('hidden');
  }
});

document.addEventListener('click', function (event) {
  let sidebar = document.getElementById('sidebar');
  let burger = document.getElementById('burger');

  if (!sidebar.contains(event.target) && !burger.contains(event.target)) {
    sidebar.classList.remove('show');
    sidebar.classList.add('hidden');
  }
});
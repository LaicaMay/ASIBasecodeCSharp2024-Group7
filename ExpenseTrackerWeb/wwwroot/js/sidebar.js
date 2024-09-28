document.getElementById('hamburger').addEventListener('click', function() {
  document.getElementById('sidebar').classList.add('show');
  document.getElementById('overlay').classList.add('show');
});

document.getElementById('close-btn').addEventListener('click', function() {
  document.getElementById('sidebar').classList.remove('show');
  document.getElementById('overlay').classList.remove('show');
});

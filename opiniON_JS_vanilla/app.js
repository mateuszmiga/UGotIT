
const input = document.getElementById("search-input");
const searchContainer = document.querySelector("search-container");

input.addEventListener("keyup", function(event) {
  if (event.keyCode === 13) {
    submitInput();
  }
});

function submitInput() {
  const userInput = input.value;
  console.log(userInput)

}





const opinionsUrl = "https://example.com/api/opinions";

async function getOpinions() {
  try {
    const response = await fetch(opinionsUrl);
    const opinions = await response.json();
    return opinions;
  } catch (error) {
    console.error(error);
  }
}

async function renderOpinions() {
  const opinions = await getOpinions();
  const opinionsContainer = document.getElementById("opinions");
  opinions.forEach(opinion => {
    const opinionElement = document.createElement("div");
    opinionElement.classList.add("opinion");
    opinionElement.innerHTML = `
      <h2>${opinion.title}</h2>
      <p>${opinion.content}</p>
      <p>By ${opinion.author}</p>
    `;
    opinionsContainer.appendChild(opinionElement);
  });
}


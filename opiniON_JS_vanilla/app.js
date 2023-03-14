
const input = document.getElementById("search-input");
const searchContainer = document.querySelector(".search-container");

input.addEventListener("keyup", function(event) {
  if (event.keyCode === 13) {
    submitInput();
  }
});

function submitInput() {
  const userInput = input.value;
  console.log(userInput);
  searchContainer.classList.add("hidden");
  renderProducts(userInput);
}


const url = "https://localhost:7042/api/Product?productName=";


async function getProducts(userInput) {
  const productsUrl = `${url}${userInput}`;
  try {
    const response = await fetch(productsUrl);
    const products = await response.json();
    return products;
  } catch (error) {
    console.error(error);
  }
}

async function renderProducts(userInput) {
  const products = await getProducts(userInput);
  const productContainer = document.getElementById("products");
  products.forEach(product => {
    const productElement = document.createElement("div");
    productElement.classList.add("product");
    productElement.innerHTML = `
      <h2>${product.productName}</h2>
      <p>${product.photoUrl}</p>
      <p>${product.url}</p>
    `;
    productContainer.appendChild(productElement);
  });
}


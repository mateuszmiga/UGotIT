
const input = document.getElementById("search-input");
const searchContainer = document.querySelector(".search-container");

input.addEventListener("keyup", function(event) {
  if (event.keyCode === 13) {
    resetPreviousSearch();
    submitInput();
  }
});

function submitInput() {
  const userInput = input.value;
  console.log(userInput);
  // searchContainer.classList.add("hidden");
  renderProducts(userInput);
}


const url = "https://localhost:7042/api/Product?productName=";


//sending GET request with userInput searching item
async function getProducts(userInput) {
  const productsUrl = `${url}${userInput}`;
  try {
    const response = await fetch(productsUrl);
    const productsJson = await response.json();
    return productsJson;
  } catch (error) {
    console.error(error);
  }
}

async function renderProducts(userInput) {
  const products = await getProducts(userInput);
  console.log(products);
  const productContainer = document.querySelector(".products");
  products.forEach(product => {
    const productElement = document.createElement("div");

    productElement.classList.add("product");
    productElement.innerHTML = `
      <div class="product-img">
        <img src=\"https:${product.photoUrl}\">
      </div>
      <div class="product-description">
        <h2>${product.productName}</h2>
        <p>${product.url}</p>
      </div>
    `;
    productContainer.appendChild(productElement);
  });
}

//reseting output of previous search - refreshing site effect without server request
function resetPreviousSearch(){
  const previousProducts = document.querySelector(".products"); 
  const newProducts = document.createElement('div');
  newProducts.classList.add('products');
  previousProducts.parentNode.replaceChild(newProducts, previousProducts);
  

  //repositioning of search input
  const searchContainer= document.querySelector(".search-container");
  searchContainer.classList.add("with-products");
  const searchInput = document.querySelector("#search-input");
  searchInput.classList.add("with-products");
}

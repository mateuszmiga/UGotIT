
const amazonLogoUrl = 'https://www.amazon.pl/ref=nav_logo';
const x_comLogoUrl = 'https://assets.x-kom.pl/public-spa/xkom/7cbf82dd32ab7e88.svg';
const komputronikLogoUrl = 'https://prowly-uploads.s3.eu-west-1.amazonaws.com/uploads/press_rooms/company_logos/1209/72b83a4a25be6621ae462be8af6edc3f.jpg';
const ceneoLogoUrl = 'https://www.ceneo.pl/Content/img/icons/logo-ceneo-simple-orange.svg';
const annemedLogoUrl = '';

const input = document.getElementById("search-input");
const searchContainer = document.querySelector(".search-container");
const searchProductUrl = "https://localhost:7042/api/Product?productName=";
const opinionUrl = 'https://localhost:7042/api/Review?productUrl=https%3A%2F%2F';

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




//sending GET request with userInput searching item
async function getProducts(userInput) {
  const searchProductUrl = `${url}${userInput}`;
  try {
    const response = await fetch(searchProductUrl);
    const productsJson = await response.json();
    return productsJson;
  } catch (error) {
    console.error(error);
  }
}

async function renderProducts(userInput) {
  const products = await getProducts(userInput);
  console.log(products);
  const productsContainer = document.querySelector(".products");
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
    productsContainer.appendChild(productElement);
  });
}

async function renderOpinions(userChosenProduct){
  const opinions = await getOpinions(userChosenProduct.url);
  console.log(opinions);
  const opinionsContainer = document.querySelector(".opinions");
  opinions.forEach(opinion =>{
    const opinionElement = document.createElement("article");
    opinionElement.classList.add("opinion");
    productElement.innerHTML = `
      <div>
        <img src=${getLogoOpinionSource(opinion.sourcePage)} alt="Author Profile Pic">
      </div>
      <div>
        <h2>Ipnone 14 max pro</h2>
        <p>Article content goes here.</p>
      <div class="author-info">
    `;
  })
} 

//sending GET request to obtain opinions for pointed product
async function getOpinions(productUrl) {
  const getOpinionsUrl = `${opinionUrl}${productUrl}`;
  try {
    const response = await fetch(getOpinionsUrl);
    const opinionsJson = await response.json();
    return opinionsJson;
  } catch (error) {
    console.error(error);
  }
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
  // const searchInput = document.querySelector("#search-input");
  // searchInput.classList.add("with-products");
}

function getLogoOpinionSource(sourcePageUrl) {
  let opinionSourceLogo = "";
  if (sourcePageUrl.includes("amazon")) opinionSourceLogo = amazonLogoUrl;
  else if (sourcePageUrl.includes("x-com")) opinionSourceLogo = x_comLogoUrl;
  else if (sourcePageUrl.includes("komputronik")) opinionSourceLogo = komputronikLogoUrl;
  if (sourcePageUrl.includes("ceneo")) opinionSourceLogo = ceneoLogoUrl;
  else opinionSourceLogo = annemedLogoUrl;
}


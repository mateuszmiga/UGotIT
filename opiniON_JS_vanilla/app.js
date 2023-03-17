
const amazonLogoUrl = 'https://img.etimg.com/thumb/msid-59738992,width-640,resizemode-4,imgsize-25499/amazon.jpg';
const x_comLogoUrl = 'https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/X-kom_logo_ver2018.png/240px-X-kom_logo_ver2018.png';
const komputronikLogoUrl = 'https://prowly-uploads.s3.eu-west-1.amazonaws.com/uploads/press_rooms/company_logos/1209/72b83a4a25be6621ae462be8af6edc3f.jpg';
const ceneoLogoUrl = 'https://www.ceneo.pl/Content/img/icons/logo-ceneo-simple-orange.svg';
const noNameLogoUrl = '';
const searchProductUrl = "https://localhost:7042/api/Product?productName=";
const opinionUrl = 'https://localhost:7042/api/Review?productUrl=https%3A%2F%2F';
let products = ['girls','just','wanna','have','fun'];

const input = document.getElementById("search-input");
const searchContainer = document.querySelector(".search-container");


//Event listeners
input.addEventListener("keyup", function(event) {
  if (event.keyCode === 13) {
    resetPreviousSearch();
    resetReviews();
    submitInput();
  }
});

//set products html element event Listeners
function setProductsEventListeners() {
  const product1 = document.querySelector(".product1");
  const product2 = document.querySelector(".product2");
  const product3 = document.querySelector(".product3");
  const product4 = document.querySelector(".product4");
  product1.addEventListener("click", () => {productHandler(products[0])});
  product2.addEventListener("click", () => {productHandler(products[1])});
  product3.addEventListener("click", () => {productHandler(products[2])});
  product4.addEventListener("click", () => {productHandler(products[3])});
}

function productHandler(item){
  console.log(item.productName);
  renderOpinions(item);
}

// productLayout.addEventListener(click, function() {
//   productLayout.
// })

function submitInput() {
  const userInput = input.value;
  console.log(userInput);
  // searchContainer.classList.add("hidden");
  renderProducts(userInput);
}


async function renderProducts(userInput) {
  products = await getProducts(userInput);
  console.log(products);
  const productsContainer = document.querySelector(".products");
  let i=1;
  products.forEach(product => {
    const productElement = document.createElement("div");

    productElement.classList.add(`product${i}`);
    // productElement.classList.add(`${i}`);
    productElement.innerHTML = `
      <div class="product-img">
        <img src=\"https:${product.photoUrl}\">
      </div>
      <div class="product-description">
        <h2>${product.productName}</h2>
        <p>${product.url}</p>
      </div>
    `;
    i ++;
    productsContainer.appendChild(productElement);
  });
  setProductsEventListeners();
}

async function renderOpinions(userChosenProduct){
  const opinions = await getOpinions(userChosenProduct.url); 
  console.log(opinions);
  resetPreviousSearch();
  const opinionsContainer = document.querySelector(".opinions");
  opinions.forEach(opinion =>{
    const opinionElement = document.createElement("article");
    opinionElement.classList.add("opinion");
    opinionElement.innerHTML = `
      <div class="logo-img">
        <img src=${getLogoOpinionSource(opinion.sourcePage)} alt="opinion provider logo">
      </div>
      <div class="opinion-rating_description">
        <h2>ocena: ${opinion.rating}</h2>
        <p>${opinion.reviewContent}</p>
      <div class="author-info">        
        // <p>${opinion.userName}</p>
      </div>
    `;
    opinionsContainer.appendChild(opinionElement);
  });
} 

//sending GET request with userInput searching item
async function getProducts(userInput) {
  products = null;
  const entireSearchProductUrl = `${searchProductUrl}${userInput}`;
  try {
    const response = await fetch(entireSearchProductUrl);
    const productsJson = await response.json();
    return productsJson;
  } catch (error) {
    console.error(error);
  }
}

//sending GET request to obtain opinions for pointed product
async function getOpinions(productUrl) {
  const productSlicedUrl = productUrl
  .slice(8);
  // .replace("/","%");

  const getOpinionsUrl = `${opinionUrl}${productSlicedUrl}`;
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
  const searchContainer = document.querySelector(".search-container");
  searchContainer.classList.add("with-products");
  // const searchInput = document.querySelector("#search-input");
  // searchInput.classList.add("with-products");
}

function resetReviews(){
  const reviews = document.querySelector(".opinions"); 
  const newReviews = document.createElement('div');
  newReviews.classList.add('opinions');
  reviews.parentNode.replaceChild(newReviews, reviews);
}

function getLogoOpinionSource(sourcePageUrl) {
  let opinionSourceLogo = "";
  if (sourcePageUrl.includes("amazon")) opinionSourceLogo = amazonLogoUrl;
  else if (sourcePageUrl.includes("x-com")) opinionSourceLogo = x_comLogoUrl;
  else if (sourcePageUrl.includes("komputronik")) opinionSourceLogo = komputronikLogoUrl;
  if (sourcePageUrl.includes("ceneo")) opinionSourceLogo = ceneoLogoUrl;
  else opinionSourceLogo = noNameLogoUrl;
  return opinionSourceLogo;
}



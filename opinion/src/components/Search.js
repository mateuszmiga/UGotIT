const Search = () => {

    const handleClick = () => {
        console.log('szukam...')
    }  

    return (  
        <div className="searchButton">
            <button onClick={handleClick}>Szukaj opini...</button>
        </div>
    );
}
 
export default Search;
import Card from "./CardV3"
//import cardData from "../assets/itemData.json"
import { useState, useEffect } from 'react'
function CardListSearch ()  {

    const [cardData, setState] = useState([]);
    const [query, setQuery] = useState('');
    useEffect(() => {
        fetch(`https://localhost:7192/api/ItemsWebAPI?searchText=${query}`)
            .then(response => response.json())
            .then(data => setState(data))
            .catch(error => {
                console.log(error);
            })
    }, [query])
    function searchQuery(evt) {
        const value = document.querySelector('[name="searchText"]').value;
        //alert('value: ' + value);
        setQuery(value);

    }

    return (
        <div id="cardListSearch">
            <div className="row justify-content-start mb-3">
                <div className="col-3">
                    <input type="text" name="searchText" className="form-control" placeholder="Type your query.. " />

                </div>
                <div className="col text-left">
                    <button className="btn btn-primary" type="button" onClick={searchQuery}>Search</button>
                </div>
            </div>
            <div id="cardList" className="row">
                {cardData.map((obj) => (
                    <Card
                        key={obj.itemId}
                        itemId={obj.itemId}
                        itemName={obj.itemName}
                        itemDescription={obj.itemDescription}
                        itemCost={obj.itemCost}
                        itemImage={obj.itemImage}
                    />

                ))}
            </div>
        </div>

    )
}
export default CardListSearch;
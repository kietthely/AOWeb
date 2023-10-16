import Card from "./CardV3"
//import cardData from "../assets/itemData.json"
import { useState, useEffect } from 'react'
const CardList = ({ }) => {

    const [cardData, setState] = useState([]);
    useEffect(() => {
        fetch("https://localhost:7192/api/ItemsWebAPI")
            .then(response => response.json())
            .then(data => setState(data))
            .catch(error => {
                console.log(error);
            })
    })

    return (
        <div className="row">
            {cardData.map((obj) => (
                <Card
                    key={obj.itemId}
                    itemId={obj.itemId}
                    itemName={obj.itemName}
                    itemDescription={obj.itemDescription}
                    itemCost={obj.itemCost}
                    itemImage={ obj.itemImage}
                />
            
            ))}
        </div>
    
    )
}

export default CardList
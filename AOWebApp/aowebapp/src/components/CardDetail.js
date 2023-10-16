import { Link , useParams} from 'react-router-dom';
import { useState, useEffect } from 'react'
const CardDetail = ({ }) => {
    let params = useParams();
    const [cardData, setState] = useState([]);
    const [itemId, setItemId] = useState(params.itemId);
    useEffect(() => {
        fetch(`https://localhost:7192/api/ItemsWebAPI?${itemId}`)
            .then(response => response.json())
            .then(data => setState(data))
            .catch(error => {
                console.log(error);
            })
    })


    return (
        <div className="card col-4 mb-2" style={{ width: 18 + 'rem' }} >
            <img className="card-img-top" src={cardData.itemImage} alt={"Image of " + cardData.itemImage} />
            <div className="card-body">
                <h5 className="card-title">{cardData.itemName}</h5>
                <p className="card-text">{cardData.itemDescription}</p>
                <p className="card-text">${cardData.itemCost}</p>
                <Link to="/Products" className="btn btn-outline-primary">Back To Products</Link>
            </div>

        </div>

    )
}
export default CardDetail;
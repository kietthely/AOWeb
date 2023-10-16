import { Link } from 'react-router-dom';


const CardV3 = ({ itemId, itemName, itemDescription, itemImage, itemCost }) => (


    <div className="card col-4 mb-2" style={{ width: 18 + 'rem' }} >
        <img className="card-img-top" src={itemImage} alt={"Image of " + itemImage} />
        <div className="card-body">
            <h5 className="card-title">{itemName}</h5>
            <p className="card-text">{itemDescription}</p>
            <p className="card-text">${itemCost}</p>
            <Link to={"/Products/" + itemId} className="btn btn-outline-primary">View Detail</Link>
        </div>

    </div>
)
       


export default CardV3
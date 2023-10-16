function CardV2({ itemId, itemName, itemDescription, itemCost, itemImage }) {
    return (
        <div className="card col-4 mb-2" style={{ width: 18 + 'rem' }} >
            <img className="card-img-top" src={itemImage} alt={"Image of " + itemImage} />
            <div className="card-body">
                <h5 className="card-title">{itemName}</h5>
                <p className="card-text">{itemDescription}</p>
                <p className="card-text">${itemCost}</p>
                <a href="#" className="btn btn-outline-primary">Go {itemId}</a>
            </div>

        </div>
    );


}

export default CardV2
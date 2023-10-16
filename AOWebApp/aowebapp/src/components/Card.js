function Card(props) {
    return (
        <div className="card col-4 mb-2" style={{width: 18 + 'rem'}} >
            <img className="card-img-top" src={props.itemImage} alt={"Image of " + props.itemImage} />
            <div className="card-body">
                    <h5 className="card-title">{ props.itemName}</h5>
                    <p className="card-text">{props.itemDescription}</p>
                    <p className="card-text">${props.itemCost }</p>
                    <a href="#" className="btn btn-outline-primary">Go { props.itemId}</a>
            </div>
            
        </div>
    
    )

}
export default Card
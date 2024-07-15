using UnityEngine;

public class Ingredient : MonoBehaviour/*, IDragHandler, IEndDragHandler, IBeginDragHandler*/
{
    private IngredientType type;
    private Vector3 offset;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //TODO sound
        // soundPlayer.playSound(pick);
        
    }

    void OnMouseDrag()
    {
        transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }
    
    void OnMouseUpAsButton()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        //TODO stop highlight
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.bodyType = RigidbodyType2D.Kinematic;
        //TODO highlight
    }


}

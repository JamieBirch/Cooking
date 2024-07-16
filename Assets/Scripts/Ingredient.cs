using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientType type;
    public Sprite sprite;
    private Vector3 offset;
    private Rigidbody2D rb;
    private bool isHeld;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        DragSetup();
    }

    public void DragSetup()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.bodyType = RigidbodyType2D.Kinematic;
        isHeld = true;
        //TODO highlight
    }

    void OnMouseDrag()
    {
        FollowCursor();
    }

    public void FollowCursor()
    {
        transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }

    void OnMouseUpAsButton()
    {
        Drop();
    }

    public void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isHeld = false;
        //TODO stop highlight
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}

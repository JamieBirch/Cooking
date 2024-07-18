using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientType type;
    public Sprite sprite;
    private Rigidbody2D rb;
    private bool isHeld;
    private Vector2 lastMousePosition;

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
        rb.bodyType = RigidbodyType2D.Kinematic;
        isHeld = true;
    }

    void OnMouseDrag()
    {
        FollowCursor();
    }

    public void FollowCursor()
    {
        transform.localPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUpAsButton()
    {
        Drop();
    }

    public void Drop()
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition).normalized;
        rb.velocity = direction * 2;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isHeld = false;
        lastMousePosition = Vector2.zero;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}

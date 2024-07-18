using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientType type;
    public Sprite sprite;
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
    }

    void OnMouseUpAsButton()
    {
        Drop();
    }

    public void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isHeld = false;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}

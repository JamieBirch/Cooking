using UnityEngine;

public class IngredientSupplier : MonoBehaviour
{
    // Prefab of the ingredient to be supplied.
    public GameObject ingredientPrefab;

    // Reference to the currently active ingredient being interacted with.
    private Ingredient activeIngredient;

    // Called when the mouse button is pressed down on the supplier object.
    void OnMouseDown()
    {
        // Instantiate a new ingredient at the mouse position and activate its drag behavior.
        activeIngredient = Instantiate(ingredientPrefab, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<Ingredient>();
        activeIngredient.DragSetup();
    }
    
    // Called while the mouse button is held down and the mouse is dragged.
    void OnMouseDrag()
    {
        // Move the active ingredient to follow the cursor position.
        activeIngredient.FollowCursor();
    }
    
    // Called when the mouse button is released after being pressed down on the supplier object.
    void OnMouseUp()
    {
        // Drop the active ingredient and reset the reference.
        activeIngredient.Drop();
        activeIngredient = null;
    }
}

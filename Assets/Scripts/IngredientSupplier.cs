using UnityEngine;

public class IngredientSupplier : MonoBehaviour
{
    public GameObject ingredientPrefab;
    private Ingredient activeIngredient;

    void OnMouseDown()
    {
        //TODO Sound
        activeIngredient = Instantiate(ingredientPrefab, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<Ingredient>();
        activeIngredient.DragSetup();
    }
    
    void OnMouseDrag()
    {
        activeIngredient.FollowCursor();
    }
    
    void OnMouseUp()
    {
        activeIngredient.Drop();
        activeIngredient = null;
    }

}

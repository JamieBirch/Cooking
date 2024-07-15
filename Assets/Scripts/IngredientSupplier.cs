using UnityEngine;

public class IngredientSupplier : MonoBehaviour
{
    public GameObject ingredientPrefab;
    private Ingredient activeIngredient;

    private void OnMouseOver()
    {
        //TODO highlight
    }
    
    void OnMouseDown()
    {
        //TODO Sound
        activeIngredient = Instantiate(ingredientPrefab, transform.position, Quaternion.identity).GetComponent<Ingredient>();
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

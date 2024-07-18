using UnityEngine;

public class IngredientHighlighter : MonoBehaviour
{
    public Material highlightMaterial;
    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;
    private GameObject currentObject;

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the mouse position
        Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            // If we hit a new object
            if (hitObject != currentObject)
            {
                // Restore the previous object's material
                RestoreOriginalMaterial();

                if (hitObject.TryGetComponent(out Ingredient ingredientComponent))
                {
                    if (ingredientComponent.IsHeld())
                    {
                        RestoreOriginalMaterial();
                    }
                    else
                    {
                        // Apply the highlight material to the new object
                        currentObject = hitObject;
                        if (currentObject.TryGetComponent(out SpriteRenderer sr))
                        {
                            spriteRenderer = sr;
                            originalMaterial = spriteRenderer.material;
                            spriteRenderer.material = highlightMaterial;
                        }
                    }
                }
            }
        }
        else
        {
            // Restore the material if we're not hitting anything
            if (currentObject != null)
            {
                spriteRenderer.material = originalMaterial;
                currentObject = null;
            }
        }
    }

    private void RestoreOriginalMaterial()
    {
        if (currentObject != null)
        {
            spriteRenderer.material = originalMaterial;
        }
    }
}

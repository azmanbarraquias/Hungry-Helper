using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    public Recipe recipe;

    public TextMeshProUGUI recipeNameTMP;

    public Toggle bookmarkToggle;

    public Image recipeImagePlaceholder;

    public void Start()
    {
        SetupInfomation();
    }

    private void SetupInfomation()
    {
        recipeNameTMP.text = recipe.recipeName;
        bookmarkToggle.isOn = PlayerPrefs.GetInt(recipe.recipeName) == 1 ? true : false;
        recipeImagePlaceholder.sprite = recipe.recipeImage;
    }

    public void ViewRecipeInfo()
    {
        RecipeManager.Instance.ViewRecipeInfo(recipe);
    }

    public void BookmarkRecepie()
    {
        RecipeManager.Instance.SaveBookmarkRecipe(recipe.recipeName, bookmarkToggle.isOn);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoryButton : MonoBehaviour
{
    public RecipeCategory recipeCategory;

    public TextMeshProUGUI categoryTitle;

    public Image categoryImageHolder;

    private void Start()
    {
        categoryTitle.text = recipeCategory.categoryTitle;
        categoryImageHolder.sprite = recipeCategory.categoryImage;
    }

    public void SelectCategory()
    {
        RecipeManager.Instance.SelectCategory(recipeCategory);
    }
}

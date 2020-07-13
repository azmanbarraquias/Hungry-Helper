using UnityEngine;

[System.Serializable]
public class RecipeCategory
{
    public FoodCategory foodCategory;

    public string categoryTitle;

    public Sprite categoryImage;

    public Recipe[] recipes;
}
public enum FoodCategory { Pork, Beef, Chicken, Seafood, Veggie }

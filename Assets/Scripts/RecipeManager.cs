using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.ComponentModel;

public class RecipeManager : MonoBehaviour
{
    [Header("Search Page")]
    public GameObject searchPage;
    public GameObject searchRecipeTemplate;
    public Transform searchHolder;

    [Header("Bookmark Page")]
    public GameObject recipeBookmarkTemplate;
    public Transform bookmarkHolder;

    [Space]

    public GameObject recipeInfoPage;
    public Button recipeInfoBackButton;
    public TMP_InputField searchFieldTMP;

    [Space]

    //public Recipe[] recipes;
    private readonly List<Recipe> recipesList = new List<Recipe>();
    public Transform categoryHolder;

    public GameObject categoryTemplate;

    public AudioSource audioSource;
    public AudioClip audioClip;

    [Space]

    public RecipeCategory[] recipeCategories;

    #region RecipeManager Manager Singleton
    private static RecipeManager instance;
    public static RecipeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RecipeManager>();
            }
            return instance;
        }
    }
    #endregion RecipeManager Manager Singleton

    private void Start()
    {
        InitializeRecipeToList(); 
        RefreshAllList();
    }

    public void InitializeRecipeToList()
    {
        // initialize all recipe
        //foreach (Recipe recipe in recipes)
        //{
        //    recipesList.Add(recipe);
        //}


        foreach (RecipeCategory recipeCategory in recipeCategories)
        {
            foreach (Recipe recipe in recipeCategory.recipes)
            {
                recipesList.Add(recipe);
            }
        }
    }

    public void LoadSearchRecipes()
    {
        foreach (Transform recipe in searchHolder)
        {
            Destroy(recipe.gameObject);
        }

        foreach (Recipe recipe in recipesList)
        {
            CreateRecipesObj(recipe, searchHolder, searchRecipeTemplate);
        }
    }

    public void LoadBookmarkRecipes()
    {
        foreach (Transform bookmarkrecipe in bookmarkHolder)
        {
            Destroy(bookmarkrecipe.gameObject);
        }

        foreach (Recipe recipe in recipesList)
        {
            if (PlayerPrefs.GetInt(recipe.recipeName) == 1 ? true : false) // Load only checked bookmark
            {
                CreateRecipesObj(recipe, bookmarkHolder, recipeBookmarkTemplate);
            }
        }
    }

    public void LoadRecipeCategories()
    {
        foreach (Transform categoryObject in categoryHolder)
        {
            Destroy(categoryObject.gameObject);
        }

        foreach (RecipeCategory recipeCategory in recipeCategories)
        {
            GameObject categoryObj = Instantiate(categoryTemplate);

            categoryObj.GetComponent<CategoryButton>().recipeCategory = recipeCategory;


            categoryObj.transform.SetParent(categoryHolder);

            categoryObj.transform.localScale = new Vector3(1, 1, 1);

        }
    }

    public void CreateRecipesObj(Recipe recipe, Transform parent, GameObject template)
    {
        GameObject recipeObj = Instantiate(template);

        if (recipeObj.GetComponent<RecipeButton>() != null)
        {
            recipeObj.GetComponent<RecipeButton>().recipe = recipe;
        }
        else
        { 
            recipeObj.GetComponent<RecipeButtonBookmark>().recipe = recipe;
        }


        recipeObj.transform.SetParent(parent);

        recipeObj.transform.localScale = new Vector3(1, 1, 1);

    }

    public void SaveBookmarkRecipe(string _recipeName, bool _bookmark)
    {
        PlayerPrefs.SetInt(_recipeName, _bookmark == true ? 1 : 0);

        foreach (var item in recipesList)
        {
            if (item.recipeName == _recipeName)
            {
                item.bookmark = _bookmark;
            }
        }
      
        PlayerPrefs.Save();
    }

    public void ViewRecipeInfo(Recipe _recipeName)
    {
        audioSource.PlayOneShot(audioClip);
        Debug.Log(_recipeName.recipeName);
        recipeInfoPage.GetComponent<RecipeInfo>().recipe = _recipeName;
        recipeInfoPage.gameObject.SetActive(true);
    }

    public void SelectCategory(RecipeCategory foodCategory)
    {
        searchPage.SetActive(true);
        audioSource.PlayOneShot(audioClip);

        searchFieldTMP.text = foodCategory.categoryTitle;

        List<Recipe> _recipeList = new List<Recipe>();
        foreach (RecipeCategory recipeCategory in recipeCategories)
        {
            if (recipeCategory.foodCategory == foodCategory.foodCategory)
            {
                foreach (var recipe in recipeCategory.recipes)
                {
                    _recipeList.Add(recipe);
                }
            }
        }

        foreach (Transform item in searchHolder)
        {
            Destroy(item.gameObject);
        }

        foreach (Recipe recipe in _recipeList)
        {
            CreateRecipesObj(recipe, searchHolder, searchRecipeTemplate);
        }
    }

    public void ResetRecipeInfo()
    {
        recipeInfoBackButton.GetComponent<Button>().onClick.Invoke();
    }

    public void TypingSearchField()
    {
        if (string.IsNullOrWhiteSpace(searchFieldTMP.text))
        {
            RefreshAllList();
        }
    }

    public void SearchRecipes()
    {
        List<Recipe> _searhResult = new List<Recipe>();
        var search = searchFieldTMP.text.ToLower();

        if (!string.IsNullOrWhiteSpace(search))
        {
            foreach (Recipe recipeItem in recipesList)
            {
                if (recipeItem.recipeName.ToLower().Contains(search) || recipeItem.tags.Contains(search))
                {
                    _searhResult.Add(recipeItem);
                }
            }

            foreach (Transform item in searchHolder)
            {
                Destroy(item.gameObject);
            }

            foreach (var recipe in _searhResult)
            {
                CreateRecipesObj(recipe, searchHolder, searchRecipeTemplate);
            }
        }
    }

    public void RefreshAllList()
    {
        LoadSearchRecipes();
        LoadBookmarkRecipes();
        LoadRecipeCategories();
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeInfo : MonoBehaviour
{
    public TextMeshProUGUI recipeNameTMP;

    public Toggle bookmarkToggle;

    public TextMeshProUGUI descriptionTMP;

    public TextMeshProUGUI ingredientTMP;

    public TextMeshProUGUI instructionTMP;

    public Image recipeImagePlaceholder;

    [Space]

    public Recipe recipe;

    public GameObject[] buttons;

    public RecipeTTP[] recipeTTPs;

    public AudioSource audioSource;

    private void OnEnable()
    {
        SetupInfomation();
    }

    private void OnDisable()
    {
        ClearInformation();
    }

    private void SetupInfomation()
    {
        buttons[0].SetActive(true);
        buttons[1].SetActive(false);
        buttons[2].SetActive(false);

        recipeNameTMP.text = recipe.recipeName;
        descriptionTMP.text = recipe.description;
        ingredientTMP.text = recipe.ingredient;
        instructionTMP.text = recipe.instruction;

        bookmarkToggle.isOn = PlayerPrefs.GetInt(recipe.recipeName) == 1 ? true : false;

        recipeImagePlaceholder.sprite = recipe.recipeImage;
    }

    private void ClearInformation()
    {
        audioSource.Stop();

        recipeNameTMP.text = "";
        descriptionTMP.text = "";
        ingredientTMP.text = "";
        instructionTMP.text = "";

        foreach (var recipeTTP in recipeTTPs)
        {
            recipeTTP.audioClip = null;
            recipeTTP.hasRun = false;
        }
    }


    public void SaveBookmarkRecepie()
    {
        RecipeManager.Instance.SaveBookmarkRecipe(recipe.recipeName, bookmarkToggle.isOn);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeSelf == true)
            {
                this.gameObject.SetActive(false);
                RecipeManager.Instance.RefreshAllList();
            }
        }
    }
}

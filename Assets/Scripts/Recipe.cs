using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string recipeName;

    public Sprite recipeImage;

    public bool bookmark;

    [TextArea(7, 7)]
    public string description;

    [TextArea(7, 7)]

    public string ingredient;


    [TextArea(7, 7)]

    public string instruction;

    [Space]
    public string[] tags;
}

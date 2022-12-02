using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "New Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public string ingredientName;
    public string action;
    public SpriteRenderer spriteRender;
    public Sprite sprite;
    public Transform transform;
    public Vector3 pos;

    public void initValues(string rName, string iName, string act)
    {
        this.recipeName = rName;
        this.ingredientName = iName;
        this.action = act;
    }

}

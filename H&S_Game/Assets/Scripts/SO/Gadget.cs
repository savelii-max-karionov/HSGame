using System.Collections.Generic;
using UnityEngine;


public abstract class Gadget : ScriptableObject
{
    public new string name;
    public string id;
    public string description;
    public Sprite icon;
    public bool isCraftable;
    public List<string> recipe;



    public abstract void OnUse(PlayerComponent target);

}

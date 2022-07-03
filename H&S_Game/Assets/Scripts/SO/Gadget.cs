using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Gadget : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;



    public abstract void OnUse(PlayerComponent target);

}

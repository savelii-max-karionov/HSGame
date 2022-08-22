using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemEffect : IEffect
{
    private GameObject producer;
    private GameObject receiver;
    private SkillData data;

    public PlaceItemEffect(SkillData data)
    {

    }

    public void Start(GameObject producer, GameObject receiver = null)
    {
        throw new System.NotImplementedException();
    }
}

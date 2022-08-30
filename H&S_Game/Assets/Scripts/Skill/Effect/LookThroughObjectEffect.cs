using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookThroughObjectEffect : IEffect
{
    private GameObject producer;
    private GameObject receiver;
    private SkillData data;

    public LookThroughObjectEffect(SkillData data)
    {
        //TODO
    }

    public void Start(GameObject producer, GameObject receiver = null)
    {
        throw new System.NotImplementedException();
    }
}

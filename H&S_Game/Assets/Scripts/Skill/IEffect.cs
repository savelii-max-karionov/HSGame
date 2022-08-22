using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect 
{
    public void Start(GameObject producer, GameObject receiver = null);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
    public static Dictionary<string,GameObject> prefabDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        prefabDict.Add("Spectator", Resources.Load<GameObject>("Spectator"));
    }
}

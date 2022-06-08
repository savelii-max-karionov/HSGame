using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private bool isHiding = false;


    public static List<GameObject> playerList=new List<GameObject>();

    public bool IsHiding { get => isHiding; set => isHiding = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnDestroy()
    {
        playerList.Remove(gameObject);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private bool isHiding = false;

    public bool IsHiding { get => isHiding; set => isHiding = value; }

    // Start is called before the first frame update
    protected void Awake()
    {
        GameStatistics.playerList.Add(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GameStatistics.playerList.Remove(gameObject);
    }
}

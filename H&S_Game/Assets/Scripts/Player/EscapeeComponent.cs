using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class EscapeeComponent : PlayerComponent
{

    public InventoryManager inventoryManager;
    public ItemBarUI itemBarUI;

    public Collider2D walkCollider;

    // Start is called before the first frame update
    private void Awake()
    {
        inventoryManager = new InventoryManager(this);
    }
    // Update is called once per frame
    void Update()
    {

    }


}

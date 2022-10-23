using Photon.Pun;
using Photon.Realtime;

public class EscapeeComponent : PlayerComponent
{

    public InventoryManager inventoryManager;
    public ItemBarUI itemBarUI;

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

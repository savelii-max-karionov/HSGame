public class EscapeeComponent : PlayerComponent
{

    public InventoryManager inventoryManager;
    public ItemBarUI itemBarUI;

    // Start is called before the first frame update
    private new void Awake()
    {
        base.Awake();
        inventoryManager = new InventoryManager(this);
    }
    // Update is called once per frame
    void Update()
    {

    }


}

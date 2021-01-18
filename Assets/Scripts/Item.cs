public class Item{
    public enum ItemType {
        Handle, 
        Cheese,
        IOU,
        Key
    };

    readonly ItemType type;

    public Item(ItemType type)
    {
        this.type = type;
    }

    public ItemType Type => type;
}
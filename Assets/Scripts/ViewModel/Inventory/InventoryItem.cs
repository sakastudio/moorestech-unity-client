namespace ViewModel.Inventory
{
    public class InventoryItem
    {
        public readonly int ItemId;
        public readonly int Amount;

        public InventoryItem(int itemId, int amount)
        {
            ItemId = itemId;
            Amount = amount;
        }
    }
}
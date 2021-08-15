using System.Collections.Generic;

namespace ViewModel.Inventory
{
    public interface IInventoryChangeNotify
    {
        List<InventoryItem> GETInventoryItems();
        public void SwapInventorySlot(int from,int to);
        //TODO 残りのメソッドを定義
        //TODO 持ちアイテムとの交換、1アイテムずつ、半分ずつに分ける時のメソッドを追加

    }
}
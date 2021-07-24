using UnityEngine;

namespace UnityCode.UI.View.DragAndDrop
{
    public class SlotUIClickDetector : MonoBehaviour
    {
        public delegate void SlotClick();

        public event SlotClick ONSlotClick;

        public void AddEvent(SlotClick onClick)
        {
            ONSlotClick += onClick;
        }

        public void RemoveEvent(SlotClick onClick)
        {
            ONSlotClick -= onClick;
        }
    }
}

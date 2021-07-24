using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityCode.UI.View.DragAndDrop
{
    public class SlotUIClickDetector : MonoBehaviour,IPointerClickHandler
    {
        public delegate void SlotClick();

        public event SlotClick LeftSlotClick;
        public event SlotClick RightSlotClick;
        public event SlotClick MiddleSlotClick;

        public void AddLeftClickEvent(SlotClick onClick)
        {
            LeftSlotClick += onClick;
        }

        public void RemoveLeftClickEvent(SlotClick onClick)
        {
            LeftSlotClick -= onClick;
        }
        public void AddRightClickEvent(SlotClick onClick)
        {
            RightSlotClick += onClick;
        }

        public void RemoveRightClickEvent(SlotClick onClick)
        {
            RightSlotClick -= onClick;
        }
        public void AddMiddleClickEvent(SlotClick onClick)
        {
            MiddleSlotClick += onClick;
        }

        public void RemoveMiddleClickEvent(SlotClick onClick)
        {
            MiddleSlotClick -= onClick;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                LeftSlotClick?.Invoke();
            else if (eventData.button == PointerEventData.InputButton.Middle)
                RightSlotClick?.Invoke();
            else if (eventData.button == PointerEventData.InputButton.Right)
                MiddleSlotClick?.Invoke();
        }
    }
}

using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityCode.UI.View.DragAndDrop
{
    public class SlotUIClickDetector : MonoBehaviour,IPointerClickHandler
    {
        public delegate void SlotClick();
        public event SlotClick LeftSlotClick;
        public event SlotClick RightSlotClick;
        public event SlotClick MiddleSlotClick;

        const int ExceptionNumber = -1;
        
        public int slotNumber = ExceptionNumber;

        public void Constructor(int number)
        {
            slotNumber = number;
        }
        
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
            if (slotNumber == ExceptionNumber)
            {
                throw new ConstraintException("初期化操作を実行していません。 The initialization operation has not been executed.");
            }
            if (eventData.button == PointerEventData.InputButton.Left)
                LeftSlotClick?.Invoke();
            else if (eventData.button == PointerEventData.InputButton.Middle)
                RightSlotClick?.Invoke();
            else if (eventData.button == PointerEventData.InputButton.Right)
                MiddleSlotClick?.Invoke();
        }
    }
}

using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityCode.UI.View.DragAndDrop
{
    public class SlotUIClickDetector : MonoBehaviour,IPointerClickHandler
    {
        public delegate void SlotClick();
        private event SlotClick LeftSlotClick;
        private event SlotClick RightSlotClick;
        private event SlotClick MiddleSlotClick;

        const int ExceptionNumber = -1;
        
        private int _slotNumber = ExceptionNumber;

        public void Constructor(int number)
        {
            _slotNumber = number;
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
            if (_slotNumber == ExceptionNumber)
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

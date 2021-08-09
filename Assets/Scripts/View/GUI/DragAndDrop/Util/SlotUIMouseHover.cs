using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUI.View.DragAndDrop.Util
{
    public class SlotUIMouseHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private Color mouseOverColor = new Color(0.8f,0.8f,0.8f);
        [SerializeField] private Color originalColor = Color.white;
        private Image _image;
        private void Start()
        {
            _image = gameObject.GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _image.color = mouseOverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _image.color = originalColor;
        }
    }
}

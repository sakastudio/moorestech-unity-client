using UnityEngine;

namespace GUI.View.UIDisplayControl.UIObject
{
    public abstract class UIObjectBase : MonoBehaviour 
    {
        public abstract void ShowUI();
        public abstract void HideUI();
        public abstract bool IsShowUI();
    }
}

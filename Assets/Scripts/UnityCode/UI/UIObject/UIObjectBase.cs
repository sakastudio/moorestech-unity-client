using UnityEngine;

namespace UnityCode.UI.UIObject
{
    public abstract class UIObjectBase : MonoBehaviour 
    {
        public abstract void ShowUI();
        public abstract void HideUI();
        public abstract bool IsShowUI();
    }
}

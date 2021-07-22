using UnityEngine;

namespace UnityCode.UI.UIObject
{
    public class NullUIObject : UIObjectBase
    {
        public override void ShowUI()
        {
        }

        public override void HideUI()
        {
        }

        public override bool IsShowUI()
        {
            return false;
        }
    }
}

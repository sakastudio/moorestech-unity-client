namespace GUI.View.UIDisplayControl.UIObject
{
    public class GeneralUIObject : UIObjectBase
    {

        public override void ShowUI()
        {
            gameObject.SetActive(true);
        }

        public override void HideUI()
        {
            gameObject.SetActive(false);
        }

        public override bool IsShowUI()
        {
            return gameObject.activeSelf;
        }
    }
}

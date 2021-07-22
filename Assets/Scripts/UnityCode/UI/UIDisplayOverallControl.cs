using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityCode.UI
{
    public class UIDisplayOverallControl : MonoBehaviour
    {
         [SerializeField] private List<UIObjectData> uiObjectDataList;
         private UIObjectData _openedUIObject;
        void Update()
        {
            //何かしらUIが開いていたら閉じるかどうかの判定処理
            if (_openedUIObject.UIObject.IsShowUI())
            {
                //開かれているUIのキーが押されたら閉じる
                if (Input.GetKeyDown(_openedUIObject.keyCode))
                {
                    _openedUIObject.UIObject.HideUI();
                }
            }
            //UIが開かれてなければ登録されているUIのどれかのキーが押されるまで待機
            else
            {
                foreach (var uiObjectData in uiObjectDataList)
                {
                    //特定のキーが押されていたらそのuiを開く
                    if (Input.GetKeyDown(uiObjectData.keyCode))
                    {
                        uiObjectData.UIObject.ShowUI();
                        _openedUIObject = uiObjectData;
                        break;
                    }
                }
            }
        }
    }

    [System.Serializable]
    class UIObjectData
    {
        [Header("UIオブジェクト")]
        public IUIObject UIObject;
        [Header("このキーを押したら開く")]
        public KeyCode keyCode;
    }
}

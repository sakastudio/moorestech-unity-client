using System.Collections.Generic;
using UnityCode.UI.View.UIObject;
using UnityEngine;

namespace UnityCode.UI.View
{
    /// <summary>
    /// 全体的なUIのオンオフの制御を行う
    /// </summary>
    public class UIDisplayOverallControl : MonoBehaviour
    {
         [SerializeField] private List<UIObjectData> uiObjectDataList;
         private UIObjectData _openedUIObject = new UIObjectData();
        void Update()
        {
            //何かしらUIが開いていたら閉じるかどうかの判定処理
            if (_openedUIObject.uiObject.IsShowUI())
            {
                //開かれているUIのキーが押されたら閉じる
                if (Input.GetKeyDown(_openedUIObject.keyCode) || Input.GetKeyDown(KeyCode.Escape))
                {
                    _openedUIObject.uiObject.HideUI();
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
                        uiObjectData.uiObject.ShowUI();
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
        public UIObjectBase uiObject;
        [Header("このキーを押したら開く")]
        public KeyCode keyCode;

        public UIObjectData()
        {
            uiObject = new NullUIObject();
        }
    }
}

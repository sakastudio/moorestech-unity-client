using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityCode.UI
{
    public class UIDisplayOverallControl : MonoBehaviour
    {
         [SerializeField] private List<UIObjectData> uiObjectDataList;
        void Update()
        {
        
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

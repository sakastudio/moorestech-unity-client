using System.Collections.Generic;
using UnityEngine;

namespace GUI.View.DragAndDrop
{
    public class DragAndDropUIObjectManager : MonoBehaviour
    {
        [SerializeField] private List<SlotUIClickDetector> _slotUIClickDetectors; 
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < _slotUIClickDetectors.Count; i++)
            {
                _slotUIClickDetectors[i].Constructor(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

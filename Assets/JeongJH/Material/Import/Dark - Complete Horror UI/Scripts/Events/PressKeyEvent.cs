using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Michsky.UI.Dark
{
    public class PressKeyEvent : MonoBehaviour
    {
        // Settings
        public InputAction hotkey;

        // Events
        public UnityEvent onPressEvent;

        void Start()
        {
            hotkey.Enable();

            

        }

        void Update()
        {
            if (hotkey.triggered)
                onPressEvent.Invoke();
        }



        //이벤트 한 번 추가해봄. 
        void PopupOn()
        {
            
        }
    }
}
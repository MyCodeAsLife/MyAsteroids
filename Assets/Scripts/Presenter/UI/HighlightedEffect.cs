using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private EventSystem _eventSystem;
    private Button _button;

    private void Awake()
    {
        _eventSystem = EventSystem.current;
        _button = GetComponent<Button>();
    }

    private void LateUpdate()
    {
        
    }
}

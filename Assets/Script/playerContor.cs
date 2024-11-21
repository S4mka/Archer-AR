using UnityEngine.InputSystem;
using UnityEngine;
using JetBrains.Annotations;

public class playerContor : MonoBehaviour
{
    public bool _ongrab = false;
    public void OnGrab(InputAction.CallbackContext context)
    {
        _ongrab = true;
    }
}

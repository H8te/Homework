using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public event UnityAction Opened;
    public event UnityAction Closed;

    private bool _isOpened;

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out Thief _))
            return;

        _isOpened = !_isOpened;

        if (_isOpened)
            Opened?.Invoke();
        else
            Closed?.Invoke();
    }
}

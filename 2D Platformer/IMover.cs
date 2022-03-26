using UnityEngine.Events;

public interface IMover
{
    public event UnityAction<bool> Jumping;
    public event UnityAction<bool> Running;
    public event UnityAction<bool> Crouching;
}

using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _force;

    private void OnEnable()
    {
        _spawner.Spawned += Shoot;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= Shoot;
    }

    private void Shoot(Nucleus nucleus)
    {
        nucleus.Rigidbody.AddForce(_direction.normalized * _force, ForceMode.Impulse);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    private Rigidbody _rigidBody;
    private Vector3 _direction = Vector3.zero;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!Input.anyKey)
            return;

        if (Input.GetKey(KeyCode.D))
            _direction = new Vector3(1, 0, 0);
        else if (Input.GetKey(KeyCode.A))
            _direction = new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.W))
            _direction = new Vector3(0, 0, 1);
        else if (Input.GetKey(KeyCode.S))
            _direction = new Vector3(0, 0, -1);

        _rigidBody.velocity = _direction * Time.deltaTime * _speed;
    }
}

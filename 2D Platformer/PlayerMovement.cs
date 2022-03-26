using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour, IMover
{
    [SerializeField] private float _runSpeed = 40f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    private State _state;

    public event UnityAction<bool> Jumping;
    public event UnityAction<bool> Running;
    public event UnityAction<bool> Crouching;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.anyKey == false && _state == State.Running)
        {
            StopRunning();
        }

        if (_state == State.Jumping)
            return;

        if (Input.GetKeyDown(KeyCode.S))
            StartCrouching();
        else if (Input.GetKeyUp(KeyCode.S))
            StopCrouching();

        if (Input.GetKey(KeyCode.A))
            Move(-transform.right);
        else if (Input.GetKey(KeyCode.D))
            Move(transform.right);

        if (Input.GetKeyDown(KeyCode.W))
            Jump();
    }

    private void StopRunning()
    {
        Running?.Invoke(false);
        _state = State.Idle;
    }

    private void StartCrouching()
    {
        Crouching?.Invoke(true);
        _state = State.Crouching;
        _runSpeed /= 2f;
    }

    private void StopCrouching()
    {
        Crouching?.Invoke(false);
        _state = State.Idle;
        _runSpeed *= 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jumping?.Invoke(false);
        StopRunning();
    }

    private void Jump()
    {
        Jumping?.Invoke(true);
        _state = State.Jumping;
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void Move(Vector2 direction)
    {
        if (_state != State.Running)
        {
            Running?.Invoke(true);
            _state = State.Running;
        }

        _renderer.flipX = direction.x != 0 ? direction.x < 0 : _renderer.flipX;
        _rigidbody.AddForce(direction.normalized * _runSpeed, ForceMode2D.Force);
    }
}

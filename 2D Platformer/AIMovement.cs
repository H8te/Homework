using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AIMovement : MonoBehaviour, IMover
{
    [SerializeField] private float _speed = 40f;
    [SerializeField] private Transform _path;

    private Transform[] _points;
    private SpriteRenderer _renderer;

    private int _currentPoint;
    
    public event UnityAction<bool> Running;
    public event UnityAction<bool> Jumping;
    public event UnityAction<bool> Crouching;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        var target = DefineTarget();
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        Running?.Invoke(false);
    }

    private Transform DefineTarget()
    {
        Transform target = _points[_currentPoint];

        if (transform.position == target.position)
        {
            _currentPoint++;
            _renderer.flipX = !_renderer.flipX;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;
        }
        return _points[_currentPoint];
    }
}

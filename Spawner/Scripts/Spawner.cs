using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Nucleus _prefab;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _delay = 2;

    public event UnityAction<Nucleus> Spawned;

    private Coroutine _spawning;


    private void Start()
    {
        _spawning = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (true)
        {
            foreach (var point in _points)
            {
                Nucleus spawned = Instantiate(_prefab, point.position, Quaternion.identity);

                Spawned?.Invoke(spawned);

                yield return wait;
            }
        }
    }
}

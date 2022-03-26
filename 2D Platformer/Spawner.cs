using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _delay = 2;
    [SerializeField] private Gem _prefab;

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
                Instantiate(_prefab, point.position, Quaternion.identity);

                yield return wait;
            }
        }
    }
}

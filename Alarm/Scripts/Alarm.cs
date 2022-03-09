using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Door))]
[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    private AudioSource _audio;

    private void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
        _door.Opened += OnOpened;
        _door.Closed += OnClosed;
        _audio.volume = 0;
    }

    private void OnDisable()
    {
        _door.Opened -= OnOpened;
        _door.Closed -= OnClosed;
    }

    private void OnOpened()
    {
        _audio.Play();

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetVolume(1));
    }

    private void OnClosed()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetVolume(0));
    }

    private IEnumerator SetVolume(int target)
    {
        var wait = new WaitForSeconds(0.1f);

        while (_audio.volume != target)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, target, _speed);

            yield return wait;
        }
    }
}

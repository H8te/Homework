using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _filling;
    [SerializeField] private Health _health;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth)
    {
        float value = currentHealth / _health.MaxHealth;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ChangeSmoothly(value));
    }

    private IEnumerator ChangeSmoothly(float target)
    {
        var wait = new WaitForFixedUpdate();

        while (_filling.fillAmount != target)
        {
           _filling.fillAmount = Mathf.MoveTowards(_filling.fillAmount, target, _speed);
            _filling.color = _gradient.Evaluate(_filling.fillAmount);
            yield return wait;
        }
    }
}

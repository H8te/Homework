using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _current;

    public event Action<float> HealthChanged;

    public float MaxHealth { get; private set; } = 100;

    private void Start()
    {
        _current = MaxHealth;
    }

    public void Heal(float heal)
    {
        if (heal < 0)
            throw new ArgumentOutOfRangeException();

        _current = Mathf.Clamp(_current + heal, 0 , MaxHealth);
        HealthChanged?.Invoke(_current);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        _current = Mathf.Clamp(_current - damage, 0, MaxHealth);
        HealthChanged?.Invoke(_current);
    }
}

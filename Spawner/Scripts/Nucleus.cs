using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Nucleus : MonoBehaviour
{
    private Coroutine _destroing;

    public Rigidbody Rigidbody { get; private set; }

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody>();

        _destroing = StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    { 
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}

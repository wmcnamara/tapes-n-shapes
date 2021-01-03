using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxSpawner : MonoBehaviour
{
    private Vector3 defaultPosition;
    public UnityEvent OnDestroy;
    public UnityEvent OnCompletion;

    private void Start() => defaultPosition = transform.position;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Endzone"))
        {
            Destroy();
            return;
        }

        OnCompletion.Invoke();
        Debug.Log("Completed");
        Destroy(this);
    }

    void Destroy()
    {
        OnDestroy.Invoke();
        transform.position = defaultPosition;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Activate()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Button : MonoBehaviour
{
    public UnityEvent OnButtonClick;

    private void OnTriggerEnter(Collider other)
    {
        OnButtonClick.Invoke();
    }
}

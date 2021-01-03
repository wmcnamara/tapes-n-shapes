using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Holder : MonoBehaviour
{
    public UnityEvent OnComplete;
    public Transform holdPosition;
    public Pickup pickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pickup>(out Pickup _pickup))
        {
            if (_pickup.gameObject == pickup.gameObject)
            {
                OnComplete.Invoke();
                _pickup.InUse = true;
                pickup.transform.position = holdPosition.position;
            }
        }
    }
}

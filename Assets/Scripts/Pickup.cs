using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool inUse;
  
    public bool InUse
    {
        set
        {
            inUse = value;

            if (inUse)
            {
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                GetComponent<Collider>().enabled = true;
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        get { return inUse; }
    }

    private Vector3 startPosition; //Used to move back if cube goes out of bounds
    private void Start() => startPosition = transform.position;

    //If the player throws it through a wall/out of the map, itll move back.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OOB"))
        {
            transform.position = startPosition;          
        }
    }
}

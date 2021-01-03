using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public float windSpeed = 0.5f;
    public float interactDistance = 4f;
    public LayerMask interactable;

    Camera mainCam;
    [HideInInspector] public PlayerLook look;

    public Transform pickupPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        look = GetComponentInChildren<PlayerLook>();
        mainCam = Camera.main;
    }

    //Called when the player stops interacting with a tape recorder.
    [HideInInspector] public UnityEvent OnStopInteract;

    public bool holding; //True if the player is holding something.
    [SerializeField] private Pickup currentPickup;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (holding)
            {
                Drop();
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, interactDistance, interactable))
            {
                if (hit.transform.TryGetComponent<Pickup>(out currentPickup) && !holding)
                {
                    currentPickup.InUse = true;
                    return;
                }
            }
        }

        if (currentPickup != null)
        {
            holding = true;
            currentPickup.GetComponent<Rigidbody>().isKinematic = true;
            currentPickup.transform.position = pickupPosition.position;
        }

        if (Input.GetKey(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, interactDistance, interactable))
            {
                if (hit.transform.TryGetComponent<TapeRecorder>(out TapeRecorder recorder))
                {
                    recorder.WindUp();
                    return;
                }
                else
                {
                    OnStopInteract.Invoke();
                    return;
                }
            }
        }
        OnStopInteract.Invoke();     
    }

    void Drop()
    {
        holding = false;
        currentPickup.InUse = false;
        currentPickup.GetComponent<Rigidbody>().isKinematic = false;
        currentPickup = null;
    }
}

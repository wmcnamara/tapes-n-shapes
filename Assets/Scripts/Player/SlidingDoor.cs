using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlidingDoor : MonoBehaviour, ITapeInteractable
{
    public TapeRecorder recorder;
    [Space]
    public bool stayOpen;
    public Vector3 openPosition;
    private Vector3 defaultPosition;

    //Called if stayOpen is true.
    public UnityEvent OnComplete;

    void Awake()
    {
        defaultPosition = transform.position;
        recorder.OnChange += OnChange;
    }

    public void OnChange(float value)
    {
        if (value >= 1 && stayOpen)
        {
            Debug.Log("Completed");
            OnComplete.Invoke();
            recorder.OnChange -= OnChange;
            Destroy(this);
            return;
        }

        transform.position = Vector3.Lerp(defaultPosition, openPosition, value);
    }
}
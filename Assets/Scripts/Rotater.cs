using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rotater : MonoBehaviour
{
    public TapeRecorder recorder;
    public float speed = 35f;

    [Tooltip("Rotation indicating completion of the puzzle")]
    public Transform finishedRotation;

    [Header("Events")]
    public UnityEvent OnComplete;
    public UnityEvent OnRotation;

    Quaternion oldRotation;
    private void Start()
    {
        oldRotation = transform.rotation;
        recorder.onCompletion.AddListener(Rotate);
    }

    public void Rotate() => oldRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, oldRotation, speed * Time.deltaTime);
    }
}

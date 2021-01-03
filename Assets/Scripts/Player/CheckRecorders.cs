using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CheckRecorders : MonoBehaviour
{
    public UnityEvent OnComplete;
    public List<TapeRecorder> recorders;

    private int indexUntilComplete = 0;

    // Update is called once per frame
    void Awake()
    {
        foreach (TapeRecorder recorder in recorders)
        {
            recorder.onCompletion.AddListener(Incremement);
        }
    }

    private void Update()
    {
        if (indexUntilComplete >= recorders.Count)
        {
            Run();
        }
    }

    void Run()
    {
        Debug.Log("CheckRecorder complete");
        OnComplete.Invoke();
        Destroy(this);
    }

    void Incremement() { indexUntilComplete++; }
}

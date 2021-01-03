using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HolderChecker : MonoBehaviour
{
    public UnityEvent OnComplete;

    public Holder holder1;
    public Holder holder2;

    private bool holder1Complete;
    private bool holder2Complete;

    private void Holder1Complete() => holder1Complete = true;
    private void Holder2Complete() => holder2Complete = true;

    void Awake()
    {
        //Subscribe completion functions
        holder1.OnComplete.AddListener(Holder1Complete);
        holder2.OnComplete.AddListener(Holder2Complete);

        //Subscribe completion check functions
        holder1.OnComplete.AddListener(CheckIfComplete);
        holder2.OnComplete.AddListener(CheckIfComplete);
    }

    void CheckIfComplete()
    {
        if (holder1Complete && holder2Complete)
        {
            OnComplete.Invoke();
            Debug.Log("Complete");
        }
    }
}

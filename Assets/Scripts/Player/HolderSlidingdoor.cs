using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HolderSlidingdoor : MonoBehaviour
{
    public UnityEvent OnComplete;
    public Holder holder;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        holder.OnComplete.AddListener(Complete);
    }

    void Complete()
    {
        OnComplete.Invoke();
        anim.SetTrigger("Lift");
    }
}

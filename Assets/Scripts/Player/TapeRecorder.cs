using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TapeRecorder : MonoBehaviour
{
    public bool usable;
    public bool stopOnCompletion;

    public event OnChangedHandlers OnChange;
    public delegate void OnChangedHandlers(float value);

    [HideInInspector] public UnityEvent OnInteract;
    [HideInInspector] public UnityEvent OnStopInteract;

    Player player;
    Animator animator;

    public AudioSource windUp;

    [SerializeField] private float value = 0;
    private bool complete;
    public UnityEvent onCompletion;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();

        player.OnStopInteract.AddListener(WindDown);
        OnStopInteract.AddListener(WindDown);
        OnInteract.AddListener(WindUp);
    }

    void Update()
    {
        if (!complete && stopOnCompletion)
        {
            value = Mathf.Clamp(value, 0, 1);

            if (value == 0)
            {
                StopSound();
                animator.SetTrigger("ImmediateStop");
            }
            else if (value == 1)
            {

                complete = true;
                onCompletion.Invoke();
                StopSound();
                Destroy(windUp);
                animator.SetTrigger("ImmediateStop");
                value = 0;
                return;
            }
            else if (Time.timeScale < 0.1f)
            {
                StopSound();
            }
        }  
        else if (!stopOnCompletion)
        {
            value = Mathf.Clamp(value, 0, 1);

            if (value > 0)
            {
                PlayWindUpSound();

            }
            if (value == 0)
            {
                StopSound();
                animator.SetTrigger("ImmediateStop");
            }
            else if (value == 1)
            {
                Debug.Log("Completed");
                onCompletion.Invoke();
                StopSound();
                animator.SetTrigger("ImmediateStop");
                value = 0;
                return;
            }
            else if (Time.timeScale < 0.1f)
            {
                StopSound();
            }
        }
        else
        {
            animator.SetTrigger("ImmediateStop");
        }
    }

    public void WindDown()
    {
        if (value > 0 && usable && !complete)
        {
            value -= player.windSpeed * Time.deltaTime;

            OnChange?.Invoke(value);

            animator.SetBool("WindDown", true);
            animator.SetBool("WindUp", false);
        }
        if (value > 0 && usable && stopOnCompletion && complete)
        {
            animator.SetTrigger("ImmediateStop");
        }
    }

    public void WindUp()
    {
        if (value < 1 && usable && !complete)
        {
            value += player.windSpeed * Time.deltaTime;

            OnChange?.Invoke(value);

            PlayWindUpSound();
            animator.SetBool("WindUp", true);
            animator.SetBool("WindDown", false);
        }
    }

    void PlayWindUpSound()
    {
        windUp.enabled = true;
    }

    void StopSound ()
    {
        windUp.enabled = false;
    }

    public void Activate() { usable = true; }
    public void Deactivate() { usable = false; }
}
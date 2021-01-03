using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioClip[] steps = null;
    [SerializeField] float stepDelay = 0.5f;
    LoopableInt stepIndex;

    public float playerSpeed = 12f;
    private CharacterController controller;
    private AudioSource source;

    public const float gravity = -9.81f;

    private void Start()
    {
        stepIndex = new LoopableInt(steps.Length - 1);
        canStep = true;
        source = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    float velocity;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (!controller.isGrounded)
        {
            velocity += gravity * Time.deltaTime;
        }
        else
        {
            velocity = 0;
        }

        Vector3 move = (transform.right * x) + (transform.forward * z);
        move.y = velocity;

        controller.Move(move * playerSpeed * Time.deltaTime);
        if (x != 0 || z != 0)
        {
            if (canStep)
            {
                StartCoroutine(Step());
            }
        }
        else
        {
            source.Stop();
        }     
    }

    private bool canStep = true;
    public IEnumerator Step()
    {
        canStep = false;
        source.PlayOneShot(steps[stepIndex.Value]);
        stepIndex.Incremement();
        yield return new WaitForSeconds(stepDelay);
        canStep = true;
    }
}

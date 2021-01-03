using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    public string levelToTransitionTo;
    public bool useTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (useTrigger && other.CompareTag("Player"))
            SceneManager.LoadScene(levelToTransitionTo);
    }

    public void Trigger()
    {
        SceneManager.LoadScene(levelToTransitionTo);
    }
}

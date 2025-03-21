using UnityEngine;

public class Ball : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}

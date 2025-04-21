using UnityEngine;

public class HeadsetSocket : MonoBehaviour
{
    public bool headsetAttached = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on the socket!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Headset") && !headsetAttached)
        {
            headsetAttached = true;

            // Play audio
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            Debug.Log("Headset attached and audio started.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Headset") && headsetAttached)
        {
            headsetAttached = false;

            // Detach headset
            other.transform.SetParent(null);

            // Stop audio
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            Debug.Log("Headset detached and audio stopped.");
        }
    }
}

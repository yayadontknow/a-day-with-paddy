using UnityEngine;
using System.Collections;
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
            StartCoroutine(DelayedHeadsetExit(other));
        }
    }

    private IEnumerator DelayedHeadsetExit(Collider other)
    {
        yield return new WaitForSeconds(1f);

        // Double-check if headset is still outside after 1 second
        if (!Physics.CheckSphere(other.transform.position, 0.1f, LayerMask.GetMask("Headset"))) // Adjust logic/layer as needed
        {
            headsetAttached = false;

            // Detach headset
            other.transform.SetParent(null);

            // Stop audio
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            Debug.Log("Headset detached and audio stopped after delay.");
        }
    }

}

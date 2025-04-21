using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public AudioClip coinSound;       // Drag your sound clip here in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinSound);
            StartCoroutine(DestroyAfterDelay(other.gameObject, 1f)); // wait 2 seconds
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay(GameObject coin, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(coin);
    }
}

using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public GameObject[] sodaCans;         // Assign 5 can prefabs in Inspector
    public Transform spawnPoint;          // Where cans should appear
    public AudioClip dispenseClip;        // Sound when dispensing a can
    public AudioClip errorClip;           // Sound when not enough coins
    public AudioClip correctClip;           // Sound when not enough coins

    private AudioSource audioSource;
    private int coinCount = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on VendingMachine!");
        }
    }

    // Called by another script when a coin is inserted
    public void AddCoin()
    {
        coinCount++;
        Debug.Log("Coin added. Total coins: " + coinCount);
    }

    // Called when a button is pressed
    public void TryDispense(int canIndex)
    {
        if (coinCount <= 0)
        {
            Debug.Log("No coins to dispense!");
            PlaySound(errorClip);
            return;
        }

        if (canIndex < 0 || canIndex >= sodaCans.Length)
        {
            Debug.LogError("Invalid can index");
            PlaySound(errorClip);
            return;
        }

        Instantiate(sodaCans[canIndex], spawnPoint.position, spawnPoint.rotation);
        coinCount--;
        Debug.Log($"Dispensed can {canIndex}. Coins left: {coinCount}");
        PlaySound(correctClip);
        PlaySound(dispenseClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

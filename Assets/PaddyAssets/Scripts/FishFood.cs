using UnityEngine;

public class FishFood : MonoBehaviour
{
    private bool hasTouchedWater = false;

    void OnTriggerEnter(Collider other)
{
    Debug.Log($"Touched: {other.gameObject.name}, Tag: {other.tag}");

    if (!hasTouchedWater && other.CompareTag("Water"))
    {
        Debug.Log("Touch Water");
        hasTouchedWater = true;

        FishSwim[] allFish = FindObjectsOfType<FishSwim>();

        foreach (FishSwim fish in allFish)
        {
            fish.SetTarget(transform);
        }
    }
}

}

using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Assign 3 skyboxes in the Inspector
    private int currentIndex = 0;
    private float timer = 0f;
    private float interval = 100f; // 100 seconds per skybox (5 mins / 3)

    void Start()
    {
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[0];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % skyboxes.Length;
            RenderSettings.skybox = skyboxes[currentIndex];
        }
    }
}

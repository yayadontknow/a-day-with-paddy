using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Assign 3 skyboxes in the Inspector
    public float[] skyboxIntensities = { 1f, 0.5f, 0.3f }; // Match intensity per skybox

    private int currentIndex = 0;
    private float timer = 0f;
    private float interval = 100f; // Switch every 30 seconds

    void Start()
    {
        if (skyboxes.Length > 0 && skyboxIntensities.Length == skyboxes.Length)
        {
            RenderSettings.skybox = skyboxes[0];
            RenderSettings.ambientIntensity = skyboxIntensities[0];
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
            RenderSettings.ambientIntensity = skyboxIntensities[currentIndex];
        }
    }
}

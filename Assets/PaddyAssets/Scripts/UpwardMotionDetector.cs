using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UpwardMotionDetector : MonoBehaviour
{
    public Transform leftHandController;
    public Transform rightHandController;
    public float upwardThreshold = 1.5f;

    public GameObject fireflyPrefab; // Firefly prefab reference
    public float cooldownTime = 0.5f;

    private Vector3 lastPositionRight;
    private Vector3 lastPositionLeft;
    private float lastTriggerTime = 0f;

    void Start()
    {
        lastPositionRight = rightHandController.position;
        lastPositionLeft = leftHandController.position;
    }

    void Update()
    {
        TrackHandMovement(rightHandController, ref lastPositionRight, "Right Hand");
        TrackHandMovement(leftHandController, ref lastPositionLeft, "Left Hand");
    }

    void TrackHandMovement(Transform handController, ref Vector3 lastPosition, string handName)
    {
        if (Time.time - lastTriggerTime < cooldownTime) return;
        if (handController == null) return;

        Vector3 currentPosition = handController.position;
        Vector3 velocity = (currentPosition - lastPosition) / Time.deltaTime;

        if (velocity.y > upwardThreshold)
        {
            Debug.Log($"🎯 {handName} upward motion detected! Velocity Y: {velocity.y:F2}");
            lastTriggerTime = Time.time;

            if (handName == "Right Hand" && fireflyPrefab != null)
            {
                GameObject firefly = Instantiate(fireflyPrefab, handController.position, Quaternion.identity);
                Destroy(firefly, 10f); // Auto destroy after 10 seconds
            }
        }

        lastPosition = currentPosition;
    }
}

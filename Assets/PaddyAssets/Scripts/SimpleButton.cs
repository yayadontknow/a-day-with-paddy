using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleButton : MonoBehaviour
{
    private Vector3 originalPosition;
    private float pressDepth = 0.00016f;

    void Awake()
    {
        originalPosition = transform.localPosition;

        var interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnPress);
    }

    private void OnPress(SelectEnterEventArgs args)
    {
        transform.localPosition = originalPosition - new Vector3(0, pressDepth, 0);
        Invoke(nameof(ResetPosition), 0.1f);
    }

    void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}

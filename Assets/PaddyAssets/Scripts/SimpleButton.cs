using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleButton : MonoBehaviour
{
    public VendingMachine vendingMachine;
    public int canIndex; // 0 to 4 depending on which can this button represents
    private Vector3 originalPosition;
    private float pressDepth = 0.00016f;

    void Awake()
    {
        originalPosition = transform.localPosition;
        var interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnPress);
    }

    private void OnPress(SelectEnterEventArgs args)
    {
        transform.localPosition = originalPosition - new Vector3(0, -pressDepth, 0);
        Invoke(nameof(ResetPosition), 0.1f);

        // Ask vending machine to try dispensing
        if (vendingMachine != null)
        {
            vendingMachine.TryDispense(canIndex);
        }
    }

    void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}

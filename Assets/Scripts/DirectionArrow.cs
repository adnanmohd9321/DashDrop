using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    public DeliveryManager deliveryManager;

    public GameObject redArrow;
    public GameObject greenArrow;

    void Update()
    {
        GameObject target;

        // Arrow switching
        if (deliveryManager.HasPackage())
        {
            greenArrow.SetActive(false);
            redArrow.SetActive(true);

            target = deliveryManager.GetCurrentDeliveryPoint();
        }
        else
        {
            greenArrow.SetActive(true);
            redArrow.SetActive(false);

            target = deliveryManager.pickupPoint;
        }

        // Rotate toward target
        if (target != null)
        {
            Vector3 direction =
                target.transform.position -
                transform.position;

            direction.y = 0;

            if (direction != Vector3.zero)
            {
               transform.forward = direction.normalized;
            }
        }
    }
}
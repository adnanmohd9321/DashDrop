using UnityEngine;

public class PlayerDelivery : MonoBehaviour
{
    public DeliveryManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            if (!manager.HasPackage())
            {
                manager.PickupPackage();
            }
        }

        if (other.CompareTag("Delivery"))
        {
            if (manager.HasPackage())
            {
                manager.DeliverPackage();
            }
        }
    }
}
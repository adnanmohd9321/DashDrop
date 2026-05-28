using UnityEngine;
using TMPro;
using System.Collections;
public class DeliveryManager : MonoBehaviour
{
    public GameObject pickupPoint;
    public GameObject[] deliveryPoints;

    private GameObject currentDeliveryPoint;
    public GameObject deliveryPopup;
    public GameObject moneyPopup;

    private bool hasPackage = false;

    public int money = 0;
    public TMP_Text moneyText;
    public TMP_Text timerText;

    public float deliveryTime = 60f;

    private float currentTime;

    void Start()    {
        pickupPoint.SetActive(true);
        foreach (GameObject point in deliveryPoints)
        {
            point.SetActive(false);
        }
        timerText.gameObject.SetActive(false);
    }
    void Update()
{
    // Timer countdown
    if (hasPackage)
    {
        timerText.gameObject.SetActive(true);
        currentTime -= Time.deltaTime;

        timerText.text =
            "Time: " + Mathf.Ceil(currentTime);

        // Failed delivery
        if (currentTime <= 0)
        {
            hasPackage = false;

            currentDeliveryPoint.SetActive(false);
            pickupPoint.SetActive(true);
            timerText.gameObject.SetActive(false);

            Debug.Log("Delivery Failed");
        }
    }

    // Money UI
    moneyText.text =
        "Money: $" + money;
}

    public void PickupPackage()
    {
        hasPackage = true;

        pickupPoint.SetActive(false);
        int randomIndex =
        Random.Range(0, deliveryPoints.Length);

        currentDeliveryPoint =
        deliveryPoints[randomIndex];

        currentDeliveryPoint.SetActive(true);

        Debug.Log("Package Picked Up");
        currentTime = deliveryTime;
    }

    public void DeliverPackage()
    {
        hasPackage = false;

        currentDeliveryPoint.SetActive(false);
        pickupPoint.SetActive(true);

        money += 100;
    
        Debug.Log("Delivered! Money: " + money);
        StartCoroutine(HideTimer());
        deliveryPopup.SetActive(true);
        moneyPopup.SetActive(true);

        StartCoroutine(HidePopups());
    }

    public bool HasPackage()
    {
        return hasPackage;
    }
    IEnumerator HideTimer()
    {
        yield return new WaitForSeconds(7f);

        timerText.gameObject.SetActive(false);
    }
    public GameObject GetCurrentDeliveryPoint()
    {
        return currentDeliveryPoint;
    }

    IEnumerator HidePopups()
    {
        yield return new WaitForSeconds(2f);

        deliveryPopup.SetActive(false);
        moneyPopup.SetActive(false);
    }
}
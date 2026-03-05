using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class ButtonBuyItem : MonoBehaviour
{

    public string myItemName;
    public float startTime;
    public float totalTime;
    public bool isHovering = false;

    async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    void Update()
    {
        if (isHovering == true)
        {
            totalTime = Time.time - startTime;
        }
    }

    public void PointerEnterItem(string nameInput)
    {
        myItemName = nameInput;
        startTime = Time.time;
        isHovering = true;
        Debug.Log("เมาส์ชี้ที่: " + myItemName);
    }

    public void ClickToBuyItem()
    {
        if (isHovering == true)
        {
            isHovering = false;
            string status = "Buy";

            CustomEvent myEvent = new CustomEvent("BuyList");
            myEvent.Add("NameItem", myItemName);
            myEvent.Add("TimeRange", totalTime);
            myEvent.Add("BuyNotBuy", status);

            AnalyticsService.Instance.RecordEvent(myEvent);
            Debug.Log("กดซื้อ " + myItemName + " ใช้เวลาไป " + totalTime);
        }
    }

    public void PointerExitItem()
    {
        if (isHovering == true)
        {
            isHovering = false;
            string status = "NotBuy";
            CustomEvent myEvent = new CustomEvent("BuyList");
            myEvent.Add("NameItem", myItemName);
            myEvent.Add("TimeRange", totalTime);
            myEvent.Add("BuyNotBuy", status);

            AnalyticsService.Instance.RecordEvent(myEvent);
            Debug.Log("ไม่ซื้อ " + myItemName + " ใช้เวลาไป " + totalTime);
        }
    }
}

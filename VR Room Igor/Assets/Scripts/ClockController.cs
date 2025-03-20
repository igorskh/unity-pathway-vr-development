using System;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [Header("Clock Hands")]
    [SerializeField] private GameObject hourHand;
    [SerializeField] private GameObject minuteHand;
    [SerializeField] private GameObject secondHand;

    [SerializeField] private Vector3 defaultRotation = new(90, 180, 90);

    [Header("Time Settings")]
    [SerializeField] private bool useCurrentTime = true;
    [SerializeField] private Vector3 currentTime = Vector3.zero;
    [SerializeField] private bool isRunning = true;
    [SerializeField] private float clockSpeed = 0.1f;

    private Vector3 lastTime = Vector3.zero;
    private float elapsedTime = 0f;

    private void SetHandRotation(Transform hand, Vector3 rotation)
    {
        hand.rotation = Quaternion.Euler(rotation);
    }

    void SetHandXRotation(Transform hand, float xRotation)
    {
        hand.rotation = Quaternion.Euler(new Vector3(xRotation, defaultRotation.y, defaultRotation.z));
    }

    private void Start()
    {
        SetHandRotation(hourHand.transform, defaultRotation);
        SetHandRotation(minuteHand.transform, defaultRotation);
        SetHandRotation(secondHand.transform, defaultRotation);
    }

    private void SetCurrentTime()
    {
        DateTime dateTime = DateTime.Now;

        currentTime = new Vector3(dateTime.Hour, dateTime.Minute, dateTime.Second);
    }

    private void IterateTime()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= clockSpeed)
        {
            currentTime.z += 1;
            if (currentTime.z >= 60)
            {
                currentTime.y += 1;
            }
            if (currentTime.y >= 60)
            {
                currentTime.x += 1;
            }
            if (currentTime.x >= 12)
            {
                currentTime.x = 0;
            }

            currentTime.z %= 60;
            currentTime.y %= 60;
            currentTime.x %= 12;

            elapsedTime = 0;
        } 
    }

    void Update()
    {
        if (useCurrentTime)
        {
            SetCurrentTime();
        } else if (isRunning)
        {
            IterateTime();
        }

        if (currentTime.x != lastTime.x)
        {
            lastTime.x = currentTime.x;
            float hourAngle = -currentTime.x * 360 / 12;
            SetHandXRotation(hourHand.transform, 90 + hourAngle);
        }
        if (currentTime.y != lastTime.y)
        {
            lastTime.y = currentTime.y;
            float minuteAngle = -currentTime.y * 360 / 60;
            SetHandXRotation(minuteHand.transform, 90 + minuteAngle);
        }
        if (currentTime.z != lastTime.z)
        {
            lastTime.z = currentTime.z;
            float secondAngle = -currentTime.z * 360 / 60;
            SetHandXRotation(secondHand.transform, 90 + secondAngle);
        }
    }
}

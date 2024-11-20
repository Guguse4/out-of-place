using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMimicController : MonoBehaviour
{
    [SerializeField] private GameObject minutes;
    [SerializeField] private float timeBetweenMinutesMovement = 15f;
    private float angleOfMinute = 0f;
    [SerializeField] private float rotationAngle = 6f;

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenMinutesMovement < 0)
        {
            timeBetweenMinutesMovement = 15f;
            var minutesRotation = gameObject.transform.localRotation.eulerAngles; //get the angles;

            angleOfMinute += rotationAngle;

            minutesRotation.Set(0f, 0f, angleOfMinute); //set the angles
            minutes.transform.localRotation = Quaternion.Euler(minutesRotation); //update the transform
        }
        else
        {
            timeBetweenMinutesMovement -= Time.deltaTime;
        }
    }
}

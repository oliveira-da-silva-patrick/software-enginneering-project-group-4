/**
    Script Description

    This script is supposed to be attached to the camera in the SkillTree scene. There are 2 serialized fields that need to be filled.
    This script allows the player to scroll with one finger, and pinch with 2 fingers when using the SkillTree scene

        * zoomOutMin: A minimum zoom value, to let the player not zoom too far outside, should be set to 100.

        * zoomOutMax: A minimum zoom value, to let the player not zoom too far inside, should be set to 600.
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAndPinch : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 100;
    public float zoomOutMax = 600;
    private bool ismultiTouch = false;


    // Update is called once per frame
    void Update()
    {
        // also true if touch is registered on touch device (GetMouseButtonDown)
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // checks if 2 fingers are touching at the same time, for zoom feature
        if (Input.touchCount == 2)
        {
            ismultiTouch = true;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference);

        }
        // if only one finger is touching & the player wants to move around
        else if (Input.GetMouseButton(0) && !ismultiTouch)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // camera is bound to the desired space between -1500/-1000 & 1500/1000
            Camera.main.transform.position = new Vector3(Mathf.Clamp(direction.x + Camera.main.transform.position.x, -1500, 1500), Mathf.Clamp(direction.y + Camera.main.transform.position.y, -1000, 1000), -10);
        }
        // to let the camera not snap back to one finger after letting another go
        if (ismultiTouch && Input.touchCount <= 1)
        {
            ismultiTouch = false;
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
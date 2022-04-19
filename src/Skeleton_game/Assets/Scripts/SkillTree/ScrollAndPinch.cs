using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAndPinch : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 15;
    private bool ismultiTouch = false;


    // Update is called once per frame
    void Update()
    {
        // also true if touch is registered on touch device
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
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
        else if (Input.GetMouseButton(0) && !ismultiTouch)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position = new Vector3(Mathf.Clamp(direction.x + Camera.main.transform.position.x, -1500, 1500), Mathf.Clamp(direction.y + Camera.main.transform.position.y, -1000, 1000), -10);
        }
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIsDragging : MonoBehaviour {
        private bool isDragging = false;
    private Vector3 dragOrigin;

    public float cameraSpeed = 5f;

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // переміщення за допомогою дотику на смартфоні
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    isDragging = true;
                    dragOrigin = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 currentPos = touch.position;
                    Vector3 diff = dragOrigin - currentPos;
                    transform.position += new Vector3(diff.x, 0, diff.y) * cameraSpeed * Time.deltaTime;
                    dragOrigin = currentPos;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                }
            }
        }
        else
        {
            // переміщення за допомогою миші на комп'ютері
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                dragOrigin = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
            if (isDragging)
            {
                Vector3 currentPos = Input.mousePosition;
                Vector3 diff = dragOrigin - currentPos;
                transform.position += new Vector3(diff.x, 0, diff.y) * cameraSpeed * Time.deltaTime;
                dragOrigin = currentPos;
            }

            // переміщення за допомогою клавіш на комп'ютері
            // float horizontal = Input.GetAxis("Horizontal");
            // float vertical = Input.GetAxis("Vertical");
            // transform.position += new Vector3(horizontal, 0, vertical) * cameraSpeed * Time.deltaTime;
        }
    }
}

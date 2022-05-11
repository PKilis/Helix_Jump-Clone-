using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector2 lastTapPos;
    [SerializeField]
    private int controlSensivity = 10;

    void Update()
    {
        #region Computer controller
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPos = Input.mousePosition;
            if (lastTapPos == Vector2.zero)
            {
                lastTapPos = currentTapPos;
            }

            float xPos = lastTapPos.x - currentTapPos.x;
            lastTapPos = currentTapPos;

            transform.Rotate(Vector3.up * xPos * controlSensivity * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
        #endregion
    }
}

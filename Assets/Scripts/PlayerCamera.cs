using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5;

    public Vector3 offset;

    public float camPositionClamp = 3;

    public float moveOffsetValue;
    bool facingRight;

    void Start()
    {
        transform.position = target.position + offset;

        facingRight = true;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        Vector3 cameraOffset = new Vector3(offset.x + moveOffsetValue, offset.y, offset.z);

        Vector3 targetCamPos = target.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);



        if (move > 0 && !facingRight)
        {
            moveOffsetValue *= -1;
            facingRight = true;
        }
        else if (move < 0 && facingRight)
        {
            moveOffsetValue *= -1;
            facingRight = false;
        }

    }
}

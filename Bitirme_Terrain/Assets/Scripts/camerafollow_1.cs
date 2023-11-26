using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow_1 : MonoBehaviour
{
    public bool ballIsMoving = false;
    public GameObject ball;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ballIsMoving)
        {
            transform.position = ball.transform.position + offset;
        }
        
    }
    public void setBallIsMoving(bool isMoving)
    {
        ballIsMoving = isMoving;
    }
}

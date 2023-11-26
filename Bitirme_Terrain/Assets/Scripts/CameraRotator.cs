using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
       
    }
}

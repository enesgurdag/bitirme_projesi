using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingArea : MonoBehaviour
{

    public Vector3 SpawnPoint;
    // Start is called before the first frame update



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoundObject"))
        {
            other.gameObject.transform.position = SpawnPoint;
            other.attachedRigidbody.velocity = Vector3.zero;
            other.attachedRigidbody.angularVelocity = Vector3.zero;
        }
    }


    public Vector3 lastPosition;

    [SerializeField]
    private Rigidbody _target;



    void Update()
    {

        var currentPosition = _target.transform.position;
        if (currentPosition != lastPosition)
        {
            lastPosition = currentPosition;
        }
        if (_target.velocity == Vector3.zero)
        {
            SpawnPoint = lastPosition;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundArea : MonoBehaviour
{
    public Vector3 SpawnPoint; //the position where the ball will spawned 
    

    private void OnTriggerExit(Collider other)
    {
        //check for is the game object's tag is BoundObject or not
        if (other.CompareTag("BoundObject")) 
        {
            StartCoroutine(ExampleCoroutine(other));
            

        }
    }

    IEnumerator ExampleCoroutine(Collider other)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2); //give the delay to the game

        //After we have waited 2 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        
        //if the ball spawned , makes all vector velocities as zero
        other.gameObject.transform.position = SpawnPoint; 
        other.attachedRigidbody.velocity = Vector3.zero;
        other.attachedRigidbody.angularVelocity = Vector3.zero;
    }

    public Vector3 lastPosition; //the last position for ball

    [SerializeField]
    private Rigidbody _target; //the ball in game

    

    void Update()
    {
        //get the current position of the ball
        var currentPosition = _target.transform.position;

        //check for is the last position of the ball is equal to the current position or not
        if (currentPosition != lastPosition) 
        {
            //save the ball's last position as current position
            lastPosition = currentPosition;  
        }
        if (_target.velocity == Vector3.zero && _target.angularVelocity == Vector3.zero)
        {
            //if the velocity of ball equals , make equal last position to spawn position
            SpawnPoint = lastPosition; 
        }

    }
}

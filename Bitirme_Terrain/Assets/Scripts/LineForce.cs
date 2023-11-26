using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineForce : MonoBehaviour
{
    [SerializeField] private float stopVelocity = .05f;
    [SerializeField] private float shotPower;
    [SerializeField] private LineRenderer lineRenderer;

    private bool isIdle;
    private bool isAiming;

    public int shoots = 0;
    public Texture2D shootsIconTexture;
    [SerializeField]
    public bool isShooted = false;
    public bool isInitial = true;
    public bool isBallMoving = false;




    GameObject cameraObject = null;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false;
        cameraObject = GameObject.Find("mainCamera");
    }


    private void FixedUpdate()
    {
        if ((isInitial == true || isBallMoving == true) && rigidbody.velocity.magnitude < stopVelocity)
        {
            Stop();
            Debug.Log("Stop is called : ");
            isInitial = false;
            isBallMoving = false;
        }

        ProcessAim();

        if (isShooted == true)
        {
            shoots = shoots + 1;
            isShooted = false;
        }
    }

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }


    Vector3? worldPoint;
    public bool isOnBall = false;
    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
           // Debug.Log("Fare sorunu : ");
            return;
        }

        worldPoint = CastMouseClickRay();
        if (!worldPoint.HasValue)
        {
            return;
        }
        DrawLine(worldPoint.Value);

        isOnBall = true;

        
    }
    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && isOnBall==true)
        {
            Shoot(worldPoint.Value);
            isOnBall = false;
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        rigidbody.AddForce(direction * strength * shotPower);
        isIdle = false;

        if (cameraObject != null)
        {
            cameraObject.SendMessage("setBallIsMoving", true);
        }

        isShooted = true;
        isBallMoving = true;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions =
        {
            transform.position,
            worldPoint
        };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        isIdle = true;
        if (cameraObject!=null)
        {
            cameraObject.SendMessage("setBallIsMoving", false);
        }
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
         Input.mousePosition.x,
         Input.mousePosition.y,
         Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
        Input.mousePosition.x,
        Input.mousePosition.y,
        Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;

        //Debug.Log("far: " + worldMousePosFar.y);
        //Debug.Log("near: " + worldMousePosNear.y);

        //worldMousePosFar.y = worldMousePosNear.y;

        if (Physics.Raycast(worldMousePosNear,worldMousePosFar-worldMousePosNear,out hit,float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
       
    }


    void OnGUI()
    {
        DisplayShootsCount();

    }

    void DisplayShootsCount()
    {
        Rect shootIconRect = new Rect(10, 10, 32, 32);
        GUI.DrawTexture(shootIconRect, shootsIconTexture);

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        Rect labelRect = new Rect(shootIconRect.xMax, shootIconRect.y, 60, 32);
        GUI.Label(labelRect, shoots.ToString(), style);
    }
}

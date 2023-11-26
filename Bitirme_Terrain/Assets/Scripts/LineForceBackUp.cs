using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LineForceBackUp : MonoBehaviour
{
    [SerializeField] private float stopVelocity = .05f;
    [SerializeField] private float shotPower;
    [SerializeField] private LineRenderer lineRenderer;

    private bool isIdle;
    private bool isAiming;

    public int totalShootNumber;
    public int totalShoots;
    public int shoots = 0;
    public Texture2D shootsIconTexture;
    [SerializeField]
    public bool isShooted = false;
    public bool isInitial = true;
    public bool isBallMoving = false;
    private string levelName;

    private bool isShowHint = true;
    public Text hintText;


    GameObject hintTextObj;

    GameObject cameraObject = null;

    private Rigidbody rigidbody; //rigidbody = our game ball

    private void Awake()
    {

        rigidbody = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false;
        cameraObject = GameObject.Find("mainCamera");

         hintTextObj = GameObject.Find("textForHint"); 
        if (hintTextObj != null) //check for is there a hint text in level
        {
            hintText = hintTextObj.GetComponent<Text>();
        }
        
        
        if (!PlayerPrefs.HasKey("TotalShootNumber")) //check for is there a saved total shoot number
        {
            PlayerPrefs.SetInt("TotalShootNumber", 0); // if there is saved total shoot number , save it as 0
        }
        else
        {
            totalShootNumber = PlayerPrefs.GetInt("TotalShootNumber") + PlayerPrefs.GetInt("ShootsNumber"); // makes new total shoot number = old total shoot number + shoots number for active level 
            PlayerPrefs.SetInt("TotalShootNumber", totalShootNumber); //save new total shoot number
        }
        levelName = "ShootsNumber" + SceneManager.GetActiveScene().buildIndex.ToString(); //find the active level index for level shoot number key
        PlayerPrefs.SetInt(levelName, 0); //save the default shoot number for level opening
    }

    static float previousMagnitude = 0.1f;
    static float testingInterval = 0.1f;

    private void FixedUpdate()
    {
        if ((Time.realtimeSinceStartup - shootingTime) > testingInterval)
        {
            if ((isInitial == true || isBallMoving == true) && rigidbody.velocity.magnitude < stopVelocity) //check for can the ball get shooted or not
            {
                Stop();
                Debug.Log("Stop is called : ");
                isInitial = false;
                isBallMoving = false;
                previousMagnitude = 0.1f;
            }
        }
        
        
        ProcessAim();

        if (isShooted == true)
        {
            shoots = shoots + 1; //if ball shooted , the showing shoots number on the screen will be incremented
            isShooted = false;
            totalShoots = PlayerPrefs.GetInt(levelName) + 1; 
            PlayerPrefs.SetInt(levelName, totalShoots); //save the active level total shoots number
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
    private void ProcessAim() //if the player can aim , the process will be started.
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
        DrawLine(worldPoint.Value); //draw the line which is coming from inside the ball

        isOnBall = true;


    }
    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && isOnBall == true) //check for is the mouse on ball and is the left click of mouse released
        {
            Shoot(worldPoint.Value);
            isOnBall = false;     
        }
    }

    private float shootingTime;
    private void Shoot(Vector3 worldPoint)
    {
        shootingTime = Time.realtimeSinceStartup;
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z); //get the horizontal position vector in 3 dimensions as new vector

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized; //transform the new position's direction
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint); //calculate the strength for drew line according to the length

        rigidbody.AddForce(direction * strength * shotPower); //calculate and add the force to the ball
        isIdle = false;

        if (cameraObject != null)
        {
            cameraObject.SendMessage("setBallIsMoving", true);
        }

        isShooted = true;
        isBallMoving = true;
        isOnBall = false;
        if (hintTextObj != null)
        {
            hintText.gameObject.SetActive(false);
        }
       // Debug.Log("Is ball Moving :" + isBallMoving);

    }

    private void DrawLine(Vector3 worldPoint) //the function for drawing the line to the ball
    {
        Vector3[] positions =
        {
            transform.position,
            worldPoint
        };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void Stop() //the function for what will happen if the ball stopped
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        isIdle = true;
        if (cameraObject != null)
        {
            cameraObject.SendMessage("setBallIsMoving", false);
        }
    }

    private Vector3? CastMouseClickRay() //get the positions of mouse for 3 dimension
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

        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
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
        
        Rect shootIconRect = new Rect(10, 10, 32, 32); // the rectangle sizes for golf ball 2d icon
        GUI.DrawTexture(shootIconRect, shootsIconTexture);

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        Rect labelRect = new Rect(shootIconRect.xMax, shootIconRect.y, 60, 32); //the rectangle sizes for shoot number
        GUI.Label(labelRect, shoots.ToString(), style);

    }
}

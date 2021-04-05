using UnityEngine;
using System.Collections;

public class ViewDraw : MonoBehaviour
{
    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 camera_position = Vector3.zero;

    [SerializeField]
    float maxXPosition = 20.0f; //  right border
    [SerializeField]
    float minXPosition = -20.0f; // left border
    [SerializeField]
    float minYPosition = -15.0f; // top border
    [SerializeField]
    float maxYPosition = 15.0f; //  down border

    Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.03f;

    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // Move camera
        if (Input.GetMouseButtonDown(0))
        {
            hit_position = Input.mousePosition;
            camera_position = transform.position;

        }
        if (Input.GetMouseButton(0))
        {
            current_position = Input.mousePosition;
            LeftMouseDrag();
        }
        // Zoom camera
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                mainCamera.orthographicSize += zoomModifier;
            if (touchesPrevPosDifference < touchesCurPosDifference)
                mainCamera.orthographicSize -= zoomModifier;

        }

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 5f, 8f);
    }

    void LeftMouseDrag()
    {
        // From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
        // with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
        current_position.z = hit_position.z = camera_position.y;

        // Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
        // anyways.  
        Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);

        // Invert direction to that terrain appears to move with the mouse.
        direction = direction * -1;

        Vector3 position = camera_position + direction;
        // set limit camera move the edge of map
        float size = mainCamera.orthographicSize;
        position.x = Mathf.Clamp(position.x, (minXPosition * (5 / size)), (maxXPosition * (5 / size)));
        position.y = Mathf.Clamp(position.y, (minYPosition * (5 / size)), (maxYPosition * (5 / size)));
        // Debug.Log(position);
        transform.position = position;
    }
}
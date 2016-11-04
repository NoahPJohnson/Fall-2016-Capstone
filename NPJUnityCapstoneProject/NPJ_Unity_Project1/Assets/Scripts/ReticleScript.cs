using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tobii.EyeTracking;

public class ReticleScript : MonoBehaviour
{
    [SerializeField] Transform inputObject;
    CameraRotationScript cameraRotationScript;
    [SerializeField] Transform gazeObject;
    [SerializeField] RectTransform minPanel;
    [SerializeField] RectTransform maxPanel;
    [SerializeField] bool usingEyeTracking = true;
    [SerializeField] Vector3 mousePositionVector;
    Ray mouseRay;
    [SerializeField] Vector3 differenceVector;
    [SerializeField] float horizontalRotationBuffer = 100;
    [SerializeField] float verticalRotationBuffer = 50;
    [SerializeField] float sensitivity = 6f;
    [SerializeField] int gazeSize = 6;
    int last;
    GazePoint[] gazePointGroup;
    GazePoint lastGazePoint = GazePoint.Invalid;

    // Use this for initialization
    void Start ()
    {
        if (inputObject != null)
        {
            cameraRotationScript = inputObject.GetComponent<CameraRotationScript>();
        }
        last = gazeSize - 1;
        SetUpPoints();
        EyeTracking.Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (usingEyeTracking == true)
        {
            GazePoint gazePoint = EyeTracking.GetGazePoint();

            if (gazePoint.SequentialId > lastGazePoint.SequentialId && gazePoint.IsWithinScreenBounds)
            {
                UpdateGazePointCloud(gazePoint);
                lastGazePoint = gazePoint;
            }
        }
        else
        {
            mousePositionVector = Input.mousePosition;
        }
        if (transform.position != Input.mousePosition)
        {
            differenceVector = mousePositionVector - transform.position;
            transform.Translate(differenceVector * sensitivity * Time.deltaTime);
            //transform.position = mousePositionVector;
        }
        if (minPanel != null && maxPanel != null)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPanel.position.x, maxPanel.position.x), Mathf.Clamp(transform.position.y, minPanel.position.y, maxPanel.position.y), transform.position.z);
            mouseRay = Camera.main.ScreenPointToRay(mousePositionVector);
            cameraRotationScript.SetTargetRotation(mouseRay.direction);
        }
    }

    public void SetUpPoints()
    {
        gazePointGroup = new GazePoint[gazeSize];
        for (int i = 0; i < gazeSize; i++)
        {
            gazePointGroup[i] = GazePoint.Invalid;
        }
    }

    private int Next()
    {
        return ((last + 1) % gazeSize);
    }

    private void UpdateGazePointCloud(GazePoint gazePoint)
    {
        last = Next();
        gazePointGroup[last] = gazePoint;
        Vector3 averageGazePosition = new Vector3(0, 0, 0);
        for (int i = 0; i < gazeSize; i ++)
        {
            averageGazePosition += (Vector3)gazePointGroup[i].Screen;
        }
        mousePositionVector = averageGazePosition / gazeSize;
    }

    public Vector3 GetMousePositionVector()
    {
        return mousePositionVector;
    }
}

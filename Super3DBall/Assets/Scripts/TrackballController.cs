using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackballController : MonoBehaviour
{
    //References
    public Transform ballTransform;
    public Camera guiCamera;
    private BallController ball;

    //Variables
    private bool tochingTrackball = false;
    private bool canAddForce = false;
    private Vector3 previousTouchPosition;
    private Vector3 currentTouchPosition;

    // Start is called before the first frame update
    void Start()
    {
        ball = ballTransform.GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();

        if (canAddForce)
            UpdateTrackballForce();

        CopyRotation(ballTransform.rotation);
    }

    void CheckInputs()
    {
        if (tochingTrackball == false && Input.GetMouseButton(0) && IsTochingTrackball())
            OnStartTochingTrackball();
        else if (tochingTrackball && (Input.GetMouseButtonUp(0) || IsTochingTrackball() == false))
            OnStopTochingTrackball();

        if(canAddForce)
        {
            previousTouchPosition = currentTouchPosition;
            currentTouchPosition = Input.mousePosition;
        }
    }

    void UpdateTrackballForce()
    {
        if (tochingTrackball == false)
            canAddForce = false;

        Vector3 trackBallRadiusScreenPosition = guiCamera.WorldToScreenPoint(transform.position + (Vector3.forward * (transform.localScale.x / 2.0f)));
        Vector3 dragVector = currentTouchPosition - previousTouchPosition;
        //float dragForce = (dragVector.magnitude / trackBallRadiusScreenPosition.magnitude) / Time.deltaTime;
        dragVector = new Vector3(dragVector.x, 0f, dragVector.y);
        float dragMagnitude = dragVector.magnitude;

        if(dragMagnitude <= 0)
            ball.Brake(0.99f);
        else
            ball.GiveForce(dragVector);
    }

    void CopyRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    void OnStartTochingTrackball()
    {
        tochingTrackball = true;
        canAddForce = true;
        currentTouchPosition = Input.mousePosition;
    }

    void OnStopTochingTrackball()
    {
        tochingTrackball = false;
    }

    bool IsTochingTrackball()
    {
        Vector3 trackBallScreenPosition = guiCamera.WorldToScreenPoint(transform.position);
        Vector3 trackBallRadiusScreenPosition = guiCamera.WorldToScreenPoint(transform.position + (Vector3.forward * (transform.localScale.x / 2.0f)));

        float touchRadius = (trackBallRadiusScreenPosition - trackBallScreenPosition).sqrMagnitude;
        float mouseMagnitude = (Input.mousePosition - trackBallScreenPosition).sqrMagnitude;

        if (mouseMagnitude <= touchRadius)
            return true;

        return false;
    }
}

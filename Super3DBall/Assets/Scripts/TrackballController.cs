using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackballController : MonoBehaviour
{
    //References
    public Transform ballTransform;
    public Camera guiCamera;
    private BallController ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = ballTransform.GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        CopyRotation(ballTransform.rotation);
    }

    void CheckInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 trackBallScreenPosition = guiCamera.WorldToScreenPoint(transform.position);
            Vector3 trackBallRadiusScreenPosition = guiCamera.WorldToScreenPoint(transform.position + (Vector3.forward * (transform.localScale.x / 2.0f)));

            float touchRadius = (trackBallRadiusScreenPosition - trackBallScreenPosition).sqrMagnitude;
            float mouseMagnitude = (Input.mousePosition - trackBallScreenPosition).sqrMagnitude;

            if (mouseMagnitude <= touchRadius)
                Debug.Log("Touch Detected");
        }
    }

    void CopyRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}

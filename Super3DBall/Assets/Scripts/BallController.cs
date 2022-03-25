using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //Components
    private Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GiveForce(Vector3 force)
    {
        rBody.AddForce(force);
    }

    public void Brake(float brakePercentage)
    {
        rBody.velocity = rBody.velocity * brakePercentage;
    }
}

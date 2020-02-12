using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentScript : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 curVel;
    public float MaxVelocity;
    public float MaxForce;

    // Start is called before the first frame update
    void Start()
    {
        curVel = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        magClamp();
        transform.position += velocity * Time.deltaTime;
    }

    void addForce(Vector3 addVal)
    {
        velocity += addVal;
    }

    void magClamp()
    {
        if (velocity.magnitude > MaxVelocity)
        {
            velocity = velocity.normalized;
            velocity.x *= MaxVelocity;
            velocity.y *= MaxVelocity;
            velocity.z *= MaxVelocity;
        }
    }

    Vector3 getForce()
    {
        return curVel;
    }


    void execute()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicFlee : MonoBehaviour
{
    // The object that this agent is seeking out
    public GameObject target;

    // The speed that the agent will move at (given in units per second)
    public float speed;
    public float maxVelocity;
    public float rotSpeed;

    Vector3 force;
    Vector3 v;
    Vector3 curVel;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

        curVel = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        v = (transform.position - target.transform.position).normalized * speed;
        force = v - curVel;
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        if (velocity != Vector3.zero)
        {
            Quaternion goalRotation = Quaternion.LookRotation(velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, rotSpeed * Time.deltaTime);
        }
        transform.position += transform.forward * velocity.magnitude * Time.deltaTime;

        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
    }
    public Vector3 velocityVoid()
    {
        return velocity;
    }

}

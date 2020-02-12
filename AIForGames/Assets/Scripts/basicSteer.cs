using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSteer : MonoBehaviour
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
    Vector3 steering;


    void Update()
    {
        // TODO for Students
        // Update the position of this agent such that it is closer to its target
        curVel = transform.forward;
        Seek();
    }


    void Seek()
    {
        v = (target.transform.position - transform.position).normalized * speed;
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

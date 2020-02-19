using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEvade : MonoBehaviour
{
    // The object that this agent is seeking out
    public GameObject target;

    // The speed that the agent will move at (given in units per second)
    public float speed;
    public float maxVelocity;
    public float rotSpeed;
    public float timer = 2f;

    Vector3 force;
    Vector3 v;
    Vector3 curVel;
    Vector3 velocity;
    Vector3 steering;
    Vector3 prevTransform;
    Vector3 enemVelocity;

    private void Start()
    {
        prevTransform = target.transform.position;
    }

    void Update()
    {
        if (timer < 1)
        {
        enemVelocity = (target.transform.position - prevTransform) / Time.deltaTime;
        timer++;
        }
    // TODO for Students
    // Update the position of this agent such that it is closer to its target
    Seek();
    timer -= Time.deltaTime;
    prevTransform = target.transform.position;        
    }

    void Seek()
    {
        v = (transform.position - target.transform.position + enemVelocity).normalized * speed;
        force = v - velocity;
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        if (velocity != Vector3.zero)
        {
            Quaternion goalRotation = Quaternion.LookRotation(velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, rotSpeed * Time.deltaTime);
        }
        transform.position += transform.forward * velocity.magnitude * Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward * 10000, Color.green);
    }
}

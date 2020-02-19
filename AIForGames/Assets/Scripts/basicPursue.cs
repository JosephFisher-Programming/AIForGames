using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicPursue : MonoBehaviour
{
    // The object that this agent is seeking out
    public GameObject target;
   // private basicFlee enemFlee;

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
        //enemFlee = target.GetComponent<basicFlee>();
    }

    void Update()
    {
        enemVelocity = (target.transform.position-prevTransform) / Time.deltaTime;
        // TODO for Students
        // Update the position of this agent such that it is closer to its target
        //curVel = transform.forward;
        Seek();
        timer -= Time.deltaTime;
        prevTransform = target.transform.position;
    }


    void Seek()
    {
        

        v = (target.transform.position + enemVelocity - transform.position).normalized * speed;
        force = v - velocity;
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        if (velocity != Vector3.zero)
        {
            Quaternion goalRotation = Quaternion.LookRotation(velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, rotSpeed * Time.deltaTime);
            //transform.forward = velocity.normalized;
        }
        transform.position +=  transform.forward * velocity.magnitude * Time.deltaTime;

        //Debug.DrawRay(transform.position, velocity + target.transform.position, Color.blue);
        Debug.DrawRay(transform.position, transform.forward * 10000, Color.green);
    }
}

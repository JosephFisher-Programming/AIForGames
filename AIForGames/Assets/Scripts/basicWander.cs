using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicWander : MonoBehaviour
{
    /*public float speed;
    public float maxVelocity;
    private float timer = 1;

    Vector3 force;
    Vector3 v;
    public Vector3 curVel;
    Vector3 velocity;
    Vector3 steering;
    public Vector3 target;

    // Update is called once per frame

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            target = new Vector3(transform.forward.x + Random.Range(-.1f, .1f), transform.forward.y + Random.Range(-.1f, .1f), transform.forward.z + Random.Range(-.1f, .1f));
            timer++;
        }
        curVel = transform.forward;
        Seek();
    }

    void Seek()
    {
        v = (target - transform.position).normalized * speed;
        force = v - curVel;
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        transform.position += velocity * Time.deltaTime;
        if (velocity != Vector3.zero)
        {
            transform.forward = velocity;
        }

        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
    }*/

    public float radius;
    public float speed;
    public float maxVelocity;
    public float timer;
    public float rotSpeed;

    Vector3 circleCenter;
    Vector3 wanderDistance;
    Vector3 displace;
    Vector3 targetVec;
    Vector3 randVec;
    Vector3 force;
    Vector3 v;
    Vector3 curVel;
    Vector3 velocity;

    private void Start()
    {
        circleCenter = transform.forward;
    }

    private void OnEnable()
    {
        GetNewVec();
    }

    private void Update()
    {
        curVel = transform.forward;
        timer -= Time.deltaTime;
        if (timer < 0 || Vector3.Distance(targetVec, transform.position) < .2f)
        {
            GetNewVec();
            timer = 10f;
        }
        Seek();
    }

    void Seek()
    {
        v = (targetVec - transform.position).normalized * speed;
        force = v - curVel;
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        if (velocity != Vector3.zero)
        {
            Quaternion goalRotation = Quaternion.LookRotation(velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, rotSpeed * Time.deltaTime);
        }
        
        transform.position += transform.forward * velocity.magnitude * Time.deltaTime;
    }

    void GetNewVec()
    {
        circleCenter = transform.forward;
        circleCenter *= radius;
        randVec = Random.insideUnitSphere * (radius - 2);
        targetVec = circleCenter + randVec;
    }
}

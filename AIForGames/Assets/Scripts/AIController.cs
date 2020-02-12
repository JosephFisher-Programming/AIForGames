using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private basicFlee flee;
    private basicSteer seek;
    private basicWander wander;
    public GameObject seekTarget;
    public GameObject fleeTarget;
    public float timer = 0f;
    public float seekRangeCheck;
    public float fleeRangeCheck;

    Vector3 seekRange = new Vector3(1, 1, 1);
    Vector3 fleeRange = new Vector3(2, 2, 2);

    public float seekingRange;
    public float fleeingRange;

    private void Start()
    {
        flee = gameObject.GetComponent<basicFlee>();
        seek = gameObject.GetComponent<basicSteer>();
        wander = gameObject.GetComponent<basicWander>();
    }

    // Update is called once per frame
    void Update()
    {
        seekRangeCheck = Vector3.Distance(transform.position, seekTarget.transform.position);
        fleeRangeCheck = Vector3.Distance(transform.position, fleeTarget.transform.position);
        timer -= Time.deltaTime;
        Vector3 distanceToSeek = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 distanceToFlee = new Vector3(fleeTarget.transform.position.x, fleeTarget.transform.position.y, fleeTarget.transform.position.z);
        distanceToSeek -= seekTarget.transform.position;
        distanceToFlee -= transform.position;

        if (fleeRangeCheck < fleeingRange && timer <= 0)
        {
            flee.enabled = true;
            wander.enabled = false;
            seek.enabled = false;
            timer = 1f;
        }

        // (distanceToSeek.x > seekRange.x || distanceToSeek.y > seekRange.y || distanceToSeek.z > seekRange.z)

        else if (seekRangeCheck > seekingRange && timer <= 0)
        {
            flee.enabled = false;
            wander.enabled = false;
            seek.enabled = true;
            timer = 1f;
        }
        //(distanceToFlee.x < fleeRange.x || distanceToFlee.y < fleeRange.y || distanceToFlee.z < fleeRange.z)
        else if (timer <= 0f)
        {
            flee.enabled = false;
            wander.enabled = true;
            seek.enabled = false;
            timer = 2f;
        }
    }
}

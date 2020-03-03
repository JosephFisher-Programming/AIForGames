using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineTreeRunner : MonoBehaviour
{
    public moveTargetDecision mineTarget;
    public moveTargetDecision baseTarget;
    public actionTaken act;
    public GameObject[] targets;
    public int goldCount = 0;
    public int goldCap = 500;
    int goldSave = 500;

    void Start()
    {
        mineTarget = new moveTargetDecision(targets[0]);
        baseTarget = new moveTargetDecision(targets[1]);
        act = new actionTaken();
    }

    private void Update()
    {
        if (goldCount < goldCap)
        {
            if (Vector3.Distance(transform.position, mineTarget.target.transform.position) == 0f)
            {
                Debug.Log("TargetChange");
                goldCount++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, mineTarget.target.transform.position, .05f);
                gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }
        else
        {
            if(goldCount == goldCap)
            {
                goldCap = 0;
            }
            
            if (Vector3.Distance(transform.position, baseTarget.target.transform.position) == 0f)
            {
                Debug.Log("TargetChange");
                goldCount--;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, baseTarget.target.transform.position, .05f);
                gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            if(goldCount < goldCap)
            {
                goldCap = goldSave;
            }
        }
    }
}

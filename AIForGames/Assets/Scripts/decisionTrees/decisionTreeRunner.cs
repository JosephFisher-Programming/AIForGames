using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decisionTreeRunner : MonoBehaviour
{
    public setTargetDecision findNewTarget;
    public GameObject[] targets;
    int curTar = 0;

    void Start()
    {
        findNewTarget = new setTargetDecision(curTar, targets);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, findNewTarget.target.transform.position, .05f);
        if(Vector3.Distance(transform.position, findNewTarget.target.transform.position) == 0f)
        {
            findNewTarget.MakeDecision();
            Debug.Log("TargetChange");
        }
    }
}

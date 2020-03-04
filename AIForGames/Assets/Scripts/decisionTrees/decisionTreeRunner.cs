using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decisionTreeRunner : MonoBehaviour
{
    public CheckWayPoint findNewTarget;
    IDecision curTarget;
    public GameObject[] targets;
    public int curTar = 0;

    void Start()
    {
        findNewTarget = new CheckWayPoint(this,
            new GetNewWaypoint(this),
            new MoveTowardsWaypoint(this));
    }

    private void Update()
    {
        curTarget = findNewTarget;
        while(curTarget.MakeDecision() != null)
        {
            curTarget = curTarget.MakeDecision();
        }
    }
}

public class CheckWayPoint : IDecision
{
    decisionTreeRunner guard;
    IDecision yes;
    IDecision no;

    public CheckWayPoint()
    {
        guard = null;
        yes = null;
        no = null;
    }

    public CheckWayPoint(decisionTreeRunner guard, IDecision yes, IDecision no)
    {
        this.guard = guard;
        this.yes = yes;
        this.no = no;
    }

    public IDecision MakeDecision()
    {
        float distance = Vector3.Distance(guard.transform.position, guard.targets[guard.curTar].transform.position);
        return (distance < .5f ? yes : no);
    }
}

public class GetNewWaypoint : IDecision
{
    decisionTreeRunner guard;

    public GetNewWaypoint()
    {
        guard = null;
    }

    public GetNewWaypoint(decisionTreeRunner guard)
    {
        this.guard = guard;
    }

    public IDecision MakeDecision()
    {
        guard.curTar++;
        if(guard.curTar > 3)
        {
            guard.curTar = 0;
        }

        return null;
    }
}

public class MoveTowardsWaypoint : IDecision
{
    decisionTreeRunner guard;

    public MoveTowardsWaypoint()
    {
        guard = null;
    }

    public MoveTowardsWaypoint(decisionTreeRunner guard)
    {
        this.guard = guard;
    }

    public IDecision MakeDecision()
    {
        guard.transform.position += (guard.targets[guard.curTar].transform.position - guard.transform.position) * .05f;
        return null;
    }
}
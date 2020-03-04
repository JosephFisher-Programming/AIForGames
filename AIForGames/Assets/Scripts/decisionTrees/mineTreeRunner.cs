using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineTreeRunner : MonoBehaviour
{
    public moveTargetDecision mineTarget;
    public moveTargetDecision baseTarget;
    public actionTaken act;
    public GameObject[] targets;
    public int curTar = 0;
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

public class FullOfGod : IDecision
{
    mineTreeRunner miner;
    IDecision yes;
    IDecision no;

    public FullOfGod()
    {
        miner = null;
    }

    public FullOfGod(mineTreeRunner miner)
    {
        this.miner = miner;
    }

    public IDecision MakeDecision()
    {
        miner.curTar++;
        if (miner.curTar > 3)
        {
            miner.curTar = 0;
        }

        return null;
    }
}

public class CheckMinerLocation : IDecision
{
    mineTreeRunner miner;
    IDecision yes;
    IDecision no;

    public CheckMinerLocation()
    {
        miner = null;
        yes = null;
        no = null;
    }

    public CheckMinerLocation(mineTreeRunner miner, IDecision yes, IDecision no)
    {
        this.miner = miner;
        this.yes = yes;
        this.no = no;
    }

    public IDecision MakeDecision()
    {
        float distance = Vector3.Distance(miner.transform.position, miner.targets[miner.curTar].transform.position);
        return (distance < .5f ? yes : no);
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

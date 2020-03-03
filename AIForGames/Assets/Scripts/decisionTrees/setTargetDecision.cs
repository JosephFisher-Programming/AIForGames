using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTargetDecision : IDecision
{
    public GameObject target;
    GameObject[] targetList;
    int curPlace;

    // default constructor
    public setTargetDecision()
    {

    }

    // parameterized constructor allows you to define branch on construction
    public setTargetDecision(int targetNew, GameObject[] targets)
    {
        targetList = targets;
        curPlace = targetNew;
        target = targetList[curPlace];
        
    }

    // evaluate the decision
    public IDecision MakeDecision()
    {
        curPlace++;
        if(curPlace > 3)
        {
            curPlace = 0;
        }
        target = targetList[curPlace];

        return null;
    }
}

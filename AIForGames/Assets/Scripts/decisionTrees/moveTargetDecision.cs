using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTargetDecision : IDecision
{
    public GameObject target;

    // default constructor
    public moveTargetDecision()
    {

    }

    // parameterized constructor allows you to define branch on construction
    public moveTargetDecision(GameObject targeted)
    {
        target = targeted;
    }

    // evaluate the decision
    public IDecision MakeDecision()
    {
        //target = targetList[curPlace];
        Debug.Log("Moving Toward " + target.name);

        return null;
    }
}

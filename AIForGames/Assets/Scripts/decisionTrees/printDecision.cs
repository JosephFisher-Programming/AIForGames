using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printDecision : IDecision
{
    public bool branch = false;

    // default constructor
    public printDecision()
    {

    }

    // parameterized constructor allows you to define branch on construction
    public printDecision(bool branch)
    {
        this.branch = branch;
    }

    // evaluate the decision
    public IDecision MakeDecision()
    {
        Debug.Log(branch ? "Yes" : "No");

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionTaken : IDecision
{
    Color[] actColors;
    public Color curColor;
    public int colorCount;


    // default constructor
    public actionTaken()
    {
        actColors[0] = Color.white;
        actColors[1] = Color.yellow;
        actColors[2] = Color.black;
    }

    public void changeColor(int color)
    {
        colorCount = color;
    }

    // evaluate the decision
    public IDecision MakeDecision()
    {
        curColor = actColors[colorCount];

        return null;
    }
}

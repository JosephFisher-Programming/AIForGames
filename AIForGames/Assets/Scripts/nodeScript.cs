using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeScript : MonoBehaviour
{
    public float moveCost;
    public int IDNum = 0;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    private void Start()
    {
        /*up = null;
        down = null;
        right = null;
        left = null;*/
    }

    private void LateUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 1.1f, Color.green);
        Debug.DrawRay(transform.position, -transform.forward * 1.1f, Color.blue);
        Debug.DrawRay(transform.position, transform.right * 1.1f, Color.red);
        Debug.DrawRay(transform.position, -transform.right * 1.1f, Color.yellow);
    }

    public int compareAdjacent()
    {
        GameObject bestOpt=null; // = new GameObject();

        if(up != null && right != null)
        {
            if (up.GetComponent<nodeScript>().moveCost > right.GetComponent<nodeScript>().moveCost)
            {
                bestOpt = right;
            }
        }

        if (right != null && left != null)
        {
            if (right.GetComponent<nodeScript>().moveCost > left.GetComponent<nodeScript>().moveCost)
            {
                bestOpt = left;
            }
        }

        if (left != null && down != null)
        {
            if (left.GetComponent<nodeScript>().moveCost > down.GetComponent<nodeScript>().moveCost)
            {
                bestOpt = down;
            }
        }

        if (down != null && up != null)
        {
            if (down.GetComponent<nodeScript>().moveCost > up.GetComponent<nodeScript>().moveCost)
            {
                bestOpt = up;
            }
        }
        return bestOpt.GetComponent<nodeScript>().IDNum;
    }
}

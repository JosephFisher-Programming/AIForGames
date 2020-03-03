using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nodeScriptAStar : MonoBehaviour
{
    public float moveCost;
    public float distance = Mathf.Infinity;
    public float heuristicScore;
    public float fScore;
    public int IDNum = 0;
    public GameObject up = null;
    public GameObject down = null;
    public GameObject left = null;
    public GameObject right = null;
    public GameObject prevNode = null;
    public Text IDDisplay;
    public bool isTraversed = false;

    private void Start()
    {
        /*up = null;
        down = null;
        right = null;
        left = null;*/  
        IDDisplay.transform.position += new Vector3(transform.position.x, transform.position.z) * 15;
    }

    private void LateUpdate()
    {
        if (isTraversed == true)
        {
            IDDisplay.color = Color.red;
            IDDisplay.text = Mathf.Round(fScore).ToString();
        }
        Debug.DrawRay(transform.position, transform.forward * 1.1f, Color.green);
        Debug.DrawRay(transform.position, -transform.forward * 1.1f, Color.blue);
        Debug.DrawRay(transform.position, transform.right * 1.1f, Color.red);
        Debug.DrawRay(transform.position, -transform.right * 1.1f, Color.yellow);
    }

    public void compareAdjacent(List<GameObject> nodeList)
    {
        float lowestVal = Mathf.Infinity;
        GameObject corVal = null;
        float[] fScores = new float[] { Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity };
        if (up)
        {
            fScores[0] = up.GetComponent<nodeScriptAStar>().fScore;
        }
        if (down)
        {
            fScores[1] = down.GetComponent<nodeScriptAStar>().fScore;
        }
        if (left)
        {
            fScores[3] = left.GetComponent<nodeScriptAStar>().fScore;
        }
        if (right)
        {
            fScores[2] = right.GetComponent<nodeScriptAStar>().fScore;
        }
        for (int i = 0; i < 4; i++)
        {
            if (lowestVal > fScores[i])
            {
                lowestVal = fScores[i];
                switch (i)
                {
                    case 0:
                        if (up.GetComponent<nodeScriptAStar>().isTraversed)
                        {
                            break;
                        }
                        corVal = up;
                        break;
                    case 1:
                        if (down.GetComponent<nodeScriptAStar>().isTraversed)
                        {
                            break;
                        }
                        corVal = down;
                        break;
                    case 2:
                        if (right.GetComponent<nodeScriptAStar>().isTraversed)
                        {
                            break;
                        }
                        corVal = right;
                        break;
                    case 3:
                        if (left.GetComponent<nodeScriptAStar>().isTraversed)
                        {
                            break;
                        }
                        corVal = left;
                        break;
                }
            }
        }
        nodeList.Add(corVal);
    }

    public void AddToList(List<GameObject> nodeList)
    {
        if (up == true && !up.GetComponent<nodeScriptAStar>().isTraversed && up.GetComponent<nodeScriptAStar>().distance > this.distance + 1)
        {
            nodeList.Add(up);
            up.GetComponent<nodeScriptAStar>().distance = this.distance + 1;
            up.GetComponent<nodeScriptAStar>().prevNode = this.gameObject;
            up.GetComponent<nodeScriptAStar>().fScore = up.GetComponent<nodeScriptAStar>().distance + up.GetComponent<nodeScriptAStar>().heuristicScore;
        }
        if (down == true && !down.GetComponent<nodeScriptAStar>().isTraversed && down.GetComponent<nodeScriptAStar>().distance > this.distance + 1)
        {
            nodeList.Add(down);
            down.GetComponent<nodeScriptAStar>().distance = this.distance + 1;
            down.GetComponent<nodeScriptAStar>().prevNode = this.gameObject;
            down.GetComponent<nodeScriptAStar>().fScore = down.GetComponent<nodeScriptAStar>().distance + down.GetComponent<nodeScriptAStar>().heuristicScore;
        }
        if (right == true && !right.GetComponent<nodeScriptAStar>().isTraversed && right.GetComponent<nodeScriptAStar>().distance > this.distance + 1)
        {
            nodeList.Add(right);
            right.GetComponent<nodeScriptAStar>().distance = this.distance + 1;
            right.GetComponent<nodeScriptAStar>().prevNode = this.gameObject;
            right.GetComponent<nodeScriptAStar>().fScore = right.GetComponent<nodeScriptAStar>().distance + right.GetComponent<nodeScriptAStar>().heuristicScore;
        }
        if (left == true && !left.GetComponent<nodeScriptAStar>().isTraversed && left.GetComponent<nodeScriptAStar>().distance > this.distance + 1)
        {
            nodeList.Add(left);
            left.GetComponent<nodeScriptAStar>().distance = this.distance + 1;
            left.GetComponent<nodeScriptAStar>().prevNode = this.gameObject;
            left.GetComponent<nodeScriptAStar>().fScore = left.GetComponent<nodeScriptAStar>().distance + left.GetComponent<nodeScriptAStar>().heuristicScore;
        }
        fScore = distance + heuristicScore;
    }
}

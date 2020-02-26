using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class nodeScript : MonoBehaviour
{
    public float moveCost;
    public float distance = Mathf.Infinity;
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
        IDDisplay.text = IDNum.ToString();
        IDDisplay.transform.position += new Vector3(transform.position.x, transform.position.z) * 15;
    }

    private void LateUpdate()
    {
        if(isTraversed == true)
        {
            IDDisplay.text = distance.ToString() ;
        }
        Debug.DrawRay(transform.position, transform.forward * 1.1f, Color.green);
        Debug.DrawRay(transform.position, -transform.forward * 1.1f, Color.blue);
        Debug.DrawRay(transform.position, transform.right * 1.1f, Color.red);
        Debug.DrawRay(transform.position, -transform.right * 1.1f, Color.yellow);
    }

    public int compareAdjacent()
    {
        float lowestVal = Mathf.Infinity; // = new GameObject();
        GameObject corVal = null;
        float[] moveCosts = new float[]{Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity};
        moveCosts[0] = up.GetComponent<nodeScript>().moveCost;
        moveCosts[1] = down.GetComponent<nodeScript>().moveCost;
        moveCosts[2] = right.GetComponent<nodeScript>().moveCost;
        moveCosts[3] = left.GetComponent<nodeScript>().moveCost;

        for(int i = 0; i < 4; i++)
        {
            if(lowestVal > moveCosts[i])
            {
                lowestVal = moveCosts[i];
                switch (i)
                {
                    case 0:
                        if (up.GetComponent<nodeScript>().isTraversed)
                        {
                            break;
                        }
                        corVal = up;
                        break;
                    case 1:
                        if (down.GetComponent<nodeScript>().isTraversed)
                        {
                            break;
                        }
                        corVal = down;
                        break;
                    case 2:
                        if (right.GetComponent<nodeScript>().isTraversed)
                        {
                            break;
                        }
                        corVal = right;
                        break;
                    case 3:
                        if (left.GetComponent<nodeScript>().isTraversed)
                        {
                            break;
                        }
                        corVal = left;
                        break;
                }
            }
        }
        return corVal.GetComponent<nodeScript>().IDNum;
    }

    public void AddToList(List<GameObject> nodeList)
    {
        if(up == true && !up.GetComponent<nodeScript>().isTraversed && up.GetComponent<nodeScript>().distance > this.distance + 1)
        {
            nodeList.Add(up);
            up.GetComponent<nodeScript>().distance = this.distance + 1;
            up.GetComponent<nodeScript>().prevNode = this.gameObject;
        }
        if (down == true && !down.GetComponent<nodeScript>().isTraversed && down.GetComponent<nodeScript>().distance > this.distance + 1)
        {
            nodeList.Add(down);
            down.GetComponent<nodeScript>().distance = this.distance + 1;
            down.GetComponent<nodeScript>().prevNode = this.gameObject;
        }
        if (right == true && !right.GetComponent<nodeScript>().isTraversed && right.GetComponent<nodeScript>().distance > this.distance + 1)
        {
            nodeList.Add(right);
            right.GetComponent<nodeScript>().distance = this.distance + 1;
            right.GetComponent<nodeScript>().prevNode = this.gameObject;
        }
        if (left == true && !left.GetComponent<nodeScript>().isTraversed && left.GetComponent<nodeScript>().distance > this.distance + 1)
        {
            nodeList.Add(left);
            left.GetComponent<nodeScript>().distance = this.distance + 1;
            left.GetComponent<nodeScript>().prevNode = this.gameObject;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMap : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject targetNode;
    
    public int numOfTiles;
    public List<GameObject> nodeList;
    public List<GameObject> visitedNodeList;
    public GameObject curNode;
    int nodeCount = 0;
    int visitCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        calcPath();
        targetNode = nodeList[nodeList.Count - 1];
        curNode = findLowCost(nodeList);
    }

    private void Update()
    {
        //while(nodeList.Count > 0)
       // {
            int spfID = curNode.GetComponent<nodeScript>().compareAdjacent();
            visitedNodeList.Capacity = nodeList.Count;
            visitedNodeList[visitCount] = curNode;
            nodeList.Remove(nodeList[spfID]);
           // break;
       // }
    }

    void calcPath()
    {
        for (int i = 0; i < numOfTiles; i++)
        {
            for (int j = 0; j < numOfTiles; j++)
            {
                nodeList.Add(Instantiate(tilePrefab, new Vector3(j * 1.1f, 0f, i * 1.1f), Quaternion.identity));
                nodeList[nodeCount].GetComponent<nodeScript>().moveCost = Mathf.Infinity;
                nodeList[nodeCount].GetComponent<nodeScript>().IDNum = nodeCount;
                nodeCount++;
            }
        }
        nodeList[0].GetComponent<nodeScript>().moveCost = 0;
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (i + numOfTiles < nodeList.Count)
            {
                nodeList[i].GetComponent<nodeScript>().up = nodeList[i + numOfTiles];
            }

            if (i - numOfTiles > -1)
            {
                nodeList[i].GetComponent<nodeScript>().down = nodeList[i - numOfTiles];
            }

            if ((i + 1) % numOfTiles != 0)
            {
                nodeList[i].GetComponent<nodeScript>().right = nodeList[i + 1];
            }

            if (i % numOfTiles != 0)
            {
                nodeList[i].GetComponent<nodeScript>().left = nodeList[i - 1];
            }
        }
    }

    GameObject findLowCost(List<GameObject> nodeListed)
    {
        GameObject lowVal = nodeListed[nodeListed.Count-1];
        int lowValInd = 0;

        for(int i = 0; i < nodeListed.Count; i++)
        {
            if (lowVal.GetComponent<nodeScript>().moveCost > nodeListed[i].GetComponent<nodeScript>().moveCost)
            {
                lowVal = nodeListed[i];
                lowValInd = i;
                Debug.Log("Its breaking");
            }
        }
        return lowVal;
    }
}

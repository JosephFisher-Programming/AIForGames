using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMap : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject targetNode;
    
    public int numOfTiles;
    public List<GameObject> nodeList;
    public List<GameObject> unvisitedNodeList;
    public List<GameObject> visitedNodeList;
    public List<GameObject> targetPath;
    public GameObject curNode;
    public GameObject AIPathWalker;
    int nodeCount = 0;
    int stepCounter = 0;
    bool pathCalc = false;
    // Start is called before the first frame update
    void Start()
    {
        createNodes();
        targetNode = nodeList[nodeList.Count - 1];
        curNode = findLowCost(nodeList);
        curNode.GetComponent<nodeScript>().isTraversed = true;
        curNode.GetComponent<nodeScript>().AddToList(unvisitedNodeList);
        unvisitedNodeList.Remove(curNode);
    }

    private void Update()
    {
        while (!targetNode.GetComponent<nodeScript>().isTraversed && unvisitedNodeList.Count > 0)
        {
            curNode.GetComponent<nodeScript>().AddToList(unvisitedNodeList);
            curNode.GetComponent<nodeScript>().isTraversed = true;
            visitedNodeList.Add(curNode);
            unvisitedNodeList.Remove(curNode);
            curNode = unvisitedNodeList[0];
        }
        if (!pathCalc)
        {
            calcPath(targetNode, targetPath);
            pathCalc = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AIPathWalker.transform.position = targetPath[stepCounter].transform.position;
            stepCounter++;
            Mathf.Clamp(stepCounter,0, targetPath.Count - 1);
        }

    }

    void createNodes()
    {
        for (int i = 0; i < numOfTiles; i++)
        {
            for (int j = 0; j < numOfTiles; j++)
            {
                nodeList.Add(Instantiate(tilePrefab, new Vector3(j * 1.1f, 0f, i * 1.1f), Quaternion.identity));
                nodeList[nodeCount].GetComponent<nodeScript>().moveCost = 1;
                nodeList[nodeCount].GetComponent<nodeScript>().distance = Mathf.Infinity;
                nodeList[nodeCount].GetComponent<nodeScript>().IDNum = nodeCount;
                nodeCount++;
            }
        }
        nodeList[0].GetComponent<nodeScript>().moveCost = 0;
        nodeList[0].GetComponent<nodeScript>().distance = 0;
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

    void calcPath(GameObject target, List<GameObject> targetPath)
    {
        GameObject pathfinder = target;
        targetPath.Add(pathfinder);
        while (pathfinder.GetComponent<nodeScript>().prevNode == true)
        {
            pathfinder = pathfinder.GetComponent<nodeScript>().prevNode;
            targetPath.Add(pathfinder);
            if (Input.GetKeyDown(KeyCode.E))
            {
                break;
            }
        }
        targetPath.Reverse();
    }
}

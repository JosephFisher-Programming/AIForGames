using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMapAStar : MonoBehaviour
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
        curNode = findLowCost(nodeList);
        curNode.GetComponent<nodeScriptAStar>().isTraversed = true;
        curNode.GetComponent<nodeScriptAStar>().AddToList(unvisitedNodeList);
        unvisitedNodeList.Remove(curNode);
        sortByFScore(unvisitedNodeList);
    }

    private void Update()
    {
        while (!targetNode.GetComponent<nodeScriptAStar>().isTraversed && unvisitedNodeList.Count > 0)
        {
            
            curNode.GetComponent<nodeScriptAStar>().AddToList(unvisitedNodeList);
            curNode.GetComponent<nodeScriptAStar>().isTraversed = true;
            sortByFScore(unvisitedNodeList);
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
            Mathf.Clamp(stepCounter, 0, targetPath.Count - 1);
        }

    }

    void createNodes()
    {
        for (int i = 0; i < numOfTiles; i++)
        {
            for (int j = 0; j < numOfTiles; j++)
            {
                nodeList.Add(Instantiate(tilePrefab, new Vector3(j * 1.1f, 0f, i * 1.1f), Quaternion.identity));
                nodeList[nodeCount].GetComponent<nodeScriptAStar>().moveCost = 1;
                nodeList[nodeCount].GetComponent<nodeScriptAStar>().distance = Mathf.Infinity;
                nodeList[nodeCount].GetComponent<nodeScriptAStar>().IDNum = nodeCount;
                nodeCount++;
            }
        }

        targetNode = nodeList[nodeList.Count - 1];
        nodeList[0].GetComponent<nodeScriptAStar>().moveCost = 0;
        nodeList[0].GetComponent<nodeScriptAStar>().distance = 0;
        for (int i = 0; i < nodeList.Count; i++)
        {
            nodeList[i].GetComponent<nodeScriptAStar>().heuristicScore = Vector3.Distance(nodeList[i].transform.position, targetNode.transform.position);
            if (i + numOfTiles < nodeList.Count)
            {
                nodeList[i].GetComponent<nodeScriptAStar>().up = nodeList[i + numOfTiles];
            }

            if (i - numOfTiles > -1)
            {
                nodeList[i].GetComponent<nodeScriptAStar>().down = nodeList[i - numOfTiles];
            }

            if ((i + 1) % numOfTiles != 0)
            {
                nodeList[i].GetComponent<nodeScriptAStar>().right = nodeList[i + 1];
            }

            if (i % numOfTiles != 0)
            {
                nodeList[i].GetComponent<nodeScriptAStar>().left = nodeList[i - 1];
            }
        }
    }

    GameObject findLowCost(List<GameObject> nodeListed)
    {
        GameObject lowVal = nodeListed[nodeListed.Count - 1];
        int lowValInd = 0;

        for (int i = 0; i < nodeListed.Count; i++)
        {
            if (lowVal.GetComponent<nodeScriptAStar>().moveCost > nodeListed[i].GetComponent<nodeScriptAStar>().moveCost)
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
        while (pathfinder.GetComponent<nodeScriptAStar>().prevNode == true)
        {
            pathfinder = pathfinder.GetComponent<nodeScriptAStar>().prevNode;
            targetPath.Add(pathfinder);
            if (Input.GetKeyDown(KeyCode.E))
            {
                break;
            }
        }
        targetPath.Reverse();
    }

    void sortByFScore(List<GameObject> nodeList)
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < nodeList.Count-1; i++)
            {
                if (nodeList[i].GetComponent<nodeScriptAStar>().fScore > nodeList[i + 1].GetComponent<nodeScriptAStar>().fScore)
                {
                    GameObject temp = nodeList[i];
                    nodeList[i] = nodeList[i + 1];
                    nodeList[i + 1] = temp;
                }
            }
        }
    }
}

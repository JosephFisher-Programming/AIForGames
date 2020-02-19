using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMap : MonoBehaviour
{
    public GameObject tilePrefab;
    public int numOfTiles;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numOfTiles; i++)
        {
            for (int j = 0; j < numOfTiles; j++)
            {
                Instantiate(tilePrefab, new Vector3(i * 1.1f, 0f,j *1.1f), Quaternion.identity);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z);
        }
    }
}

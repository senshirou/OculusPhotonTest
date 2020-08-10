using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGetL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent = GameObject.Find("LeftHandAnchor").transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float groundSpeed = -5;
    void Update()
    {
        transform.position += new Vector3(0, 0, -9) * Time.deltaTime;
    }
    
}

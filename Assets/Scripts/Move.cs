using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 0, -5) * Time.deltaTime;
    }
    
}

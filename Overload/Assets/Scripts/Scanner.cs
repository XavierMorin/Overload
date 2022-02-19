using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scanner : MonoBehaviour
{
    // Start is called before the first frame update
    public float interval = 0.2f;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if(timer > interval)
        {
            AstarPath.active.Scan();
            timer = 0;
        }
        
    }
}

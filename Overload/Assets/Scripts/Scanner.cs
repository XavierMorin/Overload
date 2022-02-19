using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scanner : MonoBehaviour
{
    // Start is called before the first frame update
    public float interval = 0.2f;
    private float timer = 0;
    private AstarData AA;
    private GridGraph gg;

    // Update is called once per frame
    void Start()
    {
        gg = AstarPath.active.data.gridGraph;
    }
    void Update()
    {

        timer += Time.deltaTime;
        if(timer > interval)
        {
            AstarPath.active.Scan();
            timer = 0;
        }
        gg.center = new Vector3(transform.position.x,transform.position.y, transform.position.z);
        
    }
}

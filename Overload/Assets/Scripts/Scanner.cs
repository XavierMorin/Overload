using UnityEngine;
using Pathfinding;

public class Scanner : MonoBehaviour
{
    // Start is called before the first frame update
    public float interval = 0.2f;
    private float timer = 0;
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
       
        
    }

    public void moveUP(float y)
    {
        gg.center = gg.center + new Vector3(0, y,0);
        Quaternion q = Quaternion.Euler(gg.rotation.x, gg.rotation.y, gg.rotation.z);
        gg.RelocateNodes(gg.center, q,gg.nodeSize,gg.aspectRatio,gg.isometricAngle);

    }
}

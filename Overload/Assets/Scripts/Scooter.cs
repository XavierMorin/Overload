using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scooter: MonoBehaviour
{
    // Start is called before the first frame update
    public Waypoint currentWayPoint;
    private AIDestinationSetter AIDS;
    private AIPath aiPath;
    public float reachedDistance = 1f;
  
    void Awake()
    {
        AIDS = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
    }
    void Start()
    {
        AIDS.target = currentWayPoint.GetTransform();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (aiPath.remainingDistance < reachedDistance && Vector3.Distance(AIDS.target.transform.position, transform.position) < reachedDistance)
        {
            
                
            Waypoint nextWayPoint = currentWayPoint.GetNextWaypoint();
          
            if (nextWayPoint == null)
            {
                Destroy(this.gameObject);
                return;
            }
            currentWayPoint = nextWayPoint;


            AIDS.target = currentWayPoint.GetTransform();
           
        }
    }
}

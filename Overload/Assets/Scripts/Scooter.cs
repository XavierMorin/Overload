using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scooter: MonoBehaviour
{
    // Start is called before the first frame update
    public Waypoint currentWayPoint;
    private GameObject currentWaypointGO;
    private AIDestinationSetter AIDS;
    private AIPath aiPath;
    public float reachedDistance = 1f;

    public static int nbScooter = 0;
  
    void Awake()
    {
        nbScooter++;
        AIDS = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
    }
   
    

    public void Start()
    {
        AIDS.target = currentWayPoint.GetTransform();
        currentWaypointGO = currentWayPoint.GetGO();
    }

    // Update is called once per frame
    void Update()
    {

        if(currentWaypointGO.activeInHierarchy == false)
        {
            nbScooter--;
            Destroy(this.gameObject);
        }
        
        if (aiPath.remainingDistance < reachedDistance && Vector3.Distance(AIDS.target.transform.position, transform.position) < reachedDistance)
        {
            
                
            Waypoint nextWayPoint = currentWayPoint.GetNextWaypoint();
          
            if (nextWayPoint == null)
            {
                nbScooter--;
                Destroy(this.gameObject);
                return;
            }
            currentWayPoint = nextWayPoint;
            currentWaypointGO = currentWayPoint.GetGO();


            AIDS.target = currentWayPoint.GetTransform();
           
        }
    }
}

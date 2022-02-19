using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;


    [Range(0f, 5f)]
    public float width = 1f;
    public List<Waypoint> branches;
   


    public Vector3 GetPosition()
    {
        float x = width * Random.Range(-1f, 1f);
        float y = width * Random.Range(-1f, 1f);
        

        return new Vector3(transform.position.x+ x,transform.position.y+y,0);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Waypoint GetNextWaypoint()
    {
        if (branches != null)
        {
            int r =Random.Range(0, branches.Count + 1);
            if (r == branches.Count)
                return nextWaypoint;
            else
                return branches[r];
        }
        else
            return nextWaypoint;
    }



}

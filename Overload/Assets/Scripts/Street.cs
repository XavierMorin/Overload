using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POS { UP,DOWN}
public enum streetType { Intersection,Vertical}
public class Street : MonoBehaviour
{
    // Start is called before the first frame update
    
    public streetType type;

    //UP DOWN LEFT RIGHT
    public Waypoint[] Entries = new Waypoint[4];
    
    public Waypoint[] Exits = new Waypoint[4];

    public Street[] Streets = new Street[2];

    

    float timer = 0;
    public void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            timer = 0;
            SpamScooter();
        }
    }
    private void SpamScooter()
    {
        foreach(Waypoint entry in Entries)
        {
            if (entry == null)
                continue;
            if(entry.previousWaypoint == null && Scooter.nbScooter<20)
               ScooterFactory.createScooter(entry);  
        }
    }
    public bool AddStreet(Street street, POS position)
    {
        if( Streets[(int)position] != null) return false;
        Streets[(int)position] = street;
        Connect(position);
        return true;
    }
    public void RemoveStreet(Street street,POS position)
    {
        Disconnect(position);
        Streets[(int)position] = null;
    }
    private void Connect(POS position)
    {
        switch (position)
        {
            case POS.UP: ConnectUp();break;
            case POS.DOWN: ConnectDown(); break;
          
        }
    }
    public void Disconnect(POS position)
    {
        switch (position)
        {
            case POS.UP: DisconnectUp(); break;
            case POS.DOWN: DisconnectDown(); break;
     
        }
    }


    private void ConnectUp()
    {
        Exits[(int)POS.UP].nextWaypoint = Streets[(int)POS.UP].Entries[(int)POS.DOWN];
        Entries[(int)POS.UP].previousWaypoint = Streets[(int)POS.UP].Exits[(int)POS.DOWN];

        Streets[(int)POS.UP].Entries[(int)POS.DOWN].previousWaypoint = Exits[(int)POS.UP];
        Streets[(int)POS.UP].Exits[(int)POS.DOWN].nextWaypoint = Entries[(int)POS.UP];

    }
    private void ConnectDown()
    {
        Exits[(int)POS.DOWN].nextWaypoint = Streets[(int)POS.DOWN].Entries[(int)POS.UP];
        Entries[(int)POS.DOWN].previousWaypoint = Streets[(int)POS.DOWN].Exits[(int)POS.UP];

        Streets[(int)POS.DOWN].Entries[(int)POS.UP].previousWaypoint = Exits[(int)POS.DOWN];
        Streets[(int)POS.DOWN].Exits[(int)POS.UP].nextWaypoint = Entries[(int)POS.DOWN];
    }
   


    private void DisconnectUp()
    {
        Exits[(int)POS.UP].nextWaypoint = null;
        Entries[(int)POS.UP].previousWaypoint = null;

        if (Streets[(int)POS.UP] != null)
        {
            Streets[(int)POS.UP].Entries[(int)POS.DOWN].previousWaypoint = null;
            Streets[(int)POS.UP].Exits[(int)POS.DOWN].nextWaypoint = null;
        }
    }
    private void DisconnectDown()
    {
        Exits[(int)POS.DOWN].nextWaypoint = null;
        Entries[(int)POS.DOWN].previousWaypoint = null;

        if (Streets[(int)POS.DOWN] != null)
        {
            Streets[(int)POS.DOWN].Entries[(int)POS.UP].previousWaypoint = null;
            Streets[(int)POS.DOWN].Exits[(int)POS.UP].nextWaypoint = null;
        }
    }

    
  

    

}

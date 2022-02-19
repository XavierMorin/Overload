using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POS { UP,DOWN,LEFT,RIGHT}
public enum streetType { Intersection,Horizontal,Vertical}
public class Street : MonoBehaviour
{
    // Start is called before the first frame update
    
    public streetType type;

    //UP DOWN LEFT RIGHT
    public Waypoint[] Entries = new Waypoint[4];
    
    public Waypoint[] Exits = new Waypoint[4];

    public Street[] Streets = new Street[4];


    public void Update()
    {
      
    }
    public void AddStreet(Street street, POS position)
    {
        Streets[(int)position] = street;
        Connect(position);

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
            case POS.LEFT: ConnectLeft(); break;
            case POS.RIGHT: ConnectRight(); break;
        }
    }
    public void Disconnect(POS position)
    {
        switch (position)
        {
            case POS.UP: DisconnectUp(); break;
            case POS.DOWN: DisconnectDown(); break;
            case POS.LEFT: DisconnectLeft(); break;
            case POS.RIGHT: DisconnectRight(); break;
        }
    }








    private void ConnectUp()
    {
        Exits[(int)POS.UP].nextWaypoint = Streets[(int)POS.UP].Entries[(int)POS.DOWN];
        Entries[(int)POS.UP].previousWaypoint = Streets[(int)POS.UP].Exits[(int)POS.DOWN];
    }
    private void ConnectDown()
    {
        Exits[(int)POS.DOWN].nextWaypoint = Streets[(int)POS.DOWN].Entries[(int)POS.UP];
        Entries[(int)POS.DOWN].previousWaypoint = Streets[(int)POS.DOWN].Exits[(int)POS.UP];
    }
    private void ConnectLeft()
    {
        Exits[(int)POS.LEFT].nextWaypoint = Streets[(int)POS.LEFT].Entries[(int)POS.RIGHT];
        Entries[(int)POS.LEFT].previousWaypoint = Streets[(int)POS.LEFT].Exits[(int)POS.RIGHT];
    }
    private void ConnectRight()
    {
        Exits[(int)POS.RIGHT].nextWaypoint = Streets[(int)POS.RIGHT].Entries[(int)POS.LEFT];
        Entries[(int)POS.RIGHT].previousWaypoint = Streets[(int)POS.RIGHT].Exits[(int)POS.LEFT];
    }


    private void DisconnectUp()
    {
        Exits[(int)POS.UP].nextWaypoint = null;
        Entries[(int)POS.UP].previousWaypoint = null;
    }
    private void DisconnectDown()
    {
        Exits[(int)POS.DOWN].nextWaypoint = null;
        Entries[(int)POS.DOWN].previousWaypoint = null;
    }
    private void DisconnectLeft()
    {
        Exits[(int)POS.LEFT].nextWaypoint = null;
        Entries[(int)POS.LEFT].previousWaypoint = null;
    }
    private void DisconnectRight()
    {
        Exits[(int)POS.RIGHT].nextWaypoint = null;
        Entries[(int)POS.RIGHT].previousWaypoint = null;
    }



}

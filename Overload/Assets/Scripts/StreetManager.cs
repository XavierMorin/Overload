
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetManager : MonoBehaviour
{
    public GameObject currentStreetGO;
    public GameObject IntersectionPrefab;
    public GameObject HorizontalStreetPrefab;
    public GameObject VerticalStreetPrefab;
    public GameObject RightUpPrefab;
    public GameObject LeftUpPrefab;
    public List<GameObject> existingStreets = new List<GameObject>();
    public Queue<GameObject> IntersectionPool = new Queue<GameObject>();
    public Queue<GameObject> HorizontalStreetPool = new Queue<GameObject>();
    public Queue<GameObject> VerticalStreetPool = new Queue<GameObject>();
    public Queue<GameObject> RightUpStreetPool = new Queue<GameObject>();
    public Queue<GameObject> LeftUpStreetPool = new Queue<GameObject>();

    public Street playerOnStreet;

    public float timer;

    public float verticalOffset;
    public float horizontalOffset;

    private void Start() {

        timer = 0f;
        Street currentStreet = currentStreetGO.GetComponent<Street>();
        for(int i  = 0; i < 10; i++)
        {
            IntersectionPool.Enqueue(Instantiate(VerticalStreetPrefab));
            HorizontalStreetPool.Enqueue(Instantiate(VerticalStreetPrefab));
            VerticalStreetPool.Enqueue(Instantiate(VerticalStreetPrefab));
            RightUpStreetPool .Enqueue(Instantiate(VerticalStreetPrefab));
            LeftUpStreetPool.Enqueue(Instantiate(VerticalStreetPrefab));
        }
    }

    private void Update() {
        if (timer > 1f )
        {
            foreach(GameObject street in existingStreets) 
            {
                if(Vector3.Distance(transform.position, street.GetComponent<Transform>().position) < 15f)
                {
                    existingStreets.Remove(street);
                    PushStreetToPool(street);
                }
            }
        }
    }


    public void AddVerticalStreet(POS pos)
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();

        if (pos == POS.LEFT || pos == POS.RIGHT)
            return;
        if (currentStreet.type == streetType.Horizontal)
            return;

        GameObject newStreetGO = Instantiate(VerticalStreetPrefab, currentStreetGO.transform.position,new Quaternion(0,0,0,0));
        Street newStreet = newStreetGO.GetComponent<Street>();
        currentStreet.AddStreet(newStreet, pos);

        if (pos == POS.UP)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, verticalOffset, 0); ;
        if (pos == POS.DOWN)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, -verticalOffset, 0);



        currentStreetGO = newStreetGO;

    }

    public void AddIntersectionStreet(POS pos)
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();

        
        if (currentStreet.type == streetType.Horizontal && (pos == POS.UP || pos == POS.DOWN))
            return;

        if (currentStreet.type == streetType.Vertical && (pos == POS.LEFT || pos == POS.RIGHT))
            return;


        GameObject newStreetGO = Instantiate(IntersectionPrefab, currentStreetGO.transform.position, new Quaternion(0, 0, 0, 0));
        Street newStreet = newStreetGO.GetComponent<Street>();
        currentStreet.AddStreet(newStreet, pos); 

        if (pos == POS.UP)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, verticalOffset, 0);
        if (pos == POS.DOWN)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, -verticalOffset, 0);
        if(pos == POS.LEFT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(-horizontalOffset,0, 0);
        if (pos == POS.RIGHT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(horizontalOffset, 0, 0);




        currentStreetGO = newStreetGO;

    }

    public void AddHorizontalStreet(POS pos)
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();

        if (pos == POS.DOWN || pos == POS.UP)
            return;
        if (currentStreet.type == streetType.Vertical)
            return;

        GameObject newStreetGO = Instantiate(HorizontalStreetPrefab, currentStreetGO.transform.position, new Quaternion(0, 0, 0, 0));
        Street newStreet = newStreetGO.GetComponent<Street>();
        currentStreet.AddStreet(newStreet, pos);

        if (pos == POS.RIGHT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(horizontalOffset, 0,0); ;
        if (pos == POS.LEFT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3( -horizontalOffset, 0,0);



        currentStreetGO = newStreetGO;

    }

     public void AddStreet()
    {
        if (playerOnStreet == null)
        {
            return;
        }
        switch(playerOnStreet.type)
        {
            case streetType.Intersection:
                AddVerticalStreetOnRun(POS.UP);
                AddHorizontalStreetOnRun(POS.LEFT);
                AddHorizontalStreetOnRun(POS.RIGHT);
            break;

            case streetType.Horizontal:
                int rand = Random.Range(0,7);
                switch (rand) 
                {
                    case 1: 
                    AddHorizontalStreetOnRun(POS.LEFT);
                    AddHorizontalStreetOnRun(POS.RIGHT);
                    break;
                    case 2: 
                    AddIntersectionStreetOnRun(POS.LEFT);
                    AddHorizontalStreetOnRun(POS.RIGHT);
                    break;
                    case 3: 
                    AddHorizontalStreetOnRun(POS.LEFT);
                    AddIntersectionStreetOnRun(POS.RIGHT);
                    break;
                    case 4: 
                    AddRightUpStreetOnRun();
                    AddHorizontalStreetOnRun(POS.RIGHT);
                    break;
                    case 5:
                    AddHorizontalStreetOnRun(POS.LEFT);
                    AddLeftUpStreetOnRun();
                    break;
                }
               
            break;

            case streetType.Vertical:
                AddHorizontalStreetOnRun(POS.UP);
            break;

            case streetType.LeftUp:
                AddHorizontalStreetOnRun(POS.UP);
            break;

            case streetType.RightUp:
                AddHorizontalStreetOnRun(POS.UP);
            break;
        }
    }

    public void AddVerticalStreetOnRun(POS pos)
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();
        if (pos == POS.LEFT || pos == POS.RIGHT)
            return;
        if (currentStreet.type == streetType.Horizontal)
            return;

        GameObject newStreetGO = VerticalStreetPool.Dequeue();
        Street newStreet = newStreetGO.GetComponent<Street>();
        
        bool isAdd = currentStreet.AddStreet(newStreet, pos);
        
        if (!isAdd) {
            VerticalStreetPool.Enqueue(newStreetGO);
            return; 
        } 
        if (pos == POS.UP)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, verticalOffset, 0); ;
        if (pos == POS.DOWN)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, -verticalOffset, 0);

    }
    public void AddHorizontalStreetOnRun(POS pos)
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();

        if (pos == POS.DOWN || pos == POS.UP)
            return;
        if (currentStreet.type == streetType.Vertical)
            return;

        GameObject newStreetGO = HorizontalStreetPool.Dequeue();
        Street newStreet = newStreetGO.GetComponent<Street>();

        bool isAdd = currentStreet.AddStreet(newStreet, pos);
        if (!isAdd) {
            HorizontalStreetPool.Enqueue(newStreetGO);
            return; 
        }
        if (pos == POS.RIGHT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(horizontalOffset, 0,0); ;
        if (pos == POS.LEFT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3( -horizontalOffset, 0,0);
    }

    public void AddIntersectionStreetOnRun(POS pos)
    {
         Street currentStreet = currentStreetGO.GetComponent<Street>();

        
        if (currentStreet.type == streetType.Horizontal && (pos == POS.UP || pos == POS.DOWN))
            return;

        if (currentStreet.type == streetType.Vertical && (pos == POS.LEFT || pos == POS.RIGHT))
            return;


        GameObject newStreetGO = IntersectionPool.Dequeue();
        Street newStreet = newStreetGO.GetComponent<Street>();

        bool isAdd = currentStreet.AddStreet(newStreet, pos);
        if (!isAdd) 
        {
            IntersectionPool.Enqueue(newStreetGO);
            return;  
        }
        if (pos == POS.UP)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, verticalOffset, 0);
        if (pos == POS.DOWN)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, -verticalOffset, 0);
        if(pos == POS.LEFT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(-horizontalOffset,0, 0);
        if (pos == POS.RIGHT)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(horizontalOffset, 0, 0);

        currentStreetGO = newStreetGO;
    }

    public void AddRightUpStreetOnRun()
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();
        GameObject newStreetGO = RightUpStreetPool.Dequeue();
        Street newStreet = newStreetGO.GetComponent<Street>();

        bool isAdd = currentStreet.AddStreet(newStreet, POS.RIGHT);
        if (!isAdd) return; 

        newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(horizontalOffset, 0,0);
    }

    public void AddLeftUpStreetOnRun()
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();
        GameObject newStreetGO = LeftUpStreetPool.Dequeue();
        Street newStreet = newStreetGO.GetComponent<Street>();

        bool isAdd = currentStreet.AddStreet(newStreet, POS.LEFT);
        if (!isAdd) return; 

        newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(-horizontalOffset, 0,0);
    }

    public void SetCurrentStreet (GameObject street)
    {
        currentStreetGO = street;
    }

    public void PushStreetToPool( GameObject street)
    {
        streetType type = street.GetComponent<Street>().type;
        switch(type){
            case streetType.Horizontal:
                HorizontalStreetPool.Enqueue(street);
            break;
            case streetType.Vertical:
                VerticalStreetPool.Enqueue(street);
            break;
            case streetType.Intersection:
                IntersectionPool.Enqueue(street);
            break;
            case streetType.RightUp:
                RightUpStreetPool.Enqueue(street);
            break;
            case streetType.LeftUp:
                LeftUpStreetPool.Enqueue(street);
            break;

        }
    }


}

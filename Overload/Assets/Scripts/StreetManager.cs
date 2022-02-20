
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetManager : MonoBehaviour
{
    public GameObject currentStreetGO;
    public GameObject IntersectionPrefab;
    public GameObject VerticalStreetPrefab;
 
    public List<GameObject> existingStreets = new List<GameObject>();
    public Queue<GameObject> IntersectionPool = new Queue<GameObject>();
    public Queue<GameObject> VerticalStreetPool = new Queue<GameObject>();
    

    public Street playerOnStreet;

    public float timer;

    public float verticalOffset;

    public float distanceToDestroy = 300f;


    public Scanner scanner;

    private void Start() {

        timer = 0f;
        Street currentStreet = currentStreetGO.GetComponent<Street>();
        Vector3 position = new Vector3(0, -20, 0);
        for(int i  = 0; i < 10; i++)
        {
            IntersectionPool.Enqueue(StreetFactory.CreateIntersection());
            VerticalStreetPool.Enqueue(StreetFactory.CreateVerticalStreet());
           
        }
       
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer > 2f )
        {
            timer = 0;
            for (int i = existingStreets.Count-1; i >=0 ; i--)
            {
                GameObject street = existingStreets[i];
                if (transform.position.y > street.GetComponent<Transform>().position.y && Vector3.Distance(transform.position, street.GetComponent<Transform>().position) > distanceToDestroy)
                {
                    Street streetScript = street.GetComponent<Street>();
                    streetScript.Disconnect(POS.UP);
                    existingStreets.Remove(street);
                    PushStreetToPool(street);
                }
            }
           
        }
    }


    public void AddVerticalStreet(POS pos)
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();

      

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

        GameObject newStreetGO = Instantiate(IntersectionPrefab, currentStreetGO.transform.position, new Quaternion(0, 0, 0, 0));
        Street newStreet = newStreetGO.GetComponent<Street>();
        currentStreet.AddStreet(newStreet, pos); 

        if (pos == POS.UP)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, verticalOffset, 0);
        if (pos == POS.DOWN)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, -verticalOffset, 0);
       
        currentStreetGO = newStreetGO;

    }

    

     public void AddStreet()
    {
        if (currentStreetGO == null)
        {
            return;
        }
        Street currentStreet = currentStreetGO.GetComponent<Street>();
        scanner.moveUP(verticalOffset);

        switch(currentStreet.type)
        {
            case streetType.Intersection:
                AddVerticalStreetOnRun(POS.UP);
            break;

            case streetType.Vertical:
                int R = Random.Range(0, 2);
                if(R == 0)
                    AddIntersectionStreetOnRun(POS.UP); 
                else
                    AddVerticalStreetOnRun(POS.UP);

                break;

           
        }
    }

    public void AddVerticalStreetOnRun(POS pos)
    {
        Street currentStreet = currentStreetGO.GetComponent<Street>();

        GameObject newStreetGO = VerticalStreetPool.Dequeue();
       
        Street newStreet = newStreetGO.GetComponent<Street>();
        
        bool isAdd = currentStreet.AddStreet(newStreet, pos);
        
        if (!isAdd) 
        {
            VerticalStreetPool.Enqueue(newStreetGO);
            return; 
        } 
        if (pos == POS.UP)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, verticalOffset, 0); 
        if (pos == POS.DOWN)
            newStreetGO.transform.position = currentStreetGO.transform.position + new Vector3(0, -verticalOffset, 0);

        newStreetGO.SetActive(true);
        newStreetGO.GetComponent<VerticalStreet>().ActiveLuagage();
        existingStreets.Add(newStreetGO);
        
    }
   

    public void AddIntersectionStreetOnRun(POS pos)
    {
         Street currentStreet = currentStreetGO.GetComponent<Street>();

      


        GameObject newStreetGO = IntersectionPool.Dequeue();
        newStreetGO.SetActive(true);
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

        newStreetGO.SetActive(true);
        currentStreetGO = newStreetGO;
       
        existingStreets.Add(newStreetGO);
    }

   

    public void SetCurrentStreet (GameObject street)
    {
        currentStreetGO = street;
    }

    public void PushStreetToPool(GameObject street)
    {
        street.SetActive(false);
        streetType type = street.GetComponent<Street>().type;
        switch(type){
           
            case streetType.Vertical:
                VerticalStreetPool.Enqueue(street);
            break;
            case streetType.Intersection:
                IntersectionPool.Enqueue(street);
            break;
           

        }
    }


}

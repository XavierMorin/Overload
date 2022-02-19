using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetManager : MonoBehaviour
{
    public GameObject currentStreetGO;
    public GameObject IntersectionPrefab;
    public GameObject HorizontalStreetPrefab;
    public GameObject VerticalStreetPrefab;


    public float verticalOffset;
    public float horizontalOffset;


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

}

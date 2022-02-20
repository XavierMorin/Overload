using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetFactory : MonoBehaviour
{
    public GameObject intersectionPrefab;
    private static GameObject intersectionPrefabStatic;

    public GameObject verticalPrefab;
    private static GameObject verticalPrefabStatic;

    void Start()
    {
        intersectionPrefabStatic = intersectionPrefab;
        verticalPrefabStatic = verticalPrefab;
    }

    public static GameObject CreateIntersection()
    {
        Vector3 position = new Vector3(0, -20, 0);
        GameObject street = Instantiate(intersectionPrefabStatic, position, Quaternion.identity);
        street.SetActive(false);
        return street;

    }

    public static GameObject CreateVerticalStreet()
    {
        Vector3 position = new Vector3(0, -20, 0);
        GameObject street = Instantiate(verticalPrefabStatic, position, Quaternion.identity);
        street.SetActive(false);
        return street;

    }
    
}

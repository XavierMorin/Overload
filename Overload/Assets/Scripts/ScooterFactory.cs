using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterFactory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scooterPrefab;
    private static GameObject scooterPrefabStatic;

    public List<Sprite> headSprite;
    public List<Sprite> bikeSprite;

    private static List<Sprite> headSpriteStatic;
    private static List<Sprite> bikeSpriteStatic;

    private void Start()
    {
        scooterPrefabStatic = scooterPrefab;
        headSpriteStatic = headSprite;
        bikeSpriteStatic = bikeSprite;
    }

    public static GameObject createScooter(Waypoint entry)
    {
        GameObject go = Instantiate(scooterPrefabStatic);

        int r = Random.Range(0, headSpriteStatic.Count);
        go.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = headSpriteStatic[r];
        r = Random.Range(0, bikeSpriteStatic.Count);
        go.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = bikeSpriteStatic[r];

        go.transform.position = entry.GetPosition();
        Scooter scooter = go.GetComponent<Scooter>();
        scooter.currentWayPoint = entry.nextWaypoint;
      

        return go;

    }
}

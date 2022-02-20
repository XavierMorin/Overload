using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    

    // Update is called once per frame
    public static void moveCameraUP(float y)
    {
        Vector3 newPosition = Camera.main.transform.position;

        newPosition.y = newPosition.y + y;
        Camera.main.transform.position = newPosition;
    }
}

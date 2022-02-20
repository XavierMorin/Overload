using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalStreet : MonoBehaviour
{
    public GameObject pickUpLeft;
    public GameObject pickUpRight;
    // Start is called before the first frame update
    public void ActiveLuagage()
    {
        
        int rand = Random.Range(1, 3);
        switch(rand)
        {
            case 1: 
                rand = Random.Range(1,3);
                switch (rand) 
                {
                    case 1:
                        pickUpLeft.SetActive(true);
                        break;
                    case 2:
                        pickUpRight.SetActive(true);
                        break;
                }
            break;
        }
    }
}

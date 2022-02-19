using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private float playerSpeed = 0.2f;
    public StreetManager streetManager;

    private Vector3 lastPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), -1, 0);
        Vector3 norm = Vector3.Normalize(move);
        transform.Translate(norm * Time.deltaTime * playerSpeed);  
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Street")
        {
            Debug.Log("Trigger");
            streetManager.SetCurrentStreet(other.gameObject);
            streetManager.AddStreet();
        }
    }

}

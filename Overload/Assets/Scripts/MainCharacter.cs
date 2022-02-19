using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private CharacterController controller;
    private float playerSpeed = 2.0f;
    public StreetManager streetManager;

    private Vector3 lastPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 1, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);
        gameObject.transform.up = move;
        if (Vector3.Distance(transform.position, lastPosition) > 5f)
        {
            streetManager.AddStreet();
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Street")
        {
            streetManager.SetCurrentStreet(other.gameObject);
            streetManager.AddStreet();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float playerSpeed = 0.2f;
    public float rotationSpeed = 1f;

    public Rigidbody2D rb;
    public StreetManager streetManager;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool isDashing = false;

    public Animator animator;


    float moveX;
    float moveY;
    Vector2 foward = new Vector2(0,1);

    float angle = 0;

    public float maxAngle = 45;
    // Start is called before the first frame update
    void Start()
    {
        dashTime = startDashTime;
        rb.velocity = foward * playerSpeed;
    }

    // Update is called once per frame
   
    private static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    void Update()
    {

        

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isDashing == false)
            {
                animator.SetTrigger("Dash");
            }
            isDashing = true;
        }
            
        if (isDashing)
        {
            if (dashTime <= 0)
            {
                dashTime = startDashTime;
                Vector2 direction = Rotate(foward, -angle);
                rb.velocity = direction * playerSpeed;
                isDashing = false;
            }
            else
            {
                dashTime -= Time.deltaTime;
                Vector2 direction = Rotate(foward, -angle);
                rb.velocity = direction * dashSpeed;

            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = Rotate(foward, -angle);
        if (Mathf.Abs(moveX) > 0.1f)
        {
            
            angle += moveX * rotationSpeed;

            if (angle > maxAngle)
                angle = maxAngle;
            if (angle < -maxAngle)
                angle = -maxAngle;

           
            if(isDashing==false)
                rb.velocity = direction * playerSpeed;
            else
                rb.velocity = direction * dashSpeed;

            var rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            transform.rotation = rotation;
        }

        if(isDashing == false)
             CameraFollow.moveCameraUP(direction.y * playerSpeed * Time.fixedDeltaTime);
        else
            CameraFollow.moveCameraUP(direction.y * dashSpeed * Time.fixedDeltaTime);


    }
    private void OnTriggerEnter2D(Collider2D other) {

        Debug.Log(other.transform.name);
        if (other.gameObject.tag == "Street")
        {
            
            streetManager.SetCurrentStreet(other.gameObject);
            streetManager.AddStreet();
        }
    }

   

}

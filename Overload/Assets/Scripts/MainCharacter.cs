using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    public float playerSpeed = 0.2f;
    public float rotationSpeed = 1f;
    public int life = 3;
    public int score = 0;
    private float timerOfhavingLuaggages = 0f;
    public int amountOfLuaggages = 0;

    public Rigidbody2D rb;
    public StreetManager streetManager;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool isDashing = false;
    private bool isCrashing = false;

    public Animator animator;

    public List<GameObject> luaggages = new List<GameObject>();
    
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

        
        if (isCrashing) return;
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

        UpdateScore();
    }

    private void UpdateScore()
    {
        if(amountOfLuaggages > 0)
        {
            timerOfhavingLuaggages += Time.deltaTime;
            if(timerOfhavingLuaggages > 2f)
            {
                score = score+amountOfLuaggages * 10;
                timerOfhavingLuaggages = 0f;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isCrashing) return;
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

    private void OnCollisionExit2D(Collision2D other) {
        Debug.Log(other.transform.name);
        CameraFollow.Adjust(transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag)
        {
            case "Street":
                streetManager.SetCurrentStreet(other.gameObject);
                streetManager.AddStreet();
            break;
            case "Scooter":
                if (isCrashing) return;
                isCrashing = true;
                life--;
                if(life <= 0 )
                {
                    PlayerPrefs.SetInt("CurrentScore", score);
                    SceneManager.LoadScene(1);
                }
                animator.SetTrigger("Crash");
                Dropluaggage();
                isCrashing = false;
            break;
            case "LuaggageShop":
                luaggages[amountOfLuaggages].SetActive(true);
                amountOfLuaggages++;
            break;
        }
    }

    private void Dropluaggage(){
        timerOfhavingLuaggages = 0f;
        for (int i = 0; i < amountOfLuaggages; i++)
        {
            luaggages[i].SetActive(false);
        }
        amountOfLuaggages = 0;
    }

   

}

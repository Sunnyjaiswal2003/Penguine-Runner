using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float Jump;

    Rigidbody rb;
    [SerializeField]
    bool isGrounded = false;

    Gamemanager gamemanager;

    Animator anim;
    Vector2 startpos = new Vector2(0, 0), endpos = new Vector2(0, 0);
    float Position;
    public float leftRightDistance=2f;
    public GameObject Ragod,Body;
    int coin;
    public int gift;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gamemanager = FindObjectOfType<Gamemanager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gamemanager.isgamepaused)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            startpos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endpos = Input.mousePosition;
            float xDifference = startpos.x - endpos.x;
            float yDifference = startpos.y - endpos.y;

            if (xDifference < 0)
            {
                xDifference *= -1;
            }
            if (yDifference < 0)
            {
                yDifference *= -1;
            }
            if (xDifference > yDifference)
            {
                if (startpos.x > endpos.x)
                {
                    Debug.Log("Left");
                    if (Position == 0 || Position == leftRightDistance) 
                    {
                        Position -= leftRightDistance;
                    }
                }
                else
                {
                    Debug.Log("Right");
                    if(Position == -leftRightDistance || Position == 0)
                    {
                        Position += leftRightDistance;
                    }
                }
            }
            else
            {
                if (startpos.y < endpos.y)
                {
                    Debug.Log("UP");
                    jump();
                }

                else
                {
                    anim.SetTrigger("Slide");
                }
            }
        }
        if (!isGrounded)
        {
            anim.SetFloat("Speed", 3);
        }
        else
        {
            anim.SetFloat("Speed", 0);
            transform.Translate(0, 0, Speed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(Position, transform.position.y, 5), .1f);
            transform.Translate(Vector3.left * -Input.GetAxis("Horizontal") * Speed * Time.deltaTime);

        }
        CheckGround();
    }
    void fallAnim()
    {
        anim.SetTrigger("Fall");
    }
    void jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * Jump, ForceMode.Impulse);
            isGrounded = false;
            anim.SetTrigger("Jump");
            Invoke(nameof(fallAnim), .5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            Debug.Log("<color=RED>Death</color>");
            gameOver();
        }
        
    }

    void CheckGround()
    {
        RaycastHit hit;

        Ray ray = new Ray();
        ray.direction = Vector3.down;
        ray.origin = this.transform.position + new Vector3(0, .1f, 0);

        if(Physics.Raycast(ray,out hit, .3f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;

        }
        anim.SetBool("IsGrounded", isGrounded);

        if (anim.GetBool("IsGrounded"))
        {
            anim.SetTrigger("Running");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Diamond")
        {
            coin++;
            Destroy(other.gameObject);
            FindObjectOfType<Gamemanager>().Score++;
        }

        if(other.tag== "Gift")
        {
            Destroy(other.gameObject);
            gamemanager.giftscore++;
        }
    }

    public void gameOver()
    {
        anim.SetTrigger("Death");
        var chunks = FindObjectsOfType<Chunks>();

        foreach(var c in chunks)
        {
            c.StopChunk();
        }

        FindObjectOfType<Bear>().Hunt(Ragod.transform);
        Invoke(nameof(showgameoverdelay), 4f);
       
        anim.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        Ragod.SetActive(true);
        Body.SetActive(false);
      var collider =  Physics.OverlapSphere(this.transform.position, 5);
        rb.isKinematic = true;
        foreach (var x in collider)
        {
            Debug.Log(x.gameObject.name);
            var rb_ =
            x.GetComponent<Rigidbody>();
            if(rb_ != null)
            {
                rb_.AddExplosionForce(800, this.transform.position, 6, 2);
            }
        }   
    }

    void SetChildColliders(bool val)
    {
        var coll = GetComponentsInChildren<CapsuleCollider>();
        var col2 = GetComponentsInChildren<SphereCollider>();

        foreach(var x in coll)
        {
            x.enabled = val;
            x.GetComponent<Rigidbody>().isKinematic = !val;
        }

        foreach (var x in col2)
        {
            x.enabled = val;
            x.GetComponent<Rigidbody>().isKinematic = !val;
        }
    }

    void showgameoverdelay()
    {
        FindObjectOfType<Gamemanager>().ShowGameOver();
    }
}

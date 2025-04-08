using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool isGiant = false;
    public Rigidbody2D rb;
    private Animator pAni;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent <Animator>();
    }
    
      




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0)
            transform.localScale = new Vector3(1f,1f,1f);
        if (moveInput > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (isGiant)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(2f,2f,1f);
            if (moveInput > 0)
                transform.localScale = new Vector3(-2f, 2f, 1f);
            else
            {
                if (moveInput < 0)
                    transform.localScale = new Vector3(1f,1f,1f);
                if (moveInput > 0)
                transform.localScale = new Vector3(-1f,1f,1f);
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("JumpAction");
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.CompareTag("Item"))
        {
            isGiant = true;
            Destroy(collision.gameObject);
        }
    }
}









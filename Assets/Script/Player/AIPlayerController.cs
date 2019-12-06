using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    public int id;
    Rigidbody2D rb;
    public float jumpVelocity = 13f;
    public float fallMultiplier = 3f;

    bool isJumping = true;
    bool isFalling = true;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance.alives += 1;
        transform.position += Vector3.right * ((Random.Range(1.0f, 2.0f) * 2) - 1);
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isJumping && !isFalling && !GameManager.instance.gameOver)
        {
            float distance = 10;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 10f, LayerMask.GetMask("EnemyLayer"));

            if (hit) {
                if (hit.collider.CompareTag("Enemy")) {
                    distance = hit.distance;
                }
            }

            double[,] input = new double[,] { { GameManager.instance.speed, distance }};

            double neural = NeuralManager.instance.neuralGenetic.generations[id].Compute(input)[0, 0];

            if (neural > 0.5) {
                Jump();
            }
        }

        if (rb.velocity.y > 0 && !GameManager.instance.gameOver)
        {
            isJumping = false;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (GameManager.instance.gameOver)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            isFalling = false;
            animator.SetBool("IsJumping", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.alives -= 1;
            NeuralManager.instance.neuralGenetic.generations[id].SetScore(GameManager.instance.score);
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) {
            isJumping = true;
            isFalling = true;
            animator.SetBool("IsJumping", true);
        }
    }
}

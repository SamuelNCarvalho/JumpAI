using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public AudioClip jumpAudioClip;
    public AudioClip deadAudioClip;

    public float jumpVelocity = 13f;
    public float fallMultiplier = 3f;

    bool isJumping = false;
    bool isFalling = false;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        GameManager.instance.alives += 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && !isFalling && !GameManager.instance.gameOver)
        {
            rb.velocity = Vector2.up * jumpVelocity;
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
            animator.SetBool("IsDead", true);
            audioSource.PlayOneShot(deadAudioClip);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) {
            isJumping = true;
            isFalling = true;
            if (!GameManager.instance.gameOver)
            {
                animator.SetBool("IsJumping", true);
                audioSource.PlayOneShot(jumpAudioClip);
            }
        }
    }
}

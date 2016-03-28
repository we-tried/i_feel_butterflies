using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

    Rigidbody2D rb;
    Animator animator;

    bool inCombat = false;
    float speed = 10f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

    void OnTriggerEnter2D()
    {
        inCombat = true;
        rb.velocity = new Vector2(3 * speed, -10f);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(!inCombat)
        {
            float move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(move * speed, 0);
            animator.SetFloat("Speed", move * speed);
        }        
	}
}

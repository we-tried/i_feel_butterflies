using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

    Rigidbody2D rb;
    Animator animator;

    bool inCombat = false;
    float speed = 15f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

    public void ToggleCombat()
    {
        inCombat = !inCombat;
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

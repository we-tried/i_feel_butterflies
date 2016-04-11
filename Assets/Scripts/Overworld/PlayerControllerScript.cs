﻿using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

    Rigidbody2D rb;
    Animator animator;
    public GameObject gm;

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
            if (move >= 0)
                rb.velocity = new Vector2(move * speed, 0);
            else
                rb.velocity = new Vector2(0, 0);
            animator.SetFloat("Speed", move * speed);
        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!inCombat)
        {
            print("Player Collision: ENter combat ::::::" + col.gameObject.GetInstanceID().ToString());
            Destroy(col.gameObject);
            gm.GetComponent<GameManagerScript>().EnterCombat();
            rb.velocity = new Vector2(0, 0);
        }
    }
}

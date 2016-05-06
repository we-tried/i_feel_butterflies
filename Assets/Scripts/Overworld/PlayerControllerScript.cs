using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

    Rigidbody2D rb;
	SpriteRenderer sr;
    Animator animator;
    public GameObject gm;

    bool inCombat = false;
    float speed = 15f;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
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

			if (move > 0)
				sr.flipX = false;
			else if (move < 0)
				sr.flipX = true;
			animator.SetFloat("Speed", Mathf.Abs(move) * speed);
        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == "CombatInitiator") {
			if (!inCombat) {
				print ("Player Collision: ENter combat ::::::" + col.gameObject.GetInstanceID ().ToString ());
				Destroy (col.gameObject);
				gm.GetComponent<GameManagerScript> ().EnterCombat ();
				rb.velocity = new Vector2 (0, 0);
			}
		}
		else if (col.tag == "Transport1")
		{
			gm.GetComponent<GameManagerScript> ().Transport1 ();
		}
    }
}

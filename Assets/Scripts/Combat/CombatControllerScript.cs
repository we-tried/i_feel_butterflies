using UnityEngine;
using UnityEngine.UI;

public class CombatControllerScript : MonoBehaviour {
	Rigidbody2D rb; 
    Animator animator;
    public GameObject gm;
    public Canvas c;
    GameManagerScript gms;
    ButterflyGeneratorScript butterfly;
    float speed = 16f;
    public int health;
    bool inCombat = false;
	public bool altMode;
	public WordGeneratorScript wgs;
	 
    // Use this for initialization
    void Start()
    {
		//if (!altMode)
			butterfly = GetComponent<ButterflyGeneratorScript>();
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gms = gm.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inCombat)
        {
            float move = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(0, move * speed,0);
        }
    }

    public void ToggleCombat()
    {
        inCombat = !inCombat;
    }

	void OnCollisionExit2D(Collision2D coll)
	{
		if (altMode) {
			Collider2D col = coll.collider;
			WordScript ws = col.gameObject.GetComponent<WordScript> ();
			string tag = col.gameObject.tag;
			if (tag == "Untagged")
				return;
			TextMesh tm = col.gameObject.GetComponent<TextMesh> ();
			string text = tm.text;

			BoxCollider2D bc = col.gameObject.GetComponent<BoxCollider2D> ();
			bc.enabled = false;

			ws.hit = true;

			if (tag == "Neutral") {
			} else if (tag == "Bad") {
			} else if (tag == "Good") {
			}
		}			
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		if (!altMode) {
			WordScript ws = col.gameObject.GetComponent<WordScript> ();
			string id = ws.id;
			string tag = col.gameObject.tag;
			TextMesh tm = col.gameObject.GetComponent<TextMesh> ();
			string text = tm.text;

			if (WordGeneratorScript.repeat == true)
				WordGeneratorScript.repeat = false;
			Destroy (col.gameObject); 
			animator.SetTrigger ("Talk");
			c.GetComponentInChildren<Text> ().text += " " + text;

			if (tag == "Neutral") {
				if (WordGeneratorScript.repeat == true)
					WordGeneratorScript.repeat = false;
				//c.GetComponentInChildren<Text> ().text += " " + text;
			} else if (tag == "Bad") {
				if (WordGeneratorScript.repeat == true)
					WordGeneratorScript.repeat = false;
				butterfly.Spawn ();
				//c.GetComponentInChildren<Text> ().text += " " + text;
				WordGeneratorScript.butterflies = (WordGeneratorScript.butterflies + 1);
			} else if (tag == "Good") {
				if (WordGeneratorScript.repeat == true)
					WordGeneratorScript.repeat = false;
				WordGeneratorScript.butterflies = (WordGeneratorScript.butterflies - 1);
				//c.GetComponentInChildren<Text> ().text += " " + text;
			} else if (tag == "Special") {
				wgs.ActivateSequence (id);
			}
			/*
        else if(tag == "Powerup")
        {

        }
        */
		}
    }
}
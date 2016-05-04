using UnityEngine;
using System.Collections;

public class WordScript : MonoBehaviour {
    
	Rigidbody2D rb;
	TextMesh tm;
	public bool altMode;
	public bool hit;
	public float fadeTime;
	public ButterflyGeneratorScript butterfly;
	public Camera combatCam;
	string tag;
	public WordGeneratorScript wgs;
	public string id; //NEW
	//bool activated; //NEW

    void Start ()
    {
		tag = this.gameObject.tag;
		//activated = false; //NEW
		hit = false;
        rb = GetComponent<Rigidbody2D>();
		tm = GetComponent<TextMesh>();
    }
	// Update is called once per frame
	void Update () {
		if (!altMode) {
			if (rb.position.x < -220 || rb.position.x > -120)
				Destroy (this.gameObject);
		}
		else {
			if (hit) {
				if (tm.color.a > 0)
					tm.color = new Color (tm.color.r, tm.color.g, tm.color.b, tm.color.a - (Time.deltaTime / fadeTime));
				else
					Destroy (this.gameObject);
			} else if (rb.position.x < (-198)) { //-198
				//string tag = this.gameObject.tag;
				TextMesh tm = GetComponent<TextMesh> ();
				string text = tm.text;
				if (WordGeneratorScript.repeat == true)
					WordGeneratorScript.repeat = false;
				//activated = true;
				Destroy (this.gameObject); 
				if (tag == "Neutral") {
				} else if (tag == "Bad") {
					if (butterfly != null)
						butterfly.Spawn ();
					else
						print ("No ButterflyGeneratorScript Attached To Word");
					WordGeneratorScript.butterflies = (WordGeneratorScript.butterflies + 1);
				} else if (tag == "Good") {
					WordGeneratorScript.butterflies = (WordGeneratorScript.butterflies - 1);

				} else if (tag == "Special") {
					wgs.ActivateSequence (id);
				}
			}
		}
		if (rb.position.y > 104 || rb.position.y < 63 || rb.position.x > -120)
			Destroy (this.gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class WordScript : MonoBehaviour {
    Rigidbody2D rb;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	// Update is called once per frame
	void Update () {
        if (rb.position.x < -220 || rb.position.x > -120)
            Destroy(this.gameObject);
	}
}

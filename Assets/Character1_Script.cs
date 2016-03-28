using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Character1_Script : MonoBehaviour {
    Rigidbody2D rb;
    bool triggered = false;
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D() {
        if (!triggered) {
            SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
            triggered = true;
            rb.velocity = new Vector2(30f, 10f);
        }
    }
}

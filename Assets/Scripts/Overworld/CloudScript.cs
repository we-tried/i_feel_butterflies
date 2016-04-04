using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.position.x < -250 || rb.position.x > 250)
            Destroy(this.gameObject);
    }
}

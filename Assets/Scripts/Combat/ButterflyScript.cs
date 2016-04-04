using UnityEngine;
using System.Collections;

public class ButterflyScript : MonoBehaviour {

    public float minx, maxx, miny, maxy, timermin, timermax;

    //AI properties
    Vector2 p;
    Rigidbody2D rb;
    float speed = 5f;
    float t, timer;    

    void Start()
    {
        timer = Random.Range(timermin, timermax);
        t = timer;
        rb = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t >= timer)
        {
            t = 0;
            timer = Random.Range(timermin, timermax);

            float x = Random.Range(-speed, speed);
            float y = Random.Range(-speed, speed);

            rb.velocity = new Vector3(x, y, 0);
        }

        //Check that your AI is within your boundaries
        if (p.x < minx || p.x > maxx)
        {
            rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, 0);
        }
        if (p.y < miny && p.y > maxy)
        {
            rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, 0);
        }
    }
}

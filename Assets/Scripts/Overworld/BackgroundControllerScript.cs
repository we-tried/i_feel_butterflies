using UnityEngine;
using System.Collections;

public class BackgroundControllerScript : MonoBehaviour {
    public GameObject cloud1, cloud2, cloud3, cloud4;
    public float spawnTime = 0.5f;

    public float yMin;
    public float yMax;
    public float xLeft;
    public float xRight;
    public float speed = 5f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        Vector2 spawnPos;
        GameObject cloud = cloud1;

        float velocity;
        bool left = Random.Range(0,1) > 0.5;
        float c = Random.Range(0, 4);

        if (left)
        {
            velocity = speed;
            spawnPos = new Vector2(xLeft, Random.Range(yMin, yMax));
        }
        else
        {
            spawnPos = new Vector2(xRight, Random.Range(yMin, yMax));
            velocity = -speed;
        }

        if (c < 1)
            cloud = cloud1;
        else if (c < 2)
            cloud = cloud2;
        else if (c < 3)
            cloud = cloud3;
        else if (c < 4)
            cloud = cloud4;

        GameObject i = Instantiate(cloud, spawnPos, Quaternion.identity) as GameObject;
        Rigidbody2D rb = i.GetComponent<Rigidbody2D>();       

        rb.velocity = new Vector2(velocity * 3, 0);
    }
}

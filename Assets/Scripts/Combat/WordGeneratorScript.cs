using UnityEngine;
using System.Collections;

public class WordGeneratorScript : MonoBehaviour {
    public GameObject wordPrefab;
    public float spawnTime = 0.5f;
    public bool left = true;

    public float yMin;
    public float yMax;
    public float xLeft;
    public float xRight;
    public float speed = 10f;

	// Use this for initialization
	void Start () {
    }

    public void Activate ()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	void Spawn () {
        Vector2 spawnPos;
        float velocity;
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
        GameObject word = Instantiate(wordPrefab, spawnPos, Quaternion.identity) as GameObject;
        Rigidbody2D rb = word.GetComponent<Rigidbody2D>();

        TextMesh tm = word.GetComponent<TextMesh>();
        tm.text = "Sup";

        if(Random.Range(0,5) > 1)
        {
            tm.text = "uhm";
            tm.tag = "Bad";
            tm.color = Color.red;
        }

        rb.velocity = new Vector2(velocity*3, 0);

        left = !left;
	}
}

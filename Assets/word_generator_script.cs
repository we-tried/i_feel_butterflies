using UnityEngine;
using System.Collections;

public class word_generator_script : MonoBehaviour {

    public GameObject wordPrefab;
    public float spawnTime = 0.5f;
    public bool left = true;

    public float yMin = 0f;
    public float yMax = 20f;
    public float xLeft = -10f;
    public float xRight = 10f;
    public float speed = 10f;


	// Use this for initialization
	void Start () {
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

        rb.velocity = new Vector2(velocity*3, 0);

        left = !left;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CombatControllerScript : MonoBehaviour {

    Rigidbody2D rb;
    float speed = 10f;
    int health = 0;
    butterfly_generator butterfly;
    public GameObject butterfly_prefab;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        butterfly = GetComponent<butterfly_generator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(0, move * speed);
    }

    void OnCollisionEnter2D()
    {
        System.Console.WriteLine("wtf");
        Vector2 spawnPos = new Vector2(health-3, 6);
        GameObject word = Instantiate(butterfly_prefab, spawnPos, Quaternion.identity) as GameObject;        
        health++;
        if (health > 3) {
            SceneManager.LoadScene("Overworld", LoadSceneMode.Single);
        }
    }
}

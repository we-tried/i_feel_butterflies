using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour {

    public GameObject ButterflyPrefab;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 5; i++)
            spawn();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Game");
    }

    void spawn()
    {
        Vector3 spawn = gameObject.GetComponent<Rigidbody2D>().position;
        spawn.z = 20;
        GameObject word = Instantiate(ButterflyPrefab, spawn, Quaternion.identity) as GameObject;

        float c = Random.Range(0, 4);
        Animator a = word.GetComponent<Animator>();
        if (c < 1)
            a.Play("green");
        else if (c < 2)
            a.Play("blue");
        else if (c < 3)
            a.Play("yellow");
        else if (c < 4)
            a.Play("pink");
    }
}

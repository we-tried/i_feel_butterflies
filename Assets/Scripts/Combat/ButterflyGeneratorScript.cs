using UnityEngine;
using System.Collections;

public class ButterflyGeneratorScript : MonoBehaviour {

    public GameObject ButterflyPrefab;
    private ArrayList butterflies;
    // Use this for initialization
    void Start () {
        butterflies = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn ()
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

        butterflies.Add(word);
    }

    public void Despawn ()
    {
        /*
        foreach(GameObject b in butterflies)
        {
            Destroy(b);
        }
        */
    }
}

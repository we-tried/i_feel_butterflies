using UnityEngine;
using System.Collections;

public class ButterflyGeneratorScript : MonoBehaviour {

    public GameObject ButterflyPrefab;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn ()
    {
        Vector3 spawn = gameObject.GetComponent<Rigidbody2D>().position;
        spawn.z = 20;
        GameObject word = Instantiate(ButterflyPrefab, spawn, Quaternion.identity) as GameObject;
    }
}

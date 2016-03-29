using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KidScript : MonoBehaviour {
    public GameObject gm;
    bool triggered = false;
    // Use this for initialization
	void Start () {
    }

    void OnTriggerEnter2D() {
        if(!triggered)
        {
            GameManagerScript gms = gm.GetComponent<GameManagerScript>();
            gms.ToggleCombat();
            triggered = true;
        }        
    }
}

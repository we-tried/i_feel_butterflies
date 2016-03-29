using UnityEngine;
using UnityEngine.UI;

public class CombatControllerScript : MonoBehaviour {
    Rigidbody2D rb;
    Animator animator;
    public GameObject gm;
    public Canvas c;
    GameManagerScript gms;
    ButterflyGeneratorScript butterfly;
    float speed = 16f;
    public int health;
    bool inCombat = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        butterfly = GetComponent<ButterflyGeneratorScript>();
        gms = gm.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inCombat)
        {
            float move = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(0, move * speed);
        }
    }

    public void ToggleCombat()
    {
        inCombat = !inCombat;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        TextMesh tm = col.gameObject.GetComponent<TextMesh>();
        string text = tm.text;

        Destroy(col.gameObject);
        if (tag == "Bad")
        {
            butterfly.Spawn();

        }
        else if (tag == "Good")
        {
            animator.SetTrigger("Talk");
            c.GetComponentInChildren<Text>().text += " " + text;

        }
        /*
        else if(tag == "Powerup")
        {

        }
        */
    }
}
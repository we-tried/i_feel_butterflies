using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using UnityEngine.UI;

public class WordGeneratorScript : MonoBehaviour {
    public GameObject wordPrefab, enemySprite;
    public Sprite sprite2;
    public float spawnTime = 0.5f;
    private bool left = true;
    private bool dialogue = true;
    public Text ptext, etext;
    private string filePath;
    private StreamReader stream;

    public GameObject gmo;
    private GameManagerScript gm;

    private int sceneCounter = 1; 

    public float yMin;
    public float yMax;
    public float xLeft;
    public float xRight;
    public float speed = 10f;

    int pause = 0;

	// Use this for initialization
	void Start () {
        gm = gmo.GetComponent<GameManagerScript>();
        // draw correct character as enemy
    }

    void newFile()
    {
        string f = sceneCounter.ToString() + ".txt";
        if(sceneCounter > 1)
        {
            enemySprite.GetComponent<SpriteRenderer>().sprite = sprite2;
        }
        sceneCounter++;
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, f);
        stream = new StreamReader(filePath, Encoding.Default);
    }


    public void Activate ()
    {
        newFile();
        InvokeRepeating("readLine", spawnTime, spawnTime);
    }

    public void Deactivate()
    {
        CancelInvoke("readLine");
        ptext.text = "";
        etext.text = "";
        gm.ExitCombat();
    }

    void Spawn (string s, bool good) {
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
        tm.text = s;
        tm.color = Color.green;

        if(!good)
        {
            tm.tag = "Bad";
            tm.color = Color.red;
        }

        rb.velocity = new Vector2(velocity*3, 0);

        left = !left;
	}

    void readLine()
    {
        if(pause > 0)
        {
            pause--;
            return;
        }

        string s = stream.ReadLine();

        if (s == "")
        {
            return;
        }
        else if (s == "s")
        {
            pause = 2;
        }

        else if (s == "d")
        { 
            ptext.text = "";
            etext.text = stream.ReadLine();
        }
        else if (s == "g")
        {
            Spawn(stream.ReadLine(), true);
        }
        else if (s == "b")
        {
            Spawn(stream.ReadLine(), false);
        }
        else if (s == "e")
        {
            print("Deactivating word gen");
            Deactivate();
        }
        else {
            return;
        }
    }
}

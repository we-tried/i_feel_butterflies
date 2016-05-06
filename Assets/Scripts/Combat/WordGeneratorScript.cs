using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
//using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordGeneratorScript : MonoBehaviour {
	public bool altMode;
	public GameObject wordPrefab, enemySprite, playerCombatHead;
	public Sprite you, child, punk, girl, man1, man2, woman1, woman2, woman3, woman4, woman5, woman6, woman7;
    public float spawnTime = 0.5f;
    private bool left = false;
    private bool dialogue = true;
    public Text ptext, etext;
    private string filePath;
    private StreamReader stream;
	private Animator animator;
	private int streamLength;
	private ArrayList allActivated;
	[SerializeField] private Camera cam;
	[SerializeField] private ButterflyGeneratorScript bgs;


    public GameObject gmo;
    private GameManagerScript gm;

    private int sceneCounter = 1; //!!!USUALLY 1!!!

    public float yMin;
    public float yMax;
    public float xLeft;
    public float xRight;
    public float speed = .1f;

    int pause = 0;

	public static bool repeat = false;
	string repTag = "";
	string repLine = "";

	public static int butterflies;
	private bool badBranch = false;
	private bool ignore = false;
	// Use this for initialization
	void Start () {
		allActivated = new ArrayList ();
		animator = playerCombatHead.GetComponent<Animator>();
		gm = gmo.GetComponent<GameManagerScript>();
		butterflies = 0;
    }

    void newFile()
    {

		allActivated.Clear ();

		string f;

		if (!altMode)
        	f = sceneCounter.ToString() + ".txt";
		else
			f = sceneCounter.ToString() + "A.txt";
		if(sceneCounter == 1)
		{
			speed = 5f;
			enemySprite.GetComponent<SpriteRenderer>().sprite = you;
			//speed = 10f;
		}
        else if(sceneCounter == 2)
        {
            enemySprite.GetComponent<SpriteRenderer>().sprite = woman3;
			speed = 2.2f;
        }
        else if (sceneCounter == 3)
        {
			speed = 5f;
            enemySprite.GetComponent<SpriteRenderer>().sprite = you;
        }
        else if (sceneCounter == 4)
        {
            enemySprite.GetComponent<SpriteRenderer>().sprite = child;
        }
        else if (sceneCounter == 5)
        {
            enemySprite.GetComponent<SpriteRenderer>().sprite = woman2;
        }
        else if (sceneCounter == 6)
        {
            enemySprite.GetComponent<SpriteRenderer>().sprite = punk;
            speed = 13f;
        }
        sceneCounter++;
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, f);
        stream = new StreamReader(filePath, Encoding.Default);
    }

	void swapEnemySprite(string name, bool fade)
	{
		if (name == "you")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = you;
		else if (name == "child")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = child;
		else if (name == "punk")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = punk;
		else if (name == "girl")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = girl;
		else if (name == "man1")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = man1;
		else if (name == "man2")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = man2;
		else if (name == "woman1")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = woman1;
		else if (name == "woman2")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = woman2;
		else if (name == "woman3")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = woman3;
		else if (name == "woman4")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = woman4;
		else if (name == "woman5")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = woman5;
		else if (name == "woman6")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = woman6;
		else if (name == "woman7")
			enemySprite.GetComponent<SpriteRenderer> ().sprite = woman7;

		//SpriteRenderer esr = enemySprite.GetComponent<SpriteRenderer> ();
		if (name == "none")
			enemySprite.GetComponent<SpriteRenderer> ().color = new Color(1,1,1, 0f);
		else if (fade)
			enemySprite.GetComponent<SpriteRenderer> ().color = new Color(1,1,1, .5f);
		else
			enemySprite.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,1f);
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

        //if(sceneCounter == 7)
            //SceneManager.LoadScene("Menu");
    }

	void Spawn (string s, string type, string id) {
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

		WordScript ws = word.GetComponent<WordScript>();
		ws.butterfly = bgs;
		ws.combatCam = cam;
		ws.altMode = this.altMode;
		ws.wgs = this;

		TextMesh tm = word.GetComponent<TextMesh>();
        tm.text = s;
        tm.color = Color.white;
		if (type == "Neutral") 
		{
			tm.tag = "Neutral";
			tm.color = Color.white;
		}
		else if(type == "Good")
		{
			tm.tag = "Good";
			tm.color = Color.green;
		}
		else if(type == "Bad")
        {
            tm.tag = "Bad";
            tm.color = Color.magenta;
        }
		else if(type == "Special")
		{
			ws.id = id;
			tm.tag = "Special";
			tm.color = Color.white;
		}


		Rigidbody2D rb = word.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velocity*3, 0);


		BoxCollider2D bc = word.AddComponent<BoxCollider2D> ();
		bc.size = new Vector2 (bc.size[0] - .1f, 1.11f);
		bc.offset = new Vector2 (bc.offset [0] + .05f, -0.03f);
		if (altMode)
			bc.isTrigger = false;
		else
			bc.isTrigger = true;

		if (!altMode){
			bc.isTrigger = true;
       		left = !left;
		}
	}

	public void ActivateSequence(string id)
	{
		allActivated.Add (id);
	}
	
    void readLine()
    {

        if(pause > 0)
        {
            pause--;
            return;
        }

		string s = "";

		if (repeat) {
			Spawn (repLine, repTag, null);
			pause = 5;
			//repeat--;
		} else 
			s = stream.ReadLine ();
		

		if (s == "") {
			return;
		} else if (s == "z") { //Branching End
			butterflies = 0;
		}

		else if (s == "bs") { //Branching Start
			string id = stream.ReadLine ();
			print(butterflies);
			int badButterflyBranchAmt = int.Parse (stream.ReadLine ());
			if (butterflies >= badButterflyBranchAmt) {
				while(s != "bb" + id)
					s = stream.ReadLine ();
			} else {
				while(s != "bg" + id)
						s = stream.ReadLine ();
				}
			} 
		else if (s == "be") { //Branching End
			string id = stream.ReadLine ();
			while (s != "bee" + id)
				s = stream.ReadLine ();
		} 

		else if (s == "u") { 
			string id = stream.ReadLine ();
			/*string tag = stream.ReadLine ();
			if (tag == "b")
				tag = "Bad";
			else if (tag == "g")
				tag = "Good";
			else
				tag = "Neutral";*/
			Spawn (stream.ReadLine (), "Special", id);
		} 
		else if (s == "us") { //Repeats the thrown word, until the player says it.
			bool proceed = false;
			string id = stream.ReadLine ();
			foreach (string pid in allActivated)
			{
				if (pid == id)
					proceed = true;
			}
			if (proceed == false)
				while(s != "ue")
					s = stream.ReadLine ();
		} 


		else if (s == "r") { //Repeats the thrown word, until the player says it.
			repTag = stream.ReadLine ();
			repLine = stream.ReadLine ();
			if (repTag == "b")
				repTag = "Bad";
			else if (repTag == "g")
				repTag = "Good";
			else if (repTag == "n")
				repTag = "Neutral";
			else {
				repLine = repTag;
				repTag = "Neutral";
			}
				repeat = true;
		} 

		else if (s == "p") {
			int p = int.Parse (stream.ReadLine ());
			pause = p;
		} 
		else if (s == "s") {
			bool fade;
			string name = stream.ReadLine ();
			string f = stream.ReadLine ();
			if (f == "t")
				fade = true;
			else
				fade = false;
			swapEnemySprite(name, fade);
		} 

		else if (s == "d") { 
			pause = 2;
			ptext.text = "";
			etext.text = stream.ReadLine ();
			etext.alignment = TextAnchor.MiddleRight;
			//streamLength = etext.Length;
		} 
		else if (s == "c") {
			pause = 2;
			ptext.text = stream.ReadLine ();
			ptext.alignment = TextAnchor.MiddleLeft;
			animator.SetTrigger ("Talk");
			//streamLength = ptext.Length;
		} 
		else if (s == "n") {
			Spawn (stream.ReadLine (), "Neutral", null);
		} 
		else if (s == "g") {
			Spawn (stream.ReadLine (), "Good", null);
		} 
		else if (s == "b") {
			Spawn (stream.ReadLine (), "Bad", null);
		} 
		else if (s == "e") {
			print ("Deactivating word gen");
			Deactivate ();
		}
        else {
            return;
        }
    }

}

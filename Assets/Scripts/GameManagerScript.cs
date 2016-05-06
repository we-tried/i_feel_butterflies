using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public bool globalAltMode;
    private static GameManagerScript manager = null;
    public GameObject player, combatPlayer, combatController, background, bookend;
	[SerializeField] private AudioSource overworldMusic;
	[SerializeField] private AudioSource combatMusic;
	[SerializeField] private AudioSource outsideAmbient;
	//[SerializeField] private AudioSource schoolAmbient;
	[SerializeField] private AudioSource schoolBellSound;
	private BookendControllerScript beController;
    private BackgroundControllerScript bgController;
    private PlayerControllerScript playerController;
    private CombatControllerScript combatPlayerController;
    private WordGeneratorScript wordGenerator;
	private bool transitionIntoCombat;
	private bool transitionOutOfCombat;
	private float vpx;
	public Camera cam, combatCam, bookendCam;

    int sceneCounter = 1;
	
    public static GameManagerScript Manager
    { 
        get { return manager; }
    }

    void Awake()
    {
        GetThisGameManager();
    }

    // Use this for initialization
	void Start () {
        print("init");
		bookendCam.enabled = true;
        cam.enabled = true;
        combatCam.enabled = false;


        playerController = player.GetComponent<PlayerControllerScript>();
        combatPlayerController = combatPlayer.GetComponent<CombatControllerScript>();
        wordGenerator = combatController.GetComponent<WordGeneratorScript>();
        bgController = background.GetComponent<BackgroundControllerScript>();
		beController = bookend.GetComponent<BookendControllerScript>();

        bgController.Activate();

		transitionIntoCombat = false;
		transitionOutOfCombat = false;

		combatPlayerController.altMode = this.globalAltMode;
		wordGenerator.altMode = this.globalAltMode;
		beController.altMode = this.globalAltMode;
    }

    void CombatTrigger ()
    {
    }

    void GetThisGameManager()
    {
        if (manager != null && manager != this)
        {
            print("wow");
            Destroy(this.gameObject);
            return;
        }
        else
        {
            manager = this;
        }
    }

    public void EnterCombat () 
	{
		overworldMusic.Stop ();
		outsideAmbient.Pause ();
		combatMusic.Play ();
       //ChangeCamera();
        print("enter");
        playerController.ToggleCombat();
        combatPlayerController.ToggleCombat();
        wordGenerator.Activate();
        bgController.Deactivate();
		combatCam.enabled = true;
		vpx = 1f;
		combatCam.rect = new Rect (vpx, 0, 1, 1);
		transitionIntoCombat = true;

    }

	void FixedUpdate()
	{
		if (transitionIntoCombat)
		{
			if (vpx > 0) {
				combatCam.rect = new Rect (vpx, 0, 1, 1);
				vpx = vpx - Time.deltaTime;
				combatCam.rect = new Rect (vpx, 0, 1, 1);
			} else {
				vpx = 0f;
				combatCam.rect = new Rect (vpx, 0, 1, 1);
				transitionIntoCombat = false;
				cam.enabled = false;
			}
		}
		if (transitionOutOfCombat)
		{
			if (vpx > -1) {
				combatCam.rect = new Rect (vpx, 0, 1, 1);
				vpx = vpx - Time.deltaTime;
				combatCam.rect = new Rect (vpx, 0, 1, 1);
			} else {
				vpx = -1f;
				combatCam.rect = new Rect (vpx, 0, 1, 1);
				transitionOutOfCombat = false;
				combatCam.enabled = false;
			}
		}

	}
    public void ExitCombat()
    {
		combatMusic.Stop ();
		overworldMusic.Play ();
		outsideAmbient.Play ();
        print("exit");
        //ChangeCamera();
        playerController.ToggleCombat();
        combatPlayerController.ToggleCombat();
        bgController.Activate();
		cam.enabled = true;
		vpx = 0f;
		combatCam.rect = new Rect (vpx, 0, 1, 1);
		transitionOutOfCombat = true;

        sceneCounter++;

        if (sceneCounter == 3)
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn1");

            player.GetComponent<Rigidbody2D>().position = spawns[0].transform.position;
        }
		if (sceneCounter == 4) {
			beController.AddScene ();
		}

    }

	public void Transport1()
	{
		schoolBellSound.Play ();
		outsideAmbient.Stop ();
		//schoolAmbient.Play ();
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn2");
		player.GetComponent<Rigidbody2D>().position = spawns[0].transform.position;
	}

	public void BookendCamChange()
	{
		print("be cam change");
		bookendCam.enabled = !bookendCam.enabled;
		if (bookendCam.enabled == false)
			overworldMusic.Play ();
		else
			overworldMusic.Stop ();
	}
    public void ChangeCamera ()
    {
        print("cam change");
        cam.enabled = !cam.enabled;
        combatCam.enabled = !combatCam.enabled;
    }
}

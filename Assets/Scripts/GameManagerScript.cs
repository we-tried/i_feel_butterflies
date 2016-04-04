using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
    private static GameManagerScript manager = null;
    public GameObject player, combatPlayer, combatController, background;
    private BackgroundControllerScript bgController;
    private PlayerControllerScript playerController;
    private CombatControllerScript combatPlayerController;
    private WordGeneratorScript wordGenerator;
    public Camera cam, combatCam;
	
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
        cam.enabled = true;
        combatCam.enabled = false;

        playerController = player.GetComponent<PlayerControllerScript>();
        combatPlayerController = combatPlayer.GetComponent<CombatControllerScript>();
        wordGenerator = combatController.GetComponent<WordGeneratorScript>();
        bgController = background.GetComponent<BackgroundControllerScript>();

        bgController.Activate();
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
        DontDestroyOnLoad(this.gameObject);
    }

    public void EnterCombat () {
        ChangeCamera();
        print("enter");
        playerController.ToggleCombat();
        combatPlayerController.ToggleCombat();
        wordGenerator.Activate();
        bgController.Deactivate();
    }

    public void ExitCombat()
    {
        print("exit");
        ChangeCamera();
        playerController.ToggleCombat();
        combatPlayerController.ToggleCombat();
        bgController.Activate();
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");

        player.GetComponent<Rigidbody2D>().position = spawns[0].transform.position;
    }

    public void ChangeCamera ()
    {
        print("cam change");
        cam.enabled = !cam.enabled;
        combatCam.enabled = !combatCam.enabled;
    }
}

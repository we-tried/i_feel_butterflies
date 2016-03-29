using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
    private static GameManagerScript manager = null;
    public GameObject player, combatPlayer, combatController;
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
        cam.enabled = true;
        combatCam.enabled = false;

        playerController = player.GetComponent<PlayerControllerScript>();
        combatPlayerController = combatPlayer.GetComponent<CombatControllerScript>();
        wordGenerator = combatController.GetComponent<WordGeneratorScript>();
	}

    void CombatTrigger ()
    {
    }

    void GetThisGameManager()
    {
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            manager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleCombat () {
        ChangeCamera();
        playerController.ToggleCombat();
        combatPlayerController.ToggleCombat();
        wordGenerator.Activate();   
    }

    public void ChangeCamera ()
    {
        cam.enabled = !cam.enabled;
        combatCam.enabled = !combatCam.enabled;
    }
}

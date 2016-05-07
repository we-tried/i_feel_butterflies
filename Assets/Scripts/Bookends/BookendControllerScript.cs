using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BookendControllerScript : MonoBehaviour {

	public GameManagerScript gms;
	public GameObject credits;
	public Text beginning, ending, extra1, extra2, theEnd, keyPress;
	public Image background;
	public int sceneCounter = 1;
    bool activated = true;
	private bool complete = false;
	public bool altMode;

	// Use this for initialization
	void Start () {
		sceneHandler ();
		credits.SetActive (false);
	}

	public IEnumerator ToggleActivated(float waitTime)
    {
        activated = !activated;
		yield return new WaitForSeconds (waitTime);
		gms.BookendCamChange ();
	}
	public void AddScene()
	{
		sceneCounter += 1;
                sceneHandler ();
	}
    // Update is called once per frame
    void Update () {
		if (activated) {
			if (Input.anyKeyDown) {
				AddScene();
				
			}
		}
	}

	void sceneHandler (){
		if (altMode) {
			if (sceneCounter == 1) {
				background.enabled = true;
				beginning.enabled = true;
				keyPress.enabled = true;
				keyPress.text = "continue [any key]";
			} else if (sceneCounter == 2) {
				StartCoroutine (FadeOutText (1, beginning, 0f));
				StartCoroutine (FadeInText (2, extra1, 1f));
				keyPress.text = "begin [any key]";
			} else if (sceneCounter == 3) {
				StartCoroutine (FadeOutImage (2, background, .5f));
				StartCoroutine (FadeOutText (1, extra1, 0));
				StartCoroutine (ToggleActivated (2.5f));
				keyPress.enabled = false;
			} else if (sceneCounter == 4) {
				StartCoroutine(ToggleActivated (0));
				keyPress.enabled = true;
				StartCoroutine (FadeInImage (2, background, 0f));
				StartCoroutine (FadeInText (2, ending, 1f));
				StartCoroutine (FadeInText (2, keyPress, 0f));
				keyPress.text = "continue [any key]";


			} else if (sceneCounter == 5) {
				StartCoroutine (FadeOutText (1, ending, 0f));
				StartCoroutine (FadeInText (2, extra2, 1f));
				keyPress.text = "continue [any key]";

			} else if (sceneCounter == 6) {
				StartCoroutine (FadeOutText (1, beginning, 0f));
				StartCoroutine (FadeInText (2, theEnd, 1f));
				keyPress.text = "credits [any key]";
			}
			else if (sceneCounter == 7) {
				theEnd.gameObject.SetActive (false);
				credits.SetActive (true);
				keyPress.text = "main menu [any key]";
			}

			else if (sceneCounter == 8) {
				SceneManager.LoadScene ("Menu");
			}
		} else {
			if (sceneCounter == 1) {
				background.enabled = true;
				beginning.enabled = true;
				keyPress.enabled = true;
				keyPress.text = "begin [any key]";
			} else if (sceneCounter == 2) {
				StartCoroutine (FadeOutImage (2, background, .5f));
				StartCoroutine (FadeOutText (2, beginning, .5f));
				StartCoroutine (ToggleActivated (2.5f));
				keyPress.enabled = false;
			} else if (sceneCounter == 3) {
				StartCoroutine(ToggleActivated (1));
				keyPress.enabled = true;
				StartCoroutine (FadeInImage (2, background, 0f));
				StartCoroutine (FadeInText (2, ending, 1f));
				StartCoroutine (FadeInText (2, keyPress, 0f));
				keyPress.text = "continue [any key]";

			} else if (sceneCounter == 4) {
				StartCoroutine (FadeOutText (1, ending, 0f));
				StartCoroutine (FadeInText (2, theEnd, 1f));
				keyPress.text = "credits [any key]";

			} else if (sceneCounter == 5) {
				theEnd.gameObject.SetActive (false);
				credits.SetActive (true);
				keyPress.text = "main menu [any key]";
			}

			else if (sceneCounter == 6) {
				SceneManager.LoadScene ("Menu");
			}
		}

	}

	public IEnumerator FadeInImage(float time, Image image, float waitTime)
	{
		image.enabled = true;

		float target = image.color.a;
		image.color = new Color (image.color.r, image.color.g, image.color.b, 0);

		yield return new WaitForSeconds (waitTime);

		while (image.color.a < target) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime / time));
			yield return null;
		}
	}

	public IEnumerator FadeOutImage(float time, Image image, float waitTime)
	{
		float target = 0;

		yield return new WaitForSeconds (waitTime);

		while (image.color.a > target) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / time));
			yield return null;
		}

		image.enabled = false;
		complete = true;
	}


	public IEnumerator FadeInText(float time, Text text, float waitTime)
	{
		text.enabled = true;

		float target = text.color.a;
		text.color = new Color (text.color.r, text.color.g, text.color.b, 0);

		yield return new WaitForSeconds (waitTime);

		while (text.color.a < target) {
			text.color = new Color (text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / time));
			yield return null;
		}
	}

	public IEnumerator FadeOutText(float time, Text text, float waitTime)
	{
		float target = 0;

		yield return new WaitForSeconds (waitTime);

		while (text.color.a > target) {
			text.color = new Color (text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
			yield return null;
		}

		text.enabled = false;
	}

}

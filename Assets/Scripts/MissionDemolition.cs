using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum GameMode
{
	idle,
	playing,
	levelEnd
}
public class MissionDemolition : Projectile
{
	static public MissionDemolition S; // a Singleton
									   // fields set in the Unity Inspector pane
	public GameObject[] castles; // An array of the castles
	public GameObject goal;

	public Text gtLevel; // The GT_Level GUIText
	public float mass;
	public int high;
	public Text gtScore; // The GT_Score GUIText
	public Text HighScore;
	public Vector3 castlePos; // The place to put castles
	public bool _____________________________;
	// fields set dynamically
	public int level; // The current level
	public int levelMax; // The number of levels
	public int shotsTaken;
	public GameObject castle; // The current castle
	public GameMode mode = GameMode.idle;
	public string showing = "Slingshot"; // FollowCam mode

	private void Awake()
	{

		if (PlayerPrefs.HasKey("MDHighScore"))
		{
			high = PlayerPrefs.GetInt("MDHighScore");
		}
		PlayerPrefs.SetInt("MDHighScore", high);
	}
	void Start()
	{

		shotsTaken = 0;

		S = this; // Define the Singleton
		level = 0;
		levelMax = castles.Length;
		StartLevel();
		HighScore.text = "HighScore: " + high;
	}
	void StartLevel()
	{
		// Get rid of the old castle if one exists
		if (castle != null)
		{
			Destroy(castle);
		}
		// Destroy old projectiles if they exist
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
		foreach (GameObject pTemp in gos)
		{
			Destroy(pTemp);
		}
		// Instantiate the new castle
		castle = Instantiate(castles[level]) as GameObject;
		castlePos = new Vector3(Random.Range(700, 800), -9, 0);
		Vector3 vect = castlePos;
		vect.x += Random.Range(1, 10);
		goal.transform.position = vect;
		castle.transform.position = castlePos;
		shotsTaken += 100;
		// Reset the camera
		SwitchView("Both");
		ProjectileLine.S.Clear();
		// Reset the goal
		Goal.goalMet = false;
		ShowGT();
		mode = GameMode.playing;
	}
	void ShowGT()
	{
		// Show the data in the GUITexts
		gtLevel.text = "Level: " + (level + 1) + " of " + levelMax;
		gtScore.text = "Score: " + shotsTaken;
	}
	void Update()
	{
		ShowGT();
		// Check for level end
		if (mode == GameMode.playing && Goal.goalMet)
		{
			// Change mode to stop checking for level end
			mode = GameMode.levelEnd;
			// Zoom out
			SwitchView("Both");
			// Start the next level in 2 seconds
			Invoke("NextLevel", 2f);
		}
		mass = Projectile.M;


		if (Slingshot.aimingMode == true)
		{
			SwitchView("Both");
		}



	}
		void NextLevel()
	{
		level++;
		if (level == levelMax)
		{
			PlayerPrefs.SetInt("MDScore", shotsTaken);

			if (shotsTaken > high)
			{

				high = shotsTaken;
				HighScore.text = "HighScore: " + high.ToString();
			}

			if (high > PlayerPrefs.GetInt("MDHighScore"))
			{
				PlayerPrefs.SetInt("MDHighScore", high);
			}

			Application.LoadLevel("End");
		}
		StartLevel();
	}
	void OnGUI()
	{
		// Draw the GUI button for view switching at the top of the screen
		Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);

		switch (showing)
		{
			case "Slingshot":
				if (GUI.Button(buttonRect, "Show Castle"))
				{
					SwitchView("Castle");
				}
				break;
			case "Castle":
				if (GUI.Button(buttonRect, "Show Both"))
				{
					SwitchView("Both");
				}
				break;
			case "Both":
				if (GUI.Button(buttonRect, "Show Slingshot"))
				{
					SwitchView("Slingshot");
				}
				break;

		}
	}
	// Static method that allows code anywhere to request a view change
	static public void SwitchView(string eView)
	{
		S.showing = eView;
		switch (S.showing)
		{
			case "Slingshot":
				FollowCam.S.poi = null;
				break;

			case "Castle":
				FollowCam.S.poi = S.castle;
				break;

			case "Both":
				FollowCam.S.poi = GameObject.Find("ViewBoth");
				break;
		}
	}
	// Static method that allows code anywhere to increment shotsTaken
	public static void ShotFired()
	{

		if (Pro.mass == 10)
		{
			S.shotsTaken -= 10;
		}
		if (Pro.mass == 5)
		{
			S.shotsTaken -= 5;
		}
		if (Pro.mass == 1)
		{
			S.shotsTaken -= 1;
		}

	}
}
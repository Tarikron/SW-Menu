using UnityEngine;
using System.Collections;

public class TimeScale : MonoBehaviour {

	static public float timeScale = 1.0f;
	static public bool paused = false;
	static public bool allowPauseKey = true;

	// For the inspector
	public float timeScaleChange = 1.0f;
	public bool pausedChange = false;
	public enum PauseKey
	{
		none, P, space, both, axisPause 
	}
	public PauseKey pausekey = PauseKey.P;
	bool oldPaused = false;

	public bool scaleGravity = true;
	private Vector3 startGravity;
	private Vector2 start2DGravity;

	public bool scaleFlareFade = true;
	private float startFlareFadeSpeed = 0f;

	void Awake()
	{
		timeScale = 1f;
		paused = false;
		allowPauseKey = true;
	}

	void Start()
	{
		startGravity = Physics.gravity;
		start2DGravity = Physics2D.gravity;
		startFlareFadeSpeed = RenderSettings.flareFadeSpeed;
		oldPhysicsTimeScale = timeScale;
		oldFlareTimeScale = timeScale;
	}

	float oldPhysicsTimeScale = 0f;
	float oldFlareTimeScale = 0f;

	void LateUpdate ()
	{

		if((Input.GetKeyDown(KeyCode.P) 	&& (pausekey == PauseKey.P 	   || pausekey == PauseKey.both))
		|| (Input.GetKeyDown(KeyCode.Space) && (pausekey == PauseKey.space || pausekey == PauseKey.both))
		|| ((Input.GetButtonDown("Pause") == true || Input.GetButtonDown("ControllerPause") == true) &&  pausekey == PauseKey.axisPause)
		   && TimeScale.allowPauseKey == true){

			if(pausedChange)
				pausedChange = false;
			else
				pausedChange = true;
		}

		if(paused == oldPaused){
			paused = pausedChange;
			oldPaused = paused;
		}
		else{
			pausedChange = paused;
			oldPaused = paused;
		}

		if(paused)
			timeScale = 0.0f;
		else
			timeScale = timeScaleChange;

		if(scaleGravity == true&& oldPhysicsTimeScale != timeScale)
		{
			oldPhysicsTimeScale = timeScale;
			Physics.gravity = startGravity * timeScale;
			Physics2D.gravity = start2DGravity * timeScale;
		}

		if(scaleFlareFade == true && oldFlareTimeScale != timeScale)
		{
			oldFlareTimeScale = timeScale;
			RenderSettings.flareFadeSpeed = startFlareFadeSpeed * timeScale;
		}
	}
}

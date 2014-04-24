using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour {
	public AudioClip[] runSound;
	public AudioClip dodgeSound;
	public AudioClip attackSound;
	public AudioClip parrySound;
	public AudioClip livingAmbiance;
	public AudioClip ghostAmbiance;
	
	private AudioSource feetSource;
	private AudioSource livingAmbSource;
	public AudioSource LivingAmbSource { 
		set { livingAmbSource = value; }
		get { return livingAmbSource; }
	}
	private AudioSource ghostAmbSource;
	public AudioSource GhostAmbSource { 
		set { ghostAmbSource = value; }
		get { return ghostAmbSource; }
	}
	
	// Use this for initialization
	void Start() {
		feetSource = gameObject.AddComponent<AudioSource>();

		livingAmbSource = gameObject.AddComponent<AudioSource>();
		livingAmbSource.playOnAwake = false;
		livingAmbSource.clip = livingAmbiance;

		ghostAmbSource = gameObject.AddComponent<AudioSource>();
		ghostAmbSource.playOnAwake = false;
		ghostAmbSource.clip = ghostAmbiance;
	}
	
	public void running(bool moving) {
		if (feetSource.isPlaying)
			return;
		
		feetSource.clip = runSound [Mathf.FloorToInt(Random.Range(0, runSound.Length))];
		
		if (moving) {
			feetSource.Play();
		} else {
			feetSource.Stop();
		}
	}
	
	public void dodge() {
		feetSource.Stop();
		feetSource.clip = dodgeSound;
		feetSource.Play();
//		
//		// Wait for dodgesound to be done
//		yield return new WaitForSeconds(feetSource.clip.length);
//		
//		feetSource.clip = runSound;
	}
	
	public void attack() {
		feetSource.Stop();
		feetSource.clip = attackSound;
		feetSource.PlayDelayed(1);
	}
	
	public void parry() {
		feetSource.Stop();
		feetSource.clip = parrySound;
		feetSource.Play();
	}
}

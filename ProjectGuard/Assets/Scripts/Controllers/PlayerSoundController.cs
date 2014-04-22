using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour
{
	public AudioClip runSound;
	public float runSoundSpeed;
	public AudioClip dodgeSound;
	public float dodgeSoundSpeed;
	public AudioClip attackSound;
	public float attackSoundSpeed;
	public AudioClip parrySound;
	public float parrySoundSpeed;
	
	private AudioSource feetSource;
	
	// Use this for initialization
	void Start()
	{
		feetSource = GetComponent<AudioSource>();
		feetSource.clip = runSound;
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
	
	public void running(bool moving)
	{
		if (feetSource.isPlaying)
			return;
		
		if (feetSource.clip != runSound)
			feetSource.clip = runSound;
		
		if (moving)
		{
			feetSource.Play();
		} else
		{
			feetSource.Stop();
		}
	}
	
	public void dodge()
	{
		feetSource.Stop();
		feetSource.clip = dodgeSound;
		feetSource.Play();
//		
//		// Wait for dodgesound to be done
//		yield return new WaitForSeconds(feetSource.clip.length);
//		
//		feetSource.clip = runSound;
	}
	
	public void attack()
	{
		
	}
	
	public void parry()
	{
		
	}
}

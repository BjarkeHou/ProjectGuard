using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour
{
	public AudioClip[] runSound;
	public AudioClip dodgeSound;
	public AudioClip attackSound;
	public AudioClip parrySound;
	
	private AudioSource feetSource;
	
	// Use this for initialization
	void Start()
	{
		feetSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
	
	public void running(bool moving)
	{
		if (feetSource.isPlaying)
			return;
		
		feetSource.clip = runSound [Mathf.FloorToInt(Random.Range(0, runSound.Length))];
		
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
		feetSource.Stop();
		feetSource.clip = attackSound;
		feetSource.PlayDelayed(1);
	}
	
	public void parry()
	{
		feetSource.Stop();
		feetSource.clip = parrySound;
		feetSource.Play();
	}
}

using UnityEngine;
using System.Collections;

public enum EnemyType
{
	MirrorImage,
	StoneElemental,
	WiiWii
}

public class EnemySoundController : MonoBehaviour
{

	public AudioClip runSound;
	public float runSoundSpeed;
	public AudioClip dodgeSound;
	public float dodgeSoundSpeed;
	public AudioClip attackSound;
	public float attackSoundSpeed;
	public AudioClip parrySound;
	public float parrySoundSpeed;
	
	private EnemyType _type;
	// Use this for initialization
	void Start()
	{
		switch (this.transform.parent.gameObject.name)
		{
			case "StoneElemental":
				_type = EnemyType.StoneElemental;
				break;
			case "MirrorImage":
				_type = EnemyType.MirrorImage;
				break;
			case "WiiWii":
				_type = EnemyType.WiiWii;
				break;
			default:
				break;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}

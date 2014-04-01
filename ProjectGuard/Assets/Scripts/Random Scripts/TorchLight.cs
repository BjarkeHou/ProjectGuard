using UnityEngine;
using System.Collections;

public class TorchLight : MonoBehaviour {

	private Light torch;
	private Light shadowCaster;

	private Color originalCol;
	private Color targetCol;
	private Vector3 originalPos;
	private Vector3 targetPos;
	private float originalIntensity;
	private float targetIntensity;

	public Vector3 colorRange;
	public Vector3 positionRange;
	public float intensityRange;

	public float maxShiftDelay;
	public float lerpSpeed;
	private float timer;

	// Use this for initialization
	void Start() {
		torch = transform.Find("Flame").GetComponent<Light>();
		shadowCaster = transform.Find("ShadowCaster").GetComponent<Light>();

		originalCol = torch.color;
		targetCol = torch.color;

		originalPos = shadowCaster.transform.position;
		targetPos = shadowCaster.transform.position;

		originalIntensity = torch.intensity;
		targetIntensity = torch.intensity;

		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update() {
		if (Time.time > timer) {
			Vector3 colVar = new Vector3 (Random.Range(-colorRange.x, colorRange.x), Random.Range(-colorRange.y, colorRange.y), Random.Range(-colorRange.z, colorRange.z));
			targetCol = new Color (originalCol.r + colVar.x, originalCol.g + colVar.y, originalCol.b + colVar.z);

			Vector3 posVar = new Vector3 (Random.Range(-positionRange.x, positionRange.x), Random.Range(-positionRange.y, positionRange.y), Random.Range(-positionRange.z, positionRange.z)); 
			targetPos = new Vector3 (originalPos.x + posVar.x, originalPos.y + posVar.y, originalPos.z + posVar.z);

			targetIntensity = originalIntensity + Random.Range(0, intensityRange);

			timer = Time.time + Random.Range(0, maxShiftDelay);
		}
		torch.color = Color.Lerp(torch.color, targetCol, Time.deltaTime * lerpSpeed);

		shadowCaster.transform.position = Vector3.Lerp(shadowCaster.transform.position, targetPos, Time.deltaTime * lerpSpeed);
		torch.intensity = Mathf.Lerp(torch.intensity, targetIntensity, Time.deltaTime * lerpSpeed);
	}
}

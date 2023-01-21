using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D flickerLight;
    [SerializeField] float maxIntensity = 2.0f;
	[SerializeField] float minIntensity = .2f;
    [SerializeField] float frequency = 1.0f;
    [SerializeField] float stepSize = .1f;
	[SerializeField] float targetIntensity = 1.0f;
	[SerializeField] float timeDifference;
	// Start is called before the first frame update
	void Start()
    {
        flickerLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        timeDifference = 1 / frequency;
	}

    // Update is called once per frame
    void Update()
    {
		timeDifference -= Time.deltaTime;
        if (timeDifference < 0) {
			targetIntensity = Random.Range(minIntensity, maxIntensity);
            timeDifference = 1 / frequency;
		}
        if (flickerLight.intensity > targetIntensity)
        {
			flickerLight.intensity -= targetIntensity * stepSize;
		} else if (flickerLight.intensity < targetIntensity)
        {
            flickerLight.intensity += targetIntensity * stepSize;
		}
    }
}

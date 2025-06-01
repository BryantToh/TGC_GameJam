using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Transition : MonoBehaviour
{
    [SerializeField] GameObject transitionLight;
    [SerializeField] GameObject playerObj;
    [SerializeField] Transform transitionPoint;
    [SerializeField] float increaseDuration;
    private Light2D lightObject;
    private float maxIntensity = 60f;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        lightObject = transitionLight.GetComponentInChildren<Light2D>();
        lightObject.intensity = 1f;
        transitionLight.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DoTransition();
            LevelManager.Instance.SetCompleteLevel(true);
        }
    }
    private void DoTransition()
    {
        transitionLight.SetActive(true);
        StartCoroutine(LightUp());
    }
    private IEnumerator LightUp()
    {
        float elapsed = 0f;
        float startIntensity = lightObject.intensity;

        while (elapsed < increaseDuration)
        {
            elapsed += Time.deltaTime;
            lightObject.intensity = Mathf.Lerp(startIntensity, maxIntensity, elapsed / increaseDuration);
            yield return null;
        }
        lightObject.intensity = maxIntensity;
        playerObj.transform.position = transitionPoint.transform.position;
        yield return new WaitForSeconds(2f);
        StartCoroutine(LightDown());
    }
    private IEnumerator LightDown()
    {
        float elapsed = 0f;
        float endIntensity = lightObject.intensity;
        
        while (elapsed < increaseDuration)
        {
            elapsed += Time.deltaTime;
            lightObject.intensity = Mathf.Lerp(endIntensity, 1f, elapsed / increaseDuration);
            yield return null;
        }
        transitionLight.SetActive(false);
    }
}

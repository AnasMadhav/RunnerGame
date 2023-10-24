using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material morningSkybox;
    public Material noonSkybox;
    public Material eveningSkybox;
    public Material nightSkybox;
    [Range(0,4)]
    public float currentTime;
    // Adjust the range of this variable for smooth blending.
    [Range(0, 1)]
    public float blendFactor;
    public float morningTime, noonTime, eveningTime, nightTime;

    private float blendDuration = 5.0f; // Duration of the blend in seconds
    private float blendStartTime;
    private float targetBlendFactor;
    private Material currentSkyboxMaterial;



    private void Start()
    {
        currentSkyboxMaterial = morningSkybox;
        RenderSettings.skybox = currentSkyboxMaterial;
    }
    void Update()
    {
       

        Material targetSkybox;

        if (currentTime >= morningTime && currentTime < noonTime)
        {
            targetSkybox = morningSkybox;
            ChangeSkybox(targetSkybox, 0);
        }
        else if (currentTime >= noonTime && currentTime < eveningTime)
        {
            targetSkybox = noonSkybox;
            ChangeSkybox(targetSkybox, 0);
        }
        else if (currentTime >= eveningTime && currentTime < nightTime)
        {
            targetSkybox = eveningSkybox;
            ChangeSkybox(targetSkybox, 0);
        }
        else
        {
            targetSkybox = nightSkybox;
            ChangeSkybox(targetSkybox, 0);
        }
        /*Debug.Log("Target Skybox: " + targetSkybox.name);
        float newBlendFactor = Mathf.Lerp(RenderSettings.skybox.GetFloat("_BlendFactor"),1f, .1f);
        //RenderSettings.skybox.Lerp(RenderSettings.skybox, targetSkybox, 0.5f * Time.deltaTime);
        RenderSettings.skybox.SetFloat("_BlendFactor", newBlendFactor);
        // Debug.Log("Blend Factor: " + blendFactor);*/
        if (Time.time - blendStartTime < blendDuration)
        {
            // Calculate the blend factor based on the elapsed time.
            float elapsed = Time.time - blendStartTime;
            float t = Mathf.Clamp01(elapsed / blendDuration);
            float newBlendFactor = Mathf.Lerp(currentSkyboxMaterial.GetFloat("_BlendFactor"), targetBlendFactor, t);
            currentSkyboxMaterial.SetFloat("_BlendFactor", newBlendFactor);
        }
        
    }
    public void ChangeSkybox(Material newSkyboxMaterial, float newBlendFactor)
    {
        blendStartTime = Time.time;
        targetBlendFactor = newBlendFactor;

        // Set the new material.
        currentSkyboxMaterial = newSkyboxMaterial;
        RenderSettings.skybox = currentSkyboxMaterial;
    }
}

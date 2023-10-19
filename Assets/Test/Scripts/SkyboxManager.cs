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

    void Update()
    {
       

        Material targetSkybox;

        if (currentTime >= morningTime && currentTime < noonTime)
        {
            targetSkybox = morningSkybox;
        }
        else if (currentTime >= noonTime && currentTime < eveningTime)
        {
            targetSkybox = noonSkybox;
        }
        else if (currentTime >= eveningTime && currentTime < nightTime)
        {
            targetSkybox = eveningSkybox;
        }
        else
        {
            targetSkybox = nightSkybox;
        }

        // Lerp between the current skybox and the target skybox based on the blend factor.
        RenderSettings.skybox.Lerp(RenderSettings.skybox, targetSkybox, blendFactor);
        //RenderSettings.skybox = targetSkybox;
    }
}

using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material morningSkybox;
    public Material noonSkybox;
    public Material eveningSkybox;
    public Material nightSkybox;

    // Adjust the range of this variable for smooth blending.
    [Range(0, 1)]
    public float blendFactor;
    public float morningTime, noonTime, eveningTime, nightTime;

    void Update()
    {
        float currentTime = 0;// Get the current time from your day-night cycle system.

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
    }
}

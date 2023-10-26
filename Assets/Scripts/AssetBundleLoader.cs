using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class AssetBundleLoader : MonoBehaviour
{
    public Slider loadingBar;
    public TextMeshProUGUI progressText;

    public string googleDriveFileID; // Replace with your Google Drive file ID
    public string assetName;         // Name of the asset you want to load
    public string assetBundleName;   // Name of the saved AssetBundle file

    void Start()
    {
        // Check if the AssetBundle exists in local storage
        if (AssetBundleExistsLocally())
        {
            LoadAssetBundleFromLocal();
        }
        else
        {
            StartCoroutine(LoadAsset());
        }
    }

    IEnumerator LoadAsset()
    {
        string url = "https://drive.google.com/uc?export=download&id=" + googleDriveFileID;

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error while loading AssetBundle: " + www.error);
                yield break;
            }

            AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(www);

            if (assetBundle == null)
            {
                Debug.LogError("Failed to load AssetBundle.");
                yield break;
            }

            // Calculate loading progress
            float loadingProgress = 1.0f;
            UpdateLoadingProgress(loadingProgress);

            // Save the AssetBundle to local storage
            SaveAssetBundleToLocal(assetBundle);

            // Proceed to the next scene
            // SceneManager.LoadScene("YourNextScene");
        }
    }

    bool AssetBundleExistsLocally()
    {
        // Check if the AssetBundle file exists in the persistent data path
        string pathToCheck = Path.Combine(Application.persistentDataPath, assetBundleName);
        return File.Exists(pathToCheck);
    }

    void LoadAssetBundleFromLocal()
    {
        // Load the AssetBundle from local storage
        string pathToLoad = Path.Combine(Application.persistentDataPath, assetBundleName);

        if (File.Exists(pathToLoad))
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(pathToLoad);

            if (assetBundle != null)
            {
                // Load assets or perform other operations with the AssetBundle
            }
            else
            {
                Debug.LogError("Failed to load AssetBundle from local storage.");
            }
        }
        else
        {
            Debug.LogError("AssetBundle file does not exist in local storage.");
        }
    }

    void SaveAssetBundleToLocal(AssetBundle assetBundle)
    {
        // Specify a path for saving the AssetBundle
        string pathToSave = Path.Combine(Application.persistentDataPath, assetBundleName);

        // Save the downloaded bytes directly to a file
        byte[] assetBundleData = File.ReadAllBytes(pathToSave);
        File.WriteAllBytes(pathToSave, assetBundleData);

        Debug.Log("AssetBundle saved to: " + pathToSave);
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateLoadingProgress(float progress)
    {
        loadingBar.value = progress;
        progressText.text = (progress * 100).ToString("F0") + "%";
    }
}

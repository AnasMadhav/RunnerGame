using UnityEngine;
using System.IO;

public class AssetBundleLoaderFromLocal : MonoBehaviour
{
    public string assetBundleName; // Name of the locally stored AssetBundle file
    public string assetName;      // Name of the asset you want to load from the AssetBundle

    void Start()
    {
        LoadAssetBundle();
    }

    void LoadAssetBundle()
    {
        // Specify the path to the locally stored AssetBundle
        string pathToLoad = Path.Combine(Application.persistentDataPath, assetBundleName);

        if (File.Exists(pathToLoad))
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(pathToLoad);

            if (assetBundle != null)
            {
                // Load assets or perform other operations with the AssetBundle
                GameObject loadedObject = assetBundle.LoadAsset<GameObject>(assetName);

                if (loadedObject != null)
                {
                    // Instantiate the loaded asset
                    Instantiate(loadedObject);
                }
                else
                {
                    Debug.LogError("Failed to load asset from AssetBundle.");
                }
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
}

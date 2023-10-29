using UnityEngine;
using UnityEditor;

public class DefaultTextureSizer : AssetPostprocessor
{

    void OnPreprocessTexture()
    {
        TextureImporter tex_importer = assetImporter as TextureImporter;
        tex_importer.maxTextureSize = 512; //Some other max size than the default
    }

}
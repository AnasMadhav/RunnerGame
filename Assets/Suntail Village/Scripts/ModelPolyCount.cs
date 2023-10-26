/*using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class ModelPolyCount : MonoBehaviour
{
    [MenuItem("Tools/Count Polygons")]
    public static void CountPolygons()
    {
        // Find all GameObjects with MeshRenderer components in the scene
        MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>();

        // Create a dictionary to store the polygon counts for each model
        Dictionary<string, int> modelPolyCounts = new Dictionary<string, int>();

        // Iterate through the mesh renderers and calculate polygon counts
        foreach (MeshRenderer renderer in meshRenderers)
        {
            string modelName = renderer.transform.name; // Use the model's name as the key
            int polyCount = 0;

            // Get the mesh and calculate the polygon count
            if (renderer.gameObject.TryGetComponent<MeshFilter>(out MeshFilter meshFilter))
            {
                Mesh mesh = meshFilter.sharedMesh;
                polyCount = mesh.triangles.Length / 3; // Each triangle has 3 vertices
            }

            // Add or update the polygon count in the dictionary
            if (modelPolyCounts.ContainsKey(modelName))
            {
                modelPolyCounts[modelName] += polyCount;
            }
            else
            {
                modelPolyCounts[modelName] = polyCount;
            }
        }

        // Write the polygon counts to a text file
        string filePath = Application.dataPath + "/PolygonCounts.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var kvp in modelPolyCounts)
            {
                writer.WriteLine("Model: " + kvp.Key + ", Polygon Count: " + kvp.Value);
            }
        }

        Debug.Log("Polygon counts have been written to " + filePath);
    }
}
*/
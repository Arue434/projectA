using UnityEngine;

[System.Serializable]
public class SceneReference
{
    [SerializeField] private Object sceneAsset;
    [SerializeField] private string scenePath;

    public string ScenePath => scenePath;

    public void OnValidate()
    {
        if (sceneAsset != null)
        {
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(sceneAsset);
            scenePath = System.IO.Path.GetFileNameWithoutExtension(assetPath);
        }
        else
        {
            scenePath = string.Empty;
        }
    }
}

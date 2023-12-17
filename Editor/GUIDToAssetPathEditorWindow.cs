using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class AssetFinderEditorWindow : EditorWindow
    {
        private string assetGuid = string.Empty;
        private string assetPath = string.Empty;

        [MenuItem("Tools/Find Asset By GUID")]
        private static void InitializeWindow()
        {
            const float windowWidth = 400, windowHeight = 120;
            GetWindowWithRect<AssetFinderEditorWindow>(new Rect(0, 0, windowWidth, windowHeight));
        }

        private void OnGUI()
        {
            CreateInputArea();
            CreateActionButtonRow("Find Asset Path", () => assetPath = RetrieveAssetPath(assetGuid), 120);
            CreateActionButtonRow("Cancel", Close, 120);
            DisplayAssetPath();
        }

        private void CreateInputArea()
        {
            GUILayout.Label("Enter Asset GUID:");
            assetGuid = GUILayout.TextField(assetGuid);
        }

        private void CreateActionButtonRow(string buttonText, System.Action action, float buttonWidth)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button(buttonText, GUILayout.Width(buttonWidth)))
                action.Invoke();
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DisplayAssetPath()
        {
            GUILayout.Label(string.IsNullOrEmpty(assetPath) ? "No path retrieved" : assetPath);
        }

        private static string RetrieveAssetPath(string guid)
        {
            var foundPath = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(foundPath)) 
            {
                Debug.LogWarning("No asset found for the provided GUID.");
                return "Asset not found";
            }
            
            Debug.Log($"Asset Path: {foundPath}");
            return foundPath;
        }
    }
}
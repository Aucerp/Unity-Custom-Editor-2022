using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class Test : EditorWindow
{
    [MenuItem("Testing/Test Window")]
    public static void ShowWindow()
    {
        Test window = GetWindow<Test>();
        window.titleContent = new GUIContent("Test Window");
        window.minSize = new Vector2(200,500);
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/test.uxml");
        root.Add(visualTree.Instantiate());

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/test.uss");
        root.styleSheets.Add(styleSheet);
    }
}

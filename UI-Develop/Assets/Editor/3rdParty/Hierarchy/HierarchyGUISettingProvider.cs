using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

public class HierarchyGUISettingProvider : SettingsProvider
{
    private const string SettingPath = "Preferences/_CustomEditor/HierarchyGUISetting";

    private Editor editor;

    [SettingsProvider]
    public static SettingsProvider CreateProvider()
    {
        return new HierarchyGUISettingProvider(SettingPath, SettingsScope.User, null);
    }

    public HierarchyGUISettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords)
    {

    }

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        var preferences = HierarchyGUISetting.instance;
        preferences.hideFlags = UnityEngine.HideFlags.HideAndDontSave & ~UnityEngine.HideFlags.NotEditable;
        Editor.CreateCachedEditor(preferences, null, ref editor);
    }


    public override void OnGUI(string searchContext)
    {
        EditorGUI.BeginChangeCheck();
        // 設定ファイルの標準のインスペクターを表示
        editor.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            EditorApplication.RepaintHierarchyWindow();
            // 差分があったら保存
            HierarchyGUISetting.instance.Save();
        }
    }
}

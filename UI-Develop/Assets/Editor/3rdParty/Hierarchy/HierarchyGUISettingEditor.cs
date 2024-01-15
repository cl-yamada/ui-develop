using UnityEngine;
using UnityEditor;

public class HierarchyGUISettingEditor : EditorWindow
{
    private static HierarchyGUISetting setting;

    private static Color defaultColor;

    // ウィンドウの表示を開始するためのメニュー項目を追加します
    [MenuItem("Edit/Clover Lab/HierarchyGUISetting")]
    public static void ShowWindow()
    {
        // ウィンドウを表示します（"My Editor Window"はウィンドウのタイトルです）
        GetWindow<HierarchyGUISettingEditor>("Heirarchy GUI Setting");
    }

    private void OnEnable()
    {
        setting = HierarchyGUISetting.instance.LoadSettings();
    }

    // エディターウィンドウのUIを描画するためのメソッドです
    private void OnGUI()
    {
        if (setting == null)
        {
            EditorGUILayout.HelpBox("ScriptableObject is not loaded.", MessageType.Warning);
            return;
        }

        defaultColor = GUI.backgroundColor;

        DesignLabelCenter("HIERARCHY GUI SETTING", bgColor: Color.cyan);

        // エディターウィンドウのUI要素をここに追加します
        DesignLabel(text: "アクティブフラグ", bgColor: Color.green);

        Toggle("Show check box", ref setting.useActiveToggle);

        DesignLabel(text: "コンポーネントアイコン", bgColor: Color.green);

        Toggle("Show icon", ref setting.useDrawComponents);

        DesignLabel(text: "Image参照アイコン", bgColor: Color.green);

        Toggle("Show mini image", ref setting.useDrawImageReference);

        // 変更があった場合にアセットを更新するために使用します。
        if (GUI.changed)
        {
            EditorApplication.RepaintHierarchyWindow();
            HierarchyGUISetting.instance.SaveSettings(setting);
            EditorUtility.SetDirty(setting);
        }
    }

    /// <summary>
    /// トグル
    /// </summary>
    /// <param name="label"></param>
    /// <param name="value"></param>
    private void Toggle(string label, ref bool value)
    {
        value = EditorGUILayout.Toggle(label, value);
    }

    /// <summary>
    /// ラベル
    /// </summary>
    /// <param name="text"></param>
    /// <param name="bgColor"></param>
    private void DesignLabel(string text, Color bgColor)
    {
        GUI.backgroundColor = bgColor;
        using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            GUILayout.Label(text);
        }
        GUI.backgroundColor = defaultColor;
    }
    private void DesignLabelCenter(string text, Color bgColor)
    {
        GUI.backgroundColor = bgColor;
        using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            GUIStyle centeredStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            GUILayout.Label(text, centeredStyle);
        }
        GUI.backgroundColor = defaultColor;
    }

    private void CheatSheet()
    {
        // 中央揃えのスタイルを作成
        GUIStyle centeredStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

        // 左寄せのスタイルを作成
        GUIStyle leftStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft };

        // 右寄せのスタイルを作成
        GUIStyle rightStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight };

        // ラベルを中央揃えで表示
        GUILayout.Label("中央揃えラベル", centeredStyle);

        // ラベルを左寄せで表示
        GUILayout.Label("左寄せラベル", leftStyle);

        // ラベルを右寄せで表示
        GUILayout.Label("右寄せラベル", rightStyle);
    }
}
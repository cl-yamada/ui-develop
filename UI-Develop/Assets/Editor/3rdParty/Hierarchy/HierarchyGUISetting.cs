using UnityEngine;
using UnityEditor;

//[FilePath("HierarchyGUISetting.asset", FilePathAttribute.Location.PreferencesFolder)]
public class HierarchyGUISetting : ScriptableSingleton<HierarchyGUISetting>
{
    // 定数としてキーを定義
    private const string KeyUseActiveToggle = "useActiveToggle";
    private const string KeyUseDrawComponents = "useDrawComponents";
    private const string KeyUseDrawImageReference = "useDrawImageReference";

    [SerializeField, Header("アクティブ切り替え")]
    public bool useActiveToggle;
    [SerializeField, Header("コンポーネントアイコン表示")]
    public bool useDrawComponents;
    [SerializeField, Header("Imageがある場合サムネイル表示")]
    public bool useDrawImageReference;

    public void Save()
    {
        Save(true);
    }

    public void SetSetting<T>(string key, T value)
    {
        EditorUserSettings.SetConfigValue(key, JsonUtility.ToJson(value));
    }
    public void SaveSettings(HierarchyGUISetting settings)
    {
        EditorUserSettings.SetConfigValue(KeyUseActiveToggle, settings.useActiveToggle.ToString());
        EditorUserSettings.SetConfigValue(KeyUseDrawComponents, settings.useDrawComponents.ToString());
        EditorUserSettings.SetConfigValue(KeyUseDrawImageReference, settings.useDrawImageReference.ToString());
    }
    public HierarchyGUISetting LoadSettings()
    {
        useActiveToggle = bool.Parse(EditorUserSettings.GetConfigValue(KeyUseActiveToggle) ?? "false");
        useDrawComponents = bool.Parse(EditorUserSettings.GetConfigValue(KeyUseDrawComponents) ?? "false");
        useDrawImageReference = bool.Parse(EditorUserSettings.GetConfigValue(KeyUseDrawImageReference) ?? "false");
        return this;
    }
}

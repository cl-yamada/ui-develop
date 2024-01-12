using UnityEngine;
using UnityEditor;

[FilePath("HierarchyGUISetting.asset", FilePathAttribute.Location.PreferencesFolder)]
public class HierarchyGUISetting : ScriptableSingleton<HierarchyGUISetting>
{
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
}

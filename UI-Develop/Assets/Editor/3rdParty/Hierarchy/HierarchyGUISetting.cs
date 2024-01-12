using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "ScriptableObject/ Create HierarchyGUISetting", fileName = "HierarchyGUISetting")]
public class HierarchyGUISetting : ScriptableObject
{
    [SerializeField, Header("アクティブ切り替え")]
    public bool useActiveToggle;
    [SerializeField, Header("コンポーネント表示")]
    public bool useDrawComponents;
    [SerializeField, Header("Imageのサムネイル表示")]
    public bool useDrawImageReference;
}

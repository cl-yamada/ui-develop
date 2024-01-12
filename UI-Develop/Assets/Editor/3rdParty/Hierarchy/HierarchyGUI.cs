using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public static class HierarchyGUI
{
    private static HierarchyGUISetting setting;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        setting = AssetDatabase.LoadAssetAtPath("Assets/Editor/3rdParty/Hierarchy/HierarchyGUISetting.asset", typeof(HierarchyGUISetting)) as HierarchyGUISetting;
        if (setting != null)
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
            //Debug.Log("++ Hi,welcome! Success start HierarchyGUI feature. ++");
        }
    }

    private const int WIDTH = 16;
    private const int OFFSET = 10;

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null)
        {
            return;
        }

        StripeHierarchy(rect: selectionRect);

        int count = 0;

        if (setting.useActiveToggle)
        {
            var rect = selectionRect;
            rect.x = rect.xMax - OFFSET - (WIDTH * count);
            rect.width = WIDTH;
            OnActiveToggle(go: go, rect: rect);
            count++;
        }

        if (setting.useDrawImageReference)
        {
            var rect = selectionRect;
            rect.x = rect.xMax - OFFSET - (WIDTH * count);
            rect.width = WIDTH;
            DrawTextureInImageComponent(go: go, rect: rect);
            count++;
        }

        if (setting.useDrawComponents)
        {
            var rect = selectionRect;
            rect.x = rect.xMax - OFFSET - (WIDTH * count);
            rect.width = WIDTH;
            DrawMiniThumbnailAtComponents(go: go, rect: rect);
        }
    }

    /// <summary>
    /// Hierarchy上にアクティブフラグのチェックボックスを表示する
    /// </summary>
    #region OnActiveToggle
    private static void OnActiveToggle(GameObject go, Rect rect)
    {
        bool active = GUI.Toggle(position: rect, value: go.activeSelf, string.Empty);
        if (active == go.activeSelf)
        {
            return;
        }

        Undo.RecordObject(go, $"{(active ? "Activate" : "Deactivate")} GameObject '{go.name}'");
        go.SetActive(active);
        EditorUtility.SetDirty(go);
    }
    #endregion

    /// <summary>
    /// Hierarchy一覧を交互に色を変えて見やすくするやつ
    /// </summary>
    #region StripeHierarchy
    private const int ROW_HEIGHT = 16;
    private const int OFFSET_Y = -4;

    private static void StripeHierarchy(Rect rect)
    {
        var index = (int)(rect.y + OFFSET_Y) / ROW_HEIGHT;

        if (index % 2 == 0)
        {
            return;
        }

        var xMax = rect.xMax;

        rect.x = 32;
        rect.xMax = xMax + 16;

        EditorGUI.DrawRect(rect, new Color(0, 0, 0, 0.1f));
    }
    #endregion

    /// <summary>
    /// Imageコンポーネントが参照しているテクスチャをサムネイル表示する
    /// </summary>
    #region DrawTextureInImageComponent
    private static void DrawTextureInImageComponent(GameObject go, Rect rect)
    {
        rect.width = WIDTH;

        if (go.TryGetComponent<Image>(out var imgComp))
        {
            if (imgComp.sprite != null)
            {
                GUI.DrawTexture(rect, imgComp.sprite.texture);
            }
            else
            {
                GUI.DrawTexture(rect, AssetPreview.GetMiniThumbnail(imgComp));
            }
        }
    }
    #endregion

    /// <summary>
    /// 全てのコンポーネントをサムネイル表示する
    /// </summary>
    #region DrawMiniThumbnailAtComponents
    private static void DrawMiniThumbnailAtComponents(GameObject go, Rect rect)
    {
        var comps = go.GetComponents<Component>();
        if (comps.Length == 0)
        {
            return;
        }

        foreach (var comp in comps)
        {
            Texture miniThum = AssetPreview.GetMiniThumbnail(comp);

            // C#スクリプトのサムネ取得
            if (miniThum == null && comp is MonoBehaviour)
            {
                var ms = MonoScript.FromMonoBehaviour(comp as MonoBehaviour);
                var path = AssetDatabase.GetAssetPath(ms);
                miniThum = AssetDatabase.GetCachedIcon(path);
            }

            if (miniThum == null)
            {
                continue;
            }

            GUI.DrawTexture(rect, miniThum);
            rect.x -= WIDTH;
        }
    }
    #endregion
}
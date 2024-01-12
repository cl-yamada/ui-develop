using UnityEngine;
using UnityEditor;

public static class ProjectGUI
{
    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.projectWindowItemOnGUI += StripeColorListView;
    }

    private const int ROW_HEIGHT = 16;
    private const int OFFSET_Y = 4;
    private const float STRIPE_ALPHA = 0.18f;

    private static void StripeColorListView(string guid, Rect selectionRect)
    {
        var index = (int)(selectionRect.y - OFFSET_Y) / ROW_HEIGHT;
        
        // 交互に
        if (index % 2 == 0)
        {
            return;
        }

        // 覆うように
        var pos = selectionRect;
        pos.x = 0;
        pos.xMax = selectionRect.xMax;

        var color = GUI.color;
        GUI.color = new Color(0, 0, 0, STRIPE_ALPHA);
        GUI.Box(pos, string.Empty);
        GUI.color = color;
    }
}

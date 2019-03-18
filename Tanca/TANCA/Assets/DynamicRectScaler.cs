using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Add this script to any rect that have a prefferde size and all else will follow! Preferably TMPro stuff. 
/// 
/// Greetings! 
/// Contact thor.sone@vobling.com if there is something weird
/// </summary>


public class DynamicRectScaler : MonoBehaviour
{
    [Header("Assign these")]
    [Tooltip("Assign a RectTransform to this value. The RectTransform must be of a component type that generates a preferred width and height. For example a TMPro component. This Rect will lead the size of the followRect")]
    [SerializeField] private RectTransform leadingRect;

    [Tooltip("Assign a RectTransform to this value. The RectTransform will follow the size and position of the leadingRect.")]
    [SerializeField] private RectTransform followRect;


    [Header("Variables")]
    [Tooltip("The maximum allowed width of the rect. Leaving this at zero will make the panel be able to expand infinitly.")]
    [SerializeField] private float maxWidth;

    [Tooltip("Horizontal and vertical padding")]
    [SerializeField] private Vector2 padding;


    [ContextMenu("UpdateRect")]
    public void UpdateRect()
    {
        if (leadingRect != null && followRect != null)
        {
            Vector2 preferredSize = new Vector2(LayoutUtility.GetPreferredWidth(leadingRect), LayoutUtility.GetPreferredHeight(leadingRect));

            if (preferredSize.x < maxWidth || maxWidth <= 0)
            {
                leadingRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredSize.x);
            }
            else leadingRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);

            leadingRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredSize.y);

            followRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, leadingRect.rect.width + padding.x);
            followRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, leadingRect.rect.height + padding.y);

            followRect.position = leadingRect.position;
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(DynamicRectScaler))]
[CanEditMultipleObjects]
public class DynamicRectScalerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DynamicRectScaler scaler = (DynamicRectScaler)target;

        scaler.UpdateRect();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Note that the scaling can be changed at runtime by calling the method called UpdateRect on this component");
    }
}
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum GradientType
{
    Top,
    Right,
    Center,
}
[AddComponentMenu("UI/Effects/TextGradient")]
public class TextGradient : BaseMeshEffect
{
    public GradientType TextType = GradientType.Top;

    public Color32 topColor = Color.white;

    public Color32 bottomColor = Color.black;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!this.IsActive())
            return;

        List<UIVertex> vertexList = new List<UIVertex>();
        vh.GetUIVertexStream(vertexList);

        ModifyVertices(vertexList);

        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexList);
    }

    public void ModifyVertices(List<UIVertex> vertexList)
    {
        if (!IsActive())
        {
            return;
        }

        int count = vertexList.Count;
        if (count > 0)
        {
            if (TextType == GradientType.Top)
            {
                float bottomY = vertexList[0].position.y;
                float topY = vertexList[0].position.y;

                for (int i = 1; i < count; i++)
                {
                    float y = vertexList[i].position.y;
                    if (y > topY)
                    {
                        topY = y;
                    }
                    else if (y < bottomY)
                    {
                        bottomY = y;
                    }
                }

                float uiElementHeight = topY - bottomY;

                for (int i = 0; i < count; i++)
                {
                    UIVertex uiVertex = vertexList[i];
                    uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.y - bottomY) / uiElementHeight);
                    vertexList[i] = uiVertex;
                }
            }
            else if (TextType == GradientType.Right)
            {
                float RightX = vertexList[0].position.x;
                float LeftX = vertexList[0].position.x;

                for (int i = 1; i < count; i++)
                {
                    float x = vertexList[i].position.x;
                    if (x > RightX)
                    {
                        RightX = x;
                    }
                    else if (x < LeftX)
                    {
                        LeftX = x;
                    }
                }

                float uiElementWeight = LeftX - RightX;

                for (int i = 0; i < count; i++)
                {
                    UIVertex uiVertex = vertexList[i];
                    uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.x - RightX) / uiElementWeight);
                    vertexList[i] = uiVertex;
                }
            }
        }
    }
}
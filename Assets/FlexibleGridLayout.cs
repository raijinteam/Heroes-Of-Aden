using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    [SerializeField] private RectTransform parent;

    public int rows;
    public int column;
    public Vector2 cellSize;
    public Vector2 spacing;

    public float parentWidth;
    public float parentHeight;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

       

    }

    public override void CalculateLayoutInputVertical()
    {
        

        float sqrRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrRt);
        column = 3;

        parentWidth = parent.rect.width;
        parentHeight = parent.rect.height;


        float cellWidth = (parentWidth / (float)column) - (spacing.x / (float)column * 2);
        float cellHeight = (parentWidth / (float)rows) - (spacing.y / (float)rows * 2);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int columnCount = 0;
        int rowsCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowsCount = i / column;
            columnCount = i % column;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount);
            var yPos = (cellSize.y * rowsCount) + (spacing.y * rowsCount);

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }


}

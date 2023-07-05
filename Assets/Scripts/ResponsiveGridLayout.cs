using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponsiveGridLayout : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public RectTransform parent;
    public int numberOfColumns = 1;
    public int numberOfRows = 2;


    private void Start()
    {
    }

    private void Update()
    {
        
        AdjustGridLayout();
    }

    private void AdjustGridLayout()
    {
        float screenHeight = parent.rect.height;
        float screenWidth = parent.rect.width;

        
                                 
        float cellWidth = (screenWidth / numberOfColumns) - (gridLayout.spacing.x / numberOfColumns * 2);
        float cellHeight = (screenWidth / numberOfColumns) - (gridLayout.spacing.y / numberOfColumns * 2);

        /*float cellWidth = (parentWidth / (float)column) - (spacing.x / (float)column * 2);
        float cellHeight = (parentWidth / (float)rows) - (spacing.y / (float)rows * 2);*/

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }
}

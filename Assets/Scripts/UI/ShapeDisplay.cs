using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class ShapeDisplay : MonoBehaviour
{
    private int _gridSize;
    [SerializeField] private float _gridImageSize = 360f;
    [SerializeField] private GameObject _gridCellPrefab;

    public void DisplayShape(int[,] shape)
    {
        _gridSize = shape.GetLength(0);
        for (int row = 0; row < _gridSize; row++)
        {
            for (int col = 0; col < _gridSize; col++)
            {
                GameObject gridCell = Instantiate(_gridCellPrefab, gameObject.transform, true);
                if (shape[row, col] == 0)
                    gridCell.GetComponent<Image>().color = Color.white;
                if (shape[row, col] == 1)
                    gridCell.GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void UpdateGrid(int gridSize)
    {
        float gridCellSide = _gridImageSize / gridSize;
        Vector2 gridCellSize = new Vector2(gridCellSide, gridCellSide);
        GetComponent<GridLayoutGroup>().cellSize = gridCellSize;
    }
}

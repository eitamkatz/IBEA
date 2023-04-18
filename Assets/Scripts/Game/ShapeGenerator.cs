using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using UnityEditor;
using UnityEngine;
using Random = Unity.Mathematics.Random;

// public class ShapeGenerator : MonoBehaviour
// {
//     private Random _random = new Random(1);
//     private int[,] _shape; // generated shape
//     private List<int[]> _availableSpots; // spots available to place a block
//     private int[][][] _blocks =
//     {
//         new[]
//         {
//             // I block
//             new[] { 1, 1, 1, 1 }
//         },
//         new[]
//         {
//             // I block (rotated)
//             new[] { 1 },
//             new[] { 1 },
//             new[] { 1 },
//             new[] { 1 }
//         },
//         new[]
//         {
//             // L block
//             new[] { 1, 1, 1 },
//             new[] { 1, 0, 0 }
//         },
//         new[]
//         {
//             // L block (rotated)
//             new[] { 1, 1 },
//             new[] { 0, 1 },
//             new[] { 0, 1 }
//         },
//         // new[]
//         // {
//         //     // L block (rotated)
//         //     new[] { 0, 0, 1 },
//         //     new[] { 1, 1, 1 }
//         // },
//         new[]
//         {
//             // L block (rotated)
//             new[] { 1, 0 },
//             new[] { 1, 0 },
//             new[] { 1, 1 }
//         },
//         new[]
//         {
//             // J block
//             new[] { 1, 1, 1 },
//             new[] { 0, 0, 1 }
//         },
//         // new[]
//         // {
//         //     // J block (rotated)
//         //     new[] { 0, 1 },
//         //     new[] { 0, 1 },
//         //     new[] { 1, 1 }
//         // },
//         new[]
//         {
//             // J block (rotated)
//             new[] { 1, 0, 0 },
//             new[] { 1, 1, 1 }
//         },
//         new[]
//         {
//             // J block (rotated)
//             new[] { 1, 1 },
//             new[] { 1, 0 },
//             new[] { 1, 0 }
//         },
//         new[]
//         {
//             // T block
//             new[] { 1, 1, 1 },
//             new[] { 0, 1, 0 }
//         },
//         // new[]
//         // { 
//         //     // T block (rotated)
//         //     new[] { 0, 1 },
//         //     new[] { 1, 1 },
//         //     new[] { 0, 1 }
//         // },
//         // new[]
//         // {
//         //     // T block (rotated)
//         //     new[] { 0, 1, 0 },
//         //     new[] { 1, 1, 1 }
//         // },
//         new[]
//         {
//             // T block (rotated)
//             new[] { 1, 0 },
//             new[] { 1, 1 },
//             new[] { 1, 0 }
//         },
//         // new[]
//         // {
//         //     // S block
//         //     new[] { 0, 1, 1 },
//         //     new[] { 1, 1, 0 }
//         // },
//         new[]
//         {
//             // S block (rotated)
//             new[] { 1, 0 },
//             new[] { 1, 1 },
//             new[] { 0, 1 }
//         },
//         new[]
//         {
//             // Z block
//             new[] { 1, 1, 0 },
//             new[] { 0, 1, 1 }
//         },
//         // new[]
//         // {
//         //     // Z block (rotated)
//         //     new[] { 0, 1 },
//         //     new[] { 1, 1 },
//         //     new[] { 1, 0}
//         // },
//         new[]
//         {
//             // O block
//             new[] { 1, 1 },
//             new[] { 1, 1 }
//         }
//     };
//
//     /*
//      * generates a single shape made up of randomized tetrominoes 
//      */
//     public int[,] GenerateShape(int level, int gridSize)
//     {
//         _shape = new int[gridSize, gridSize];
//         _availableSpots = new List<int[]>();
//         TakeSpot(0, 0);
//         while (level > 0)
//         {
//             int blockIndex = _random.NextInt(_blocks.Length);
//             int spotIndex = _random.NextInt(_availableSpots.Count);
//             if (CheckFit(blockIndex, spotIndex))
//             {
//                 PlaceBlock(blockIndex, spotIndex);
//                 level--;
//             }
//         }
//
//         return _shape;
//     }
//
//     /*
//      * checks if block fits in a given spot
//      */
//     private bool CheckFit(int blockIndex, int spotIndex)
//     {
//         int[][] block = _blocks[blockIndex];
//         int[] spot = _availableSpots[spotIndex];
//         for (int row = 0; row < block.Length; row++)
//         {
//             for (int col = 0; col < block[0].Length; col++)
//             {
//                 if (block[row][col] != 0)
//                 {
//                     if (spot[0] + row > _shape.GetLength(0) - 1) return false;
//                     if (spot[1] + col > _shape.GetLength(1) - 1) return false;
//                     if (_shape[spot[0] + row, spot[1] + col] != 0) return false;
//                 }
//             }
//         }
//
//         return true;
//     }
//
//     /*
//      * places a block in a given spot
//      */
//     void PlaceBlock(int blockIndex, int spotIndex)
//     {
//         int[][] shape = _blocks[blockIndex];
//         int[] spot = _availableSpots[spotIndex];
//         for (int row = 0; row < shape.Length; row++)
//         {
//             for (int col = 0; col < shape[0].Length; col++)
//             {
//                 if (shape[row][col] != 0)
//                     TakeSpot(spot[0] + row, spot[1] + col);
//             }
//         }
//     }
//
//     /*
//      * places a single square and adds it's 
//      */
//     void TakeSpot(int row, int col)
//     {
//         _shape[row, col] = 1;
//         _availableSpots.Remove(new [] { row, col });
//         if (row + 1 < _shape.GetLength(0) && _shape[row + 1, col] == 0)
//             _availableSpots.Add(new [] { row + 1, col });
//         if (row - 1 >= 0 && _shape[row - 1, col] == 0)
//             _availableSpots.Add(new [] { row - 1, col });
//         if (col + 1 < _shape.GetLength(1) && _shape[row, col + 1] == 0)
//             _availableSpots.Add(new [] { row, col + 1 });
//         if (col - 1 >= 0 && _shape[row, col - 1] == 0)
//             _availableSpots.Add(new [] { row, col - 1 });
//     }
// }

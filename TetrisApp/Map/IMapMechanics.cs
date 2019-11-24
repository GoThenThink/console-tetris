using System;
using TetrisApp.Objects;

namespace TetrisApp.Map
{
    //Интерфейс, отвечающий за игровую механику.
    interface IMapMechanics
    {
        bool IntersectionCheck(Tetramino currentFigure, bool[,] buildingArea);
        void PlaceCurrentFigure(Tetramino currentFigure, bool[,] buildingArea);
        int CompletedLines(bool[,] buildingArea);
        void DrawBuildingArea(bool[,] buildingArea);
    }
}

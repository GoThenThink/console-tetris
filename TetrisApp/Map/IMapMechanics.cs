using System;
using TetrisApp.Objects;

namespace TetrisApp.Map
{
    //Интерфейс, отвечающий за игровую механику.
    interface IMapMechanics
    {
        bool IntersectionCheck(ITetramino currentFigure, bool[,] buildingArea);
        void PlaceCurrentFigure(ITetramino currentFigure, bool[,] buildingArea);
        int CompletedLines(bool[,] buildingArea);
        void DrawBuildingArea(bool[,] buildingArea);
    }
}

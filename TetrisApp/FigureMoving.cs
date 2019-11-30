using System;
using TetrisApp.Objects;
using TetrisApp.Map;
using System.Collections.Generic;

namespace TetrisApp
{
    //Класс, отвечающий за изменение положения фигур.
    static class FigureMoving
    {
        public static void Moving(IMap map)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow)
            {
                RotateLeft(map.CurrentFigure, map.BuildingArea);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                RotateRight(map.CurrentFigure, map.BuildingArea);
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                MoveLeft(map.CurrentFigure, map.BuildingArea);
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                MoveRight(map.CurrentFigure, map.BuildingArea);
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        // Метод, отвечающий за поворот фигуры против часовой стрелке.
        public static void RotateLeft(ITetramino currentFigure, bool[,] buildingArea)
        {
            int nextState = currentFigure.State;                                    //Запоминаем номер текущего положения фигуры во временную переменную.
            if (currentFigure.State - 1 < 1)                                        //Делаем проверку на выход из массива положений фигуры.
            {
                nextState = currentFigure.CountState;                               //Изменяем номер текущего положения на -1 (выполняем поворот против часовой стрелки).
            }
            else nextState--;                                                       //Изменяем номер текущего положения на -1 (выполняем поворот против часовой стрелки).
            ITetramino nextStateCurFig = (ITetramino)currentFigure.Clone();         //Создаем копию текущей фигуры.
            nextStateCurFig.Body = currentFigure.FigureState[nextState];            //Приводим новую фигуру в соответствии с измененным номером положения (уже повернутая).

            if (RotateCheck(nextStateCurFig, buildingArea))                         //Проверяем не будет ли ошибок, если фигуру повернуть.
            {
                currentFigure.State = nextState;                                    //Если ошибок нет, то выполняем поворот для изначальной фигуры.
                currentFigure.Body = nextStateCurFig.Body;                          //(записываем номер положения временной фигуры и приводим саму фигуру в соответствии с номером).
            }
        }

        // Метод, отвечающий за поворот фигуры по часовой стрелке.
        public static void RotateRight(ITetramino currentFigure, bool[,] buildingArea)
        {
            int nextState = currentFigure.State;
            if (currentFigure.State + 1 > currentFigure.CountState)
            {
                nextState = 1;
            }
            else nextState++;
            ITetramino nextStateCurFig = (ITetramino)currentFigure.Clone();
            nextStateCurFig.Body = currentFigure.FigureState[nextState];

            if (RotateCheck(nextStateCurFig, buildingArea))
            {
                currentFigure.State = nextState;
                currentFigure.Body = nextStateCurFig.Body;
            }
        }

        // Метод, отвечающий за проверку возможности поворота фигура.
        public static bool RotateCheck(ITetramino nextCurFig, bool[,] buildingArea)
        {
            if (nextCurFig.CurrentRow + nextCurFig.Body.GetLength(0) > buildingArea.GetLength(0))           //Если повернутая фигура окажется ниже               
            {                                                                                               //нижней границы поля игры, то возвращаем
                return false;                                                                               //"ложь" (поворот делать не стоит).
            }
            else if (nextCurFig.CurrentCol + nextCurFig.Body.GetLength(1) > buildingArea.GetLength(1))      //Если повернутая фигура окажется правее
            {                                                                                               //правой границы поля игры, то возвращаем        
                return false;                                                                               //"ложь" (поворот делать не стоит).
            }

            for (int row = 0; row < nextCurFig.Body.GetLength(0); row++)                                    //Если части повернутой фигуры оказались внутри
            {                                                                                           //построенных блоков, то также возвращаем "ложь".
                for (int col = 0; col < nextCurFig.Body.GetLength(1); col++)
                {
                    if (buildingArea[nextCurFig.CurrentRow + row, nextCurFig.CurrentCol + col] == true &
                    nextCurFig.Body[row, col] == true)
                        return false;
                }
            }
            return true;
        }

        // Метод, отвечающий за движение фигуры влево.
        public static void MoveLeft(ITetramino currentFigure, bool[,] buildingArea)
        {
            if (MoveLeftCheck(currentFigure, buildingArea))                                                 //Проверяем на предмет ошибок при движении фигуры влево.
                currentFigure.CurrentCol--;
        }

        // Метод, отвечающий за проверку возможности движения фигуры влево.
        public static bool MoveLeftCheck(ITetramino currentFigure, bool[,] buildingArea)
        {
            bool moveLeftCheck = true;
            for (int i = 0; i < currentFigure.Body.GetLength(0); i++)
            {
                if (moveLeftCheck == false)
                    break;
                else
                {
                    if (currentFigure.CurrentCol > 0 &&                                                     //Если фигура не нарушает левую границу поля
                       buildingArea[currentFigure.CurrentRow + i, currentFigure.CurrentCol - 1] == false)   //и с левой от нее стороны нет построенных блоков,
                        moveLeftCheck = true;                                                               //то возвращаем "истина" (можно двигать влево).
                    else moveLeftCheck = false;
                }
            }
            return moveLeftCheck;
        }

        // Метод, отвечающий за движение фигуры вправо.
        public static void MoveRight(ITetramino currentFigure, bool[,] buildingArea)
        {
            if (MoveRightCheck(currentFigure, buildingArea))
                currentFigure.CurrentCol++;
        }

        // Метод, отвечающий за проверку возможности движения фигуры вправо.
        public static bool MoveRightCheck(ITetramino currentFigure, bool[,] buildingArea)
        {
            bool moveRightCheck = true;
            for (int i = 0; i < currentFigure.Body.GetLength(0); i++)
            {
                if (moveRightCheck == false)
                    break;
                else
                {
                    if (currentFigure.CurrentCol + currentFigure.Body.GetLength(1) < buildingArea.GetLength(1) &&
                       buildingArea[currentFigure.CurrentRow + i, currentFigure.CurrentCol + currentFigure.Body.GetLength(1)] == false)
                        moveRightCheck = true;
                    else moveRightCheck = false;
                }
            }
            return moveRightCheck;
        }
    }
}

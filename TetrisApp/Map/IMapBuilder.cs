using System;

namespace TetrisApp.Map
{
    // Интерфейс, отвечающий за создание карты игры.
    interface IMapBuilder
    {
        IMap Build();
    }
}

using System;
using Model.LevelStateLogic;
using Model.MapLogic;
using UnityEngine;

namespace Model.LevelTasksLogic
{
    public class RectangularAreaTask : ILevelTask
    {
        private readonly int _areaLimit;

        public int AreaLimit => _areaLimit;

        public RectangularAreaTask(int areaLimit)
        {
            _areaLimit = areaLimit;
        }

        private int CalculateVerticalBorder(ReadOnlyMap map, Func<int, int> xTransform)
        {
            for(int x = 0; x < map.Width; x++)
            {
                for(int y = 0; y < map.Height; y++)
                {
                    int transformedX = xTransform.Invoke(x);
                    if(map[transformedX, y].IsOccupied)
                        return transformedX;
                }
            }
            return 0;
        } 

        private int CalculateHorizontalBorder(ReadOnlyMap map, Func<int, int> yTransform)
        {
            for(int y = 0; y < map.Height; y++)
            {
                for(int x = 0; x < map.Width; x++)
                {
                    int transformedY = yTransform.Invoke(y);
                    if(map[x, transformedY].IsOccupied)
                        return transformedY;
                }
            }
            return 0;
        } 

        private int CalculateArea(ReadOnlyMap map) 
        {
            int left = CalculateVerticalBorder(map, x => x);
            int right = CalculateVerticalBorder(map, x => map.Width - 1 - x);
            int top = CalculateHorizontalBorder(map, y => map.Height - 1 - y);
            int bottom = CalculateHorizontalBorder(map, y => y);

            return (right - left + 1) * (top - bottom + 1);
        }

        public bool CheckCompletion(LevelState levelState)
            => CalculateArea(levelState.Map) <= _areaLimit;

        public T Accept<T>(ILevelTaskVisitor<T> visitor)
            => visitor.Visit(this);
    }
}
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Factories
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly IShapeService _shapeService;

        public ShapeFactory(IShapeService shapeService)
        {
            _shapeService = shapeService;
        }

        /// <summary>
        /// Calculates coordinates of a shape based on its grid value
        /// </summary>
        /// <param name="shapeEnum"> type of shape</param>
        /// <param name="grid"> pixel size of the grid </param>
        /// <param name="gridValue"> grid value of the shape </param>
        /// <returns> Shape calculated from the grid value or null if the shape type is invalid </returns>
        public Shape? CalculateCoordinates(ShapeEnum shapeEnum, Grid grid, GridValue gridValue)
        {
            switch (shapeEnum)
            {
                case ShapeEnum.Triangle:
                    Shape resultingShape = _shapeService.ProcessTriangle(grid, gridValue);
                    return resultingShape;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Returns a grid value of the given shape based on its coordinates
        /// </summary>
        /// <param name="shapeEnum"> type of shape </param>
        /// <param name="grid"> pixel size of the grid </param>
        /// <param name="shape"> shape to calculate the value of or null if shape type is invalid </param>
        /// <returns></returns>
        public GridValue? CalculateGridValue(ShapeEnum shapeEnum, Grid grid, Shape shape)
        {
            switch (shapeEnum)
            {
                case ShapeEnum.Triangle:
                    if (shape.Coordinates.Count != 3)
                        return null;

                    Triangle triangleResultingFromPassedShape = new Triangle(shape.Coordinates[0], shape.Coordinates[1], shape.Coordinates[2]);
                    GridValue resultingGridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangleResultingFromPassedShape);
                    return resultingGridValue;
                default:
                    return null;
            }
        }
    }
}

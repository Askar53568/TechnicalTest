using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Services
{
    public class ShapeService : IShapeService
    {
        /// <summary>
        /// Calculates the coordinates of a triangle based on its grid value
        /// </summary>
        /// <param name="grid"> pixel size of the grid</param>
        /// <param name="gridValue"> value of the triangle </param>
        /// <returns> coordinates of the triangle </returns>
        public Shape ProcessTriangle(Grid grid, GridValue gridValue)
        {
            // number of cells in the grid is 6, number of grid values is 12
            // thus we devide the grid value by 2 and round up to take into account odd numbers
            int column = (int)Math.Ceiling((double)gridValue.Column / 2.0) * grid.Size;
            int row = (gridValue.GetNumericRow() - 1) * grid.Size;
            Coordinate OuterVertex = new Coordinate(0, 0);
            Coordinate TopLeftVertex = new Coordinate(0, 0);
            Coordinate BottomRightVertex = new Coordinate(0, 0);
            // Check if a triangle is on the bottom or the top
            if (gridValue.Column % 2 == 0)
            {
                // If the column value is even then triangle is at the top
                OuterVertex = new Coordinate(Convert.ToInt32(column), row);
                TopLeftVertex = new Coordinate(Convert.ToInt32(column) - 10, row);
                BottomRightVertex = new Coordinate(Convert.ToInt32(column), row + 10);
            }
            else
            {
                // If the column value is odd then triangle is on the bottom
                OuterVertex = new Coordinate(Convert.ToInt32(column) - 10, row + 10);
                TopLeftVertex = new Coordinate(Convert.ToInt32(column) - 10, row);
                BottomRightVertex = new Coordinate(Convert.ToInt32(column), row + 10);
            }

            return new Shape(new List<Coordinate>
            {
                OuterVertex,
                TopLeftVertex,
                BottomRightVertex
            });
        }
        /// <summary>
        /// Calculates a grid value of the triangle based on its coordinates
        /// </summary>
        /// <param name="grid"> pixel size of the grid</param>
        /// <param name="gridValue"> value of the triangle </param>
        /// <returns> grid value of the triangle </returns>
        public GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle)
        {
            int column = triangle.OuterVertex.X / 10 * 2;
            int row = triangle.BottomRightVertex.Y / 10;
            // if the x coordinates of the outer and top left vertices are equal, then the triangle is at the bottom
            // the character value of 0 is '@', so we need to increase the column value by 1 to get 'A'
            if ((triangle.TopLeftVertex.X - triangle.OuterVertex.X == 0) || (triangle.OuterVertex.X == 0))
            {
                column++;
            }
            return new GridValue(row, column);
        }

    }
}
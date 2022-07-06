using TechnicalTest.Core.Models;
using TechnicalTest.Core.Services;
using Xunit;

namespace TechnicalTest.Core.Tests.Services
{
    public class ShapeServiceTests
    {
        private readonly ShapeService _shapeService = new();

        [Fact]
        public void GivenGridValueA1WhenProcessingLeftTriangleThenNumberOfCoordinatesIs3()
        {
            var gridValue = new GridValue("A1");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Equal(3, shape.Coordinates.Count);
        }

        [Fact]
        public void GivenGridValueA1AndGridSize10WhenProcessingLeftTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("A1");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 0 && c.Y == 0);
            Assert.Contains(shape.Coordinates, (c) => c.X == 0 && c.Y == 10);
            Assert.Contains(shape.Coordinates, (c) => c.X == 10 && c.Y == 10);
        }

        [Fact]
        public void GivenGridValueA2AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("A2");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 0 && c.Y == 0);
            Assert.Contains(shape.Coordinates, (c) => c.X == 10 && c.Y == 0);
            Assert.Contains(shape.Coordinates, (c) => c.X == 10 && c.Y == 10);
        }

        [Fact]
        public void GivenGridValueD5AndGridSize10WhenProcessingLeftTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("D5");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 20 && c.Y == 30);
            Assert.Contains(shape.Coordinates, (c) => c.X == 20 && c.Y == 40);
            Assert.Contains(shape.Coordinates, (c) => c.X == 30 && c.Y == 40);
        }

        [Fact]
        public void GivenGridValueD6AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("D6");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 20 && c.Y == 30);
            Assert.Contains(shape.Coordinates, (c) => c.X == 30 && c.Y == 30);
            Assert.Contains(shape.Coordinates, (c) => c.X == 30 && c.Y == 40);
        }

        [Fact]
        public void GivenGridValueF11AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("F11");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 60);
            Assert.Contains(shape.Coordinates, (c) => c.X == 60 && c.Y == 60);
        }

        [Fact]
        public void GivenD6TriangleCoordinatesWhenProcessingGridValueThenGridValueIsD6()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new(20, 30), new(30, 30), new Coordinate(30, 40));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(6, gridValue.Column);
            Assert.Equal("D", gridValue.Row);
        }

        [Fact]
        public void GivenD5TriangleCoordinatesWhenProcessingGridValueThenGridValueIsD5()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new(20, 30), new(20, 40), new Coordinate(30, 40));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(5, gridValue.Column);
            Assert.Equal("D", gridValue.Row);
        }
        [Fact]
        public void GivenF11TriangleCoordinatesWhenProcessingGridValueThenGridValueIsF11()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new(50, 50), new(50, 60), new Coordinate(60, 60));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(11, gridValue.Column);
            Assert.Equal("F", gridValue.Row);
        }
    }
}
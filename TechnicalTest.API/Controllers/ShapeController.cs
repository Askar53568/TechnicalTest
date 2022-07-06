using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.DTOs;
using TechnicalTest.Core;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.API.Controllers
{
    /// <summary>
    /// Shape Controller which is responsible for calculating coordinates and grid value.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeFactory _shapeFactory;

        /// <summary>
        /// Constructor of the Shape Controller.
        /// </summary>
        /// <param name="shapeFactory"></param>
        public ShapeController(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        /// <summary>
        /// Calculates the Coordinates of a shape given the Grid Value.
        /// </summary>
        /// <param name="calculateCoordinatesRequest"></param>   
        /// <returns>A Coordinates response with a list of coordinates.</returns>
        /// <response code="200">Returns the Coordinates response model.</response>
        /// <response code="400">If an error occurred while calculating the Coordinates.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shape))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateCoordinates")]
        [HttpPost]
        public IActionResult CalculateCoordinates([FromBody] CalculateCoordinatesDTO calculateCoordinatesRequest)
        {
            // Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            ShapeEnum shapeEnum = (ShapeEnum)calculateCoordinatesRequest.ShapeType;
            if (shapeEnum == ShapeEnum.None || shapeEnum == ShapeEnum.Other)
            {
                return BadRequest();
            }
            // Call the Calculate function in the shape factory.
            Grid grid = new Grid(calculateCoordinatesRequest.Grid.Size);
            GridValue gridValue = new GridValue(calculateCoordinatesRequest.GridValue);
            Shape? shape = _shapeFactory.CalculateCoordinates(shapeEnum, grid, gridValue);
            // Return BadRequest with error message if the calculate result is null
            if (shape == null)
            {
                return BadRequest();
            }
            // Create ResponseModel with Coordinates and return as OK with responseModel.
            else
            {
                return Ok(shape.Coordinates);
            }
        }
        /// <summary>
        /// Calculates the Grid Value of a shape given the Coordinates.
        /// </summary>
        /// <remarks>
        /// A Triangle Shape must have 3 vertices, in this order: Top Left Vertex, Outer Vertex, Bottom Right Vertex.
        /// </remarks>
        /// <param name="gridValueRequest"></param>   
        /// <returns>A Grid Value response with a Row and a Column.</returns>
        /// <response code="200">Returns the Grid Value response model.</response>
        /// <response code="400">If an error occurred while calculating the Grid Value.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridValue))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateGridValue")]
        [HttpPost]
        public IActionResult CalculateGridValue([FromBody] CalculateGridValueDTO gridValueRequest)
        {
            // Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            ShapeEnum shapeEnum = (ShapeEnum)gridValueRequest.ShapeType;
            if (shapeEnum == ShapeEnum.None || shapeEnum == ShapeEnum.Other)
            {
                return BadRequest();

            }

            Grid grid = new Grid(gridValueRequest.Grid.Size);
            // Create new Shape with coordinates based on the parameters from the DTO.
            Vertex[] vertexArray = gridValueRequest.Vertices.ToArray();
            List<Coordinate> coordinates = new List<Coordinate>();
            for (int i = 0; i < vertexArray.Length; i++)
            {
                coordinates.Add(new(vertexArray[i].x, vertexArray[i].y));
            }
            Shape? shape = new(coordinates);
            // Call the function in the shape factory to calculate grid value.
            GridValue? gridValue = _shapeFactory.CalculateGridValue(shapeEnum, grid, shape);
            if (gridValue == null)
            {
                // If the GridValue result is null then return BadRequest with an error message.
                return BadRequest();
            }
            else
            {
                // Generate a ResponseModel based on the result and return it in Ok();
                return Ok(gridValue);
            }
        }
    }
}

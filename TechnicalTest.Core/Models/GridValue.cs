﻿namespace TechnicalTest.Core.Models
{
    public class GridValue
    {
        public GridValue(string gridValue)
        {
            //changed max grid value length from 2 to 3 due to the given grid size and values
            if (string.IsNullOrEmpty(gridValue) || gridValue.Length > 3) return;

            Row = gridValue[..1];
            Column = int.Parse(gridValue[1..]);
        }


        public GridValue(int row, int column)
        {
            var numericValueOfCharacter = (char)64 + row;
            Row = ((char)numericValueOfCharacter).ToString();
            Column = column;
        }

        public string? Row { get; set; }

        public int Column { get; set; }

        public int GetNumericRow() => Row != null ? char.ToUpper(char.Parse(Row)) - 64 : 0;
    }
}

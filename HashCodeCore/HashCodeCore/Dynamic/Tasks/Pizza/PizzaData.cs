using System.Collections.Generic;

namespace HashCodeCore.Dynamic.Tasks.Pizza
{
    public class PizzaData : IData
    {
        public static readonly int Mushroom = 0;
        public static readonly int Tomato = 1;
        
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Lowest { get; set; }
        public int Highest { get; set; }
        public int[,] Grid { get; set; }
        public List<PizzaSlice> Slices { get; set; }
        public int TotalMushrooms { get; set; }
        public int TotalTomatoes { get; set; }

        public PizzaData(int[] data)
        {
            Rows = data[0];
            Columns = data[1];
            Lowest = data[2];
            Highest = data[3];
            Grid = new int[Rows,Columns];
            Slices = new List<PizzaSlice>();
            TotalTomatoes = TotalMushrooms = 0;
        }

        public string[] Stringify()
        {
            string[] output = new string[Slices.Count + 1];
            output[0] = Slices.Count.ToString();
            for (var i = 0; i < Slices.Count; i++)
            {
                output[i + 1] = Slices[i].ToString();
            }

            return output;
        }

        public void AddIngredient(int row, int column, char type)
        {
            Grid[row, column] = type == 'T' ? Tomato : Mushroom;
            if (type == 'T')
            {
                ++TotalTomatoes;
            }
            else
            {
                ++TotalMushrooms;
            }
        }
    }
}
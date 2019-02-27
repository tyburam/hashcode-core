using System;
using System.Collections.Generic;

namespace HashCodeCore.Dynamic.Tasks.Pizza
{
    public class PizzaProblem : Problem
    {
        private PizzaData data;
        private List<Tuple<int,int>> shapes;
        private List<Tuple<int, int>> possibleStarts;
        private int[,] alreadySliced;
        private int freeSpace;
        private PizzaDataProcessor _processor;
        private int tomatoesLeft;
        private int mushroomsLeft;
        
        public PizzaProblem(string inputPath, IDataProcessor processor) : base(inputPath, processor)
        {
            _processor = (processor as PizzaDataProcessor);
            _processor.Process(inputPath);
            data = _processor.Data;
            tomatoesLeft = data.TotalTomatoes;
            mushroomsLeft = data.TotalMushrooms;
        }

        public override void Solve()
        {
            //build all possible pizza shapes
            shapes = new List<Tuple<int,int>>();
            for (var i = 1; i <= data.Rows; i++)
            {
                for (var j = 1; j <= data.Columns + 1; j++)
                {
                    if (i * j >= data.Lowest * 2 && i * j <= data.Highest)
                    {
                        shapes.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            shapes.Reverse(); //start greedy

            //knowing already sliced and possible slices 
            alreadySliced = new int[data.Rows, data.Columns];
            possibleStarts = new List<Tuple<int, int>>();
            for (var i = 0; i < data.Rows; i++)
            {
                for (var j = 0; j < data.Columns; j++)
                {
                    alreadySliced[i,j] = 0;
                    possibleStarts.Add(new Tuple<int, int>(i, j));
                }
            }

            freeSpace = data.Rows * data.Columns;

            var k = -1;
            while (freeSpace > 0)
            {
                if (possibleStarts.Count == 0)
                {
                    break;
                }

                ++k;
                if (k >= possibleStarts.Count)
                {
                    break;
                }
                
                foreach (var shape in shapes)
                {
                    if (SliceIfValid(possibleStarts[k], shape))
                    {
                        k = -1; //because possible starts where deleted
                        break;
                    }
                }
            }
        }
        
        public override void Store(string outputPath)
        {
            _processor.Store(data, outputPath);
        }

        private bool SliceIfValid(Tuple<int, int> candidateSlice, Tuple<int, int> shape)
        {
            //shape is to big for a place left on pizza?
            if (shape.Item1 * shape.Item2 > freeSpace)
            {
                return false;
            }
            
            int leftTop = candidateSlice.Item1,
                rightTop = leftTop + shape.Item1 - 1,
                leftBottom = candidateSlice.Item2,
                rightBottom = leftBottom + shape.Item2 - 1;

            //whole rectangle is on pizza?
            if ((rightTop >= data.Rows) || (rightBottom >= data.Columns))
            {
                return false;
            }
            
            //counting mushrooms and tomatoes on a slice
            //checking if any position was already used
            int mush = 0, tom = 0;
            for (var i = leftTop; i <= rightTop; i++)
            {
                for (var j = leftBottom; j <= rightBottom; j++)
                {
                    if (alreadySliced[i, j] == 1)
                    {
                        return false;
                    }
                    if (data.Grid[i, j] == PizzaData.Tomato)
                    {
                        ++tom;
                    }
                    else
                    {
                        ++mush;
                    }
                }
            }
            
            // to much or to few of pieces?
            if (mush < data.Lowest || mush > data.Highest || tom < data.Lowest || tom > data.Highest)
            {
                return false;
            }

            // there's still place but there's no way new slice will be valid?
            if (freeSpace > data.Highest && (tomatoesLeft - tom <= 0 || mushroomsLeft - mush <= 0))
            {
                return false;
            }
            
            // everything seems ok. Slicing now
            for (var i = leftTop; i <= rightTop; i++)
            {
                for (var j = leftBottom; j <= rightBottom; j++)
                {
                    alreadySliced[i, j] = 1;
                    possibleStarts.Remove(new Tuple<int, int>(i, j));
                    --freeSpace;
                }
            }

            tomatoesLeft -= tom;
            mushroomsLeft -= mush;
            
            data.Slices.Add(new PizzaSlice(leftTop, rightTop, leftBottom, rightBottom));
            return true;
        }
    }
}
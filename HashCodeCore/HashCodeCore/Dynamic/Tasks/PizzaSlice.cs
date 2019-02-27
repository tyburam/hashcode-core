namespace dp_net.Dynamic.Tasks
{
    public class PizzaSlice
    {
        public int Row1 { get; set; }
        public int Row2 { get; set; }
        public int Column1 { get; set; }
        public int Column2 { get; set; }

        public PizzaSlice(int r1, int r2, int c1, int c2)
        {
            Row1 = r1;
            Row2 = r2;
            Column1 = c1;
            Column2 = c2;
        }

        public override string ToString()
        {
            return $"{Row1} {Column1} {Row2} {Column2}";
        }
    }
}
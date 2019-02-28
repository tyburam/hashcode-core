using System.Collections.Generic;
using System.Text;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class Slide
    {
        public List<int> Photos { get; set; }

        public Slide(int id)
        {
            Photos = new List<int>();
            Photos.Add(id);
        }

        public Slide(int id1, int id2)
        {
            Photos = new List<int>();
            Photos.Add(id1);
            Photos.Add(id2);
        }

        public override string ToString()
        {
            if (Photos.Count == 1)
            {
                return Photos[0].ToString();
            }
            
            var sb = new StringBuilder();
            foreach (var p in Photos)
            {
                sb.AppendFormat("{0} ", p);
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
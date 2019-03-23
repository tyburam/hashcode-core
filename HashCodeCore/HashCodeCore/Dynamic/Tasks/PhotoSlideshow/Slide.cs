using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class Slide
    {
        public List<int> Photos { get; set; }
        public List<int> Tags { get; private set; }

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

        public Slide(Photo p)
        {
            Photos = new List<int>();
            Photos.Add(p.Id);
            
            Tags = new List<int>(p.ProcessedTags);
        }

        public Slide(Photo p1, Photo p2)
        {
            Photos = new List<int>();
            Photos.Add(p1.Id);
            Photos.Add(p2.Id);
            
            Tags = new List<int>(p1.ProcessedTags);
            foreach (var pt in p2.ProcessedTags)
            {
                if(Tags.Contains(pt)) continue;
                Tags.Add(pt);
            }
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
        
        public int InterestFactor(Slide s2)
        {
            var intersect = Tags.Intersect(s2.Tags).Count();
            var only1 = Tags.Count - intersect;
            var only2 = s2.Tags.Count - intersect;

            var min = intersect <= only1 ? intersect : only1;
            return min <= only2 ? min : only2;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class Photo : IComparable<Photo>
    {
        public static readonly char Horizontal = 'H';
        public static readonly char Vertical = 'V';
            
        public int Id { get; set; }
        public bool IsHorizontal { get; set; }
        public List<string> Tags { get; set; }
        public List<int> ProcessedTags { get; set; }
        public int TagsScore { get; set; }

        public Photo(string[] data, int id)
        {
            Id = id;
            IsHorizontal = data[0][0] == Horizontal;
            Tags = new List<string>();
            for (var i = 2; i < data.Length; i++)
            {
                Tags.Add(data[i]);
            }
            Tags.Sort();
            ProcessedTags = new List<int>();
        }

        public int InterestFactor(Photo p2)
        {
            var intersect = ProcessedTags.Intersect(p2.ProcessedTags).Count();
            var only1 = ProcessedTags.Count - intersect;
            var only2 = p2.ProcessedTags.Count - intersect;

            var min = intersect <= only1 ? intersect : only1;
            return min <= only2 ? min : only2;
        }

        public int CompareTo(Photo other)
        {
            return TagsScore == other.TagsScore ? 0 : (TagsScore > other.TagsScore ? 1 : -1);
        }
    }
}
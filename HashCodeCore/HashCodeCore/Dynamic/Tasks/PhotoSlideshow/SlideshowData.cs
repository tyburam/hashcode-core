using System;
using System.Collections.Generic;
using System.Numerics;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class SlideshowData : IData
    {
        public List<Photo> Photos { get; private set; }
        public Dictionary<string, int> Tags { get; private set; }
        public List<Slide> Slides { get; set; }

        private Dictionary<int, int> TagsHist;

        public SlideshowData(string[] content)
        {
            Photos = new List<Photo>();
            Tags = new Dictionary<string, int>();
            Slides = new List<Slide>();
            TagsHist = new Dictionary<int, int>();
            
            for (var i = 1; i < content.Length; i++)
            {
                Photos.Add(new Photo(content[i].Split(), i - 1));
                foreach(var tag in Photos[i-1].Tags)
                {
                    if (!Tags.ContainsKey(tag))
                    {
                        Tags.Add(tag, Tags.Count);
                        TagsHist.Add(Tags.Count - 1, 1);
                    }
                    Photos[i-1].ProcessedTags.Add(Tags[tag]);
                    ++TagsHist[Tags[tag]];
                }
            }

            foreach (var p in Photos)
            {
                var scoring = 0;
                foreach (var tag in p.ProcessedTags)
                {
                    scoring += TagsHist[tag];
                }

                p.TagsScore = scoring;
            }
        }
        
        public string[] Stringify()
        {
            var data = new string[Slides.Count + 1];
            data[0] = Slides.Count.ToString();
            for (var i = 0; i < Slides.Count; i++)
            {
                data[i + 1] = Slides[i].ToString();
            }

            return data;
        }

        public void Sort()
        {
            Photos.Sort((l, r) => l.CompareTo(r));
            Photos.Reverse();
        }
    }
}
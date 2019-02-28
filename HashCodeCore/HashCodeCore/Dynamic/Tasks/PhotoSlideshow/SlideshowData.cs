using System.Collections.Generic;
using System.Numerics;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class SlideshowData : IData
    {
        public List<Photo> Photos { get; private set; }
        public Dictionary<string, int> Tags { get; private set; }
        public List<Slide> Slides { get; set; }

        public SlideshowData(string[] content)
        {
            Photos = new List<Photo>();
            Tags = new Dictionary<string, int>();
            Slides = new List<Slide>();
            
            for (var i = 1; i < content.Length; i++)
            {
                Photos.Add(new Photo(content[i].Split(), i - 1));
                foreach(var tag in Photos[i-1].Tags)
                {
                    if (!Tags.ContainsKey(tag))
                    {
                        Tags.Add(tag, Tags.Count);
                    }
                    Photos[i-1].ProcessedTags.Add(Tags[tag]);
                }
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
            var sortedPhoto = new List<Photo>(Photos);
            for (var i = 1; i < sortedPhoto.Count; i++)
            {
                var max = 0;
                var maxInd = 0;
                for (var j = i + 1; j < sortedPhoto.Count; j++)
                {
                    var factor = sortedPhoto[i].InterestFactor(sortedPhoto[j]);
                    if (factor <= max) continue;
                    max = factor;
                    maxInd = j;
                }

                if (i + 1 >= sortedPhoto.Count) continue;
                var tmp = sortedPhoto[i + 1];
                sortedPhoto[i + 1] = sortedPhoto[maxInd];
                sortedPhoto[maxInd] = tmp;
            }

            Photos = sortedPhoto;
        }
    }
}
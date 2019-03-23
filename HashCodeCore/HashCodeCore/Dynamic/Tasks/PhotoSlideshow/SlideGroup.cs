using System.Collections.Generic;
using System.Linq;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class SlideGroup
    {
        public List<Slide> Slides { get; private set; }
        public int Factor { get; private set; }

        public SlideGroup(Slide s1) //for vertical once
        {
            Slides = new List<Slide>(){s1};
            Factor = 0;
        }

        public SlideGroup(Slide s1, Slide s2)
        {
            Slides = new List<Slide>();
            Slides.Add(s1);
            Slides.Add(s2);
            Factor = s1.InterestFactor(s2);
        }
        
        public int InterestFactor(Slide s)
        {
            return Slides.Last().InterestFactor(s);
        }

        public int InterestFactor(SlideGroup sg)
        {
            return InterestFactor(sg.Slides[0]) + sg.Factor;
        }

        public void Merge(SlideGroup sg)
        {
            foreach (var s in sg.Slides)
            {
                Slides.Add(s);
            }
            Factor += (InterestFactor(sg.Slides[0]) + sg.Factor);
        }
    }
}
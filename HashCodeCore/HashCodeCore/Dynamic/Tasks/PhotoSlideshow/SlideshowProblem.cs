using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class SlideshowProblem : Problem
    {
        private SlideshowDataProcessor _processor;
        private SlideshowData _data;
        
        public SlideshowProblem(string inputPath, IDataProcessor processor) : base(inputPath, processor)
        {
            _processor = (processor as SlideshowDataProcessor);
            _processor.Process(inputPath);
            _data = _processor.Data;
        }

        public override void Solve()
        {
            _data.Sort();
            const int i = 0;
            var groups = new List<SlideGroup>();

            var allSlides = new List<Slide>();
            foreach (var p in _data.Photos.Where(p => p.IsHorizontal).ToList())
            {
                allSlides.Add(new Slide(p));
            }
            
            var ver = _data.Photos.Where(p => !p.IsHorizontal).ToList();
            while (ver.Count > 0)
            {
                var minFactor = int.MaxValue;
                var minInd = int.MaxValue;
                for (var j = i + 1; j < ver.Count; j++)
                {
                    var fact = ver[i].InterestFactor(ver[j]);
                    if (fact > minFactor) continue;
                    minFactor = fact;
                    minInd = j;
                }
                allSlides.Add(new Slide(ver[i], ver[minInd]));
                ver.RemoveAt(minInd);
                ver.RemoveAt(i);
            }
            
            while (allSlides.Count > 0)
            {
                var maxFactor = -1;
                var maxInd = -1;
                for (var j = i + 1; j < allSlides.Count; j++)
                {
                    var fact = allSlides[i].InterestFactor(allSlides[j]);
                    if (fact <= maxFactor) continue;
                    maxFactor = fact;
                    maxInd = j;
                }

                if (allSlides.Count == 1)
                {
                    groups.Add(new SlideGroup(allSlides[i]));
                    allSlides.RemoveAt(i);
                    break;
                }
                groups.Add(new SlideGroup(allSlides[i], allSlides[maxInd]));
                allSlides.RemoveAt(maxInd);
                allSlides.RemoveAt(i);
            }

            while (groups.Count > 1)
            {
                for (var j = 0; j < groups.Count / 2; j++)
                {
                    var maxFactor = -1;
                    var maxInd = -1;
                    for (var k = j+ 1; k < groups.Count; k++)
                    {
                        var fact = groups[j].InterestFactor(groups[k]);
                        if (fact <= maxFactor) continue;
                        maxFactor = fact;
                        maxInd = k;
                    }

                    groups[j].Merge(groups[maxInd]);
                    groups.RemoveAt(maxInd);
                    if (groups.Count <= 0) break;
                }
            }

            _data.Slides = groups[0].Slides;

            var score = Score(_data.Slides);
            if (_data.Slides.Count >= 10)
            {
                Console.WriteLine("Building population. Current best score = {0}", score);
                var population = new List<List<Slide>> {new List<Slide>(_data.Slides)};
                for (var j = 0; j < 100; j++)
                {
                    var cur = new List<Slide>(_data.Slides);
                    var rand = new Random(2019 + i);
                    cur.Shuffle(rand);
                    population.Add(cur);
                }

                Console.WriteLine("Evolution");
                var rndInd = 0;
                for (var j = 0; j < 1000; j++)
                {
                    if (j % 100 == 0)
                    {
                        Console.WriteLine("Current epoch = {0}", j);
                    }

                    foreach (var t in population)
                    {
                        var s = Score(t);
                        if (s > score)
                        {
                            _data.Slides = new List<Slide>(t);
                            score = s;
                        }
                        else
                        {
                            var rnd = new Random(2019 + rndInd);
                            t.RandomSwap(rnd);
                            ++rndInd;
                        }
                    }
                }
            }
            
                Console.WriteLine("Before scores {0}", score);
                for (var j = 0; j < 200; j++)
                {
                    var swapped = new List<Slide>(_data.Slides);
                    swapped.RandomSwap(new Random(2019 + j));
                    var s = Score(swapped);
                    if (s <= score) continue;
                    _data.Slides = swapped;
                    score = s;
                }

                Console.WriteLine("After scores {0}", score); 
        }
        
        public override void Store(string outputPath)
        {
            _processor.Store(_data, outputPath);
        }

        private static int Score(List<Slide> slides)
        {
            var sum = 0;
            for (var i = 0; i < slides.Count; i++)
            {
                if (i + 1 >= slides.Count) break;
                sum += slides[i].InterestFactor(slides[i + 1]);
            }

            return sum;
        }
    }
}
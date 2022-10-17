using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            List<float> values = new List<float>(InputSignal.Samples.Count);
            float min = InputSignal.Samples.Min();
            float max = InputSignal.Samples.Max();
            float oldRange = max - min;
            float newRange = InputMaxRange - InputMinRange;

            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float newValue = (InputSignal.Samples[i] - min) * newRange / oldRange + InputMinRange;
                values.Add(newValue);
            }

            OutputNormalizedSignal = new Signal(values, false);
        }
    }
}

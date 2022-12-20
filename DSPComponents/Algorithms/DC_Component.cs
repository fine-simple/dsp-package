using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            // Calculating avg.
            float samplesSum = 0.0F,
                avg = 0.0F;
            foreach (float sample in InputSignal.Samples)
                samplesSum += sample;
            avg = samplesSum / InputSignal.Samples.Count;

            // Removing avg. from samples
            OutputSignal = new Signal(new List<float>(), false);
            OutputSignal.SamplesIndices = InputSignal.SamplesIndices.ToList();
            
            foreach (float sample in InputSignal.Samples)
            {
                OutputSignal.Samples.Add(sample - avg);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        // TODO: Task 3
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            
            var samples = AlgorithmsUtilities.getSampleDict(InputSignal.SamplesIndices, InputSignal.Samples);
            var newSamples = new SortedDictionary<int, float>();

            foreach (var key in samples.Keys)
            {
                newSamples.Add(-key, samples[key]);
            }

            OutputFoldedSignal = new Signal(newSamples.Values.ToList(), false);            
            OutputFoldedSignal.SamplesIndices = newSamples.Keys.ToList();
        }
    }
}
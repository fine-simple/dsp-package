using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        // TODO: Task 3 [Completed]
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            int L = InputSignal.Samples.Count - InputWindowSize + 1;
            var samples = new float[L];
            for (int i = 0; i < L; i++)
            {
                float sum = 0;
                for (int j = i; j < i + InputWindowSize; j++)
                    sum += InputSignal.Samples[j];
                samples[i] = sum / InputWindowSize;
            }
            OutputAverageSignal = new Signal(samples.ToList(), false);
        }
    }
}

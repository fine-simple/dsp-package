using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives : Algorithm
    {
        // Task 3 [Completed]
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {
            FirstDerivative = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);
            SecondDerivative = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);

            // First Derivative
            for (int i = 1; i < InputSignal.Samples.Count; i++)
            {
                float prev = InputSignal.Samples[i - 1],
                    curr = InputSignal.Samples[i];
                FirstDerivative.Samples.Add(curr - prev);
                FirstDerivative.SamplesIndices.Add(InputSignal.SamplesIndices[i]);
            }

            // Second Derivative
            for (int i = 1; i < InputSignal.Samples.Count - 1; i++)
            {
                System.Console.WriteLine(i);
                float prev = i > 0 ? InputSignal.Samples[i - 1] : 0,
                    curr = InputSignal.Samples[i],
                    next = InputSignal.Samples[i + 1];

                SecondDerivative.Samples.Add(next - 2 * curr + prev);
                SecondDerivative.SamplesIndices.Add(InputSignal.SamplesIndices[i]);
            }
        }
    }
}

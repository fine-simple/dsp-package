using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        // Task 3: Completed
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }


        public int minIndex(Signal signal) => signal.SamplesIndices[0];
        public int maxIndex(Signal signal) => signal.SamplesIndices[signal.SamplesIndices.Count - 1];

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            int sizeX = InputSignal1.Samples.Count,
                sizeH = InputSignal2.Samples.Count,
                sizeY = sizeX + sizeH - 1;

            OutputConvolvedSignal = new Signal(new List<float>(), false);

            for (int n = minIndex(InputSignal1) + minIndex(InputSignal2);
                    n < minIndex(InputSignal1) + sizeY; n++)
            {
                float sum = 0;
                for (int k = minIndex(InputSignal1); k <= maxIndex(InputSignal1); k++)
                {
                    // k and n-k are in range
                    if (k >= minIndex(InputSignal1) && k <= maxIndex(InputSignal1) &&
                        n - k >= minIndex(InputSignal2) && n - k <= maxIndex(InputSignal2))
                    {
                        sum += InputSignal1.Samples[InputSignal1.SamplesIndices.IndexOf(k)] *
                        InputSignal2.Samples[InputSignal2.SamplesIndices.IndexOf(n - k)];
                    }
                }

                OutputConvolvedSignal.Samples.Add(sum);
                OutputConvolvedSignal.SamplesIndices.Add(n);
            }

            // Removing taling zeros
            while (OutputConvolvedSignal.Samples[OutputConvolvedSignal.Samples.Count - 1] == 0)
            {
                OutputConvolvedSignal.Samples.RemoveAt(OutputConvolvedSignal.Samples.Count - 1);
                OutputConvolvedSignal.SamplesIndices.RemoveAt(OutputConvolvedSignal.SamplesIndices.Count - 1);
            }

        }
    }
}

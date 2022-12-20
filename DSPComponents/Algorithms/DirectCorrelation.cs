using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            if (InputSignal2 == null)
                InputSignal2 = new Signal(
                    InputSignal1.Samples.ToList(),
                    InputSignal1.SamplesIndices.ToList(),
                    InputSignal1.Periodic
                );

            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();

            float dominator = getNormalizingDominator(InputSignal1.Samples, InputSignal2.Samples);

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                float r = getR(InputSignal1.Samples, InputSignal2.Samples);
                OutputNonNormalizedCorrelation.Add(r);
                OutputNormalizedCorrelation.Add(r / dominator);
                shiftRight(InputSignal2);
            }
        }

        private void shiftRight(Signal signal)
        {
            float[] newSamples = new float[signal.Samples.Count];

            if (signal.Periodic)
                newSamples[newSamples.Length - 1] = signal.Samples[0];
            else
                newSamples[newSamples.Length - 1] = 0;

            for (int i = 0; i < newSamples.Length - 1; i++)
                newSamples[i] = signal.Samples[i + 1];

            signal.Samples = newSamples.ToList();
        }

        private float getNormalizingDominator(List<float> signal1, List<float> signal2)
        {
            float sum1 = getSquaredSum(signal1);
            float sum2 = getSquaredSum(signal2);
            float result = (float)Math.Sqrt(sum1 * sum2) / signal1.Count;
            return result;
        }

        private float getSquaredSum(List<float> signal1)
        {
            float sum = 0;
            for (int i = 0; i < signal1.Count; i++)
            {
                sum += signal1[i] * signal1[i];
            }
            return sum;
        }

        private float getR(List<float> signal1, List<float> signal2)
        {
            float sum = 0;
            for (int i = 0; i < signal1.Count; i++)
            {
                sum += signal1[i] * signal2[i];
            }

            float corr = sum / signal1.Count;

            return corr;
        }
    }
}

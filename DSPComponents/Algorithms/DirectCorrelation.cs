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
            float sum1 = 0,
                sum2 = 0,
                normalize;
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                sum1 += (float)Math.Pow(InputSignal1.Samples[i], 2);

            if (InputSignal2 != null)
            {
                for (int i = 0; i < InputSignal2.Samples.Count; i++)
                    sum2 += (float)Math.Pow(InputSignal2.Samples[i], 2);

                if (InputSignal1.Samples.Count == InputSignal2.Samples.Count)
                    normalize = (float)Math.Sqrt(sum1 * sum2) / InputSignal1.Samples.Count;
                else
                    normalize =
                        (float)Math.Sqrt(sum1 * sum2)
                        / (InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1);
            }
            else
                normalize = sum1 / InputSignal1.Samples.Count;

            List<float> outputNonNormalized = new List<float>();
            List<float> outputNormalized = new List<float>();

            if (InputSignal2 == null)
            {
                int N = InputSignal1.Samples.Count;
                for (int j = 0; j < N; j++)
                {
                    double sum = 0;
                    if (!InputSignal1.Periodic)
                        for (int n = 0; n + j < N; n++)
                            sum += InputSignal1.Samples[n] * InputSignal1.Samples[n + j];
                    else
                        for (int n = 0; n < N; n++)
                            sum += InputSignal1.Samples[n] * InputSignal1.Samples[(n + j) % N];

                    outputNonNormalized.Add((float)sum / N);
                    outputNormalized.Add(outputNonNormalized[j] / normalize);
                }
            }
            else
            {
                int N = InputSignal2.Samples.Count + InputSignal1.Samples.Count - 1;
                normalize *= InputSignal2.Samples.Count;
                for (int j = 0; j < N; j++)
                {
                    double sum = 0;
                    if (!InputSignal1.Periodic && !InputSignal2.Periodic)
                        for (int n = 0; n + j < N; n++)
                            sum += InputSignal1.Samples[n] * InputSignal2.Samples[n + j];
                    else if (InputSignal1.Periodic && !InputSignal2.Periodic)
                        for (int n = 0; n < N; n++)
                            sum += InputSignal1.Samples[n] * InputSignal2.Samples[(n + j) % N];
                    else if (!InputSignal1.Periodic && InputSignal2.Periodic)
                        for (int n = 0; n + j < N; n++)
                            sum += InputSignal1.Samples[n] * InputSignal2.Samples[(n + j) % N];
                    else
                        for (int n = 0; n < N; n++)
                            sum += InputSignal1.Samples[n] * InputSignal2.Samples[(n + j) % N];

                    outputNonNormalized.Add((float)sum / N);
                    outputNormalized.Add(outputNonNormalized[j] / normalize);
                }
            }
            OutputNonNormalizedCorrelation = outputNonNormalized;
            OutputNormalizedCorrelation = outputNormalized;
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

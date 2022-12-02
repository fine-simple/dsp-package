using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class AlgorithmsUtilities
    {
        // Implement here any functionalities you need to share between Algorithm classes
        public static Dictionary<int, float> getSampleDict(List<int> samplesIndices, List<float> samples)
        {
            var result = new Dictionary<int, float>();
            for (int i = 0; i < samples.Count; i++)
            {
                int key = samplesIndices[i];
                float value = samples[i];
                result.Add(key, value);
            }
            return result;
        }

        public static List<Complex> timeToFrequencyDomain(List<float> freqSamples)
        {
            List<Complex> timeSamples = new List<Complex>();
            int N = freqSamples.Count;

            for (int k = 0; k < N; k++)
            {
                Complex sum = Complex.Zero;
                for (int n = 0; n < N; n++)
                {
                    double exp = -k * 2 * Math.PI * n / N;
                    sum += new Complex(Math.Cos(exp), Math.Sin(exp)) * freqSamples[n];
                }
                timeSamples.Add(sum);
            }
            return timeSamples;
        }

        public static List<float> frequencyToTimeDomain(List<Complex> freqSamples)
        {
            List<float> timeSamples = new List<float>();

            int N = freqSamples.Count;

            for (int k = 0; k < N; k++)
            {
                Complex sum = Complex.Zero;
                for (int n = 0; n < N; n++)
                {
                    double exp = k * 2 * Math.PI * n / N;
                    sum += new Complex(Math.Cos(exp), Math.Sin(exp)) * freqSamples[n];
                }
                timeSamples.Add(1.0f / N * Convert.ToSingle(sum.Real));
            }

            return timeSamples;
        }
    }
}
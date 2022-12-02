using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using static DSPAlgorithms.Algorithms.AlgorithmsUtilities;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            if(InputSignal2 == null){
                InputSignal2 = new Signal(InputSignal1.Samples.ToList(), InputSignal1.SamplesIndices.ToList(), false);
                InputSignal2.folded = InputSignal1.folded;
            }
            List<Complex> samples1 = timeToFrequencyDomain(InputSignal1.Samples);
            List<Complex> samples2 = timeToFrequencyDomain(InputSignal2.Samples);

            List<Complex> result = new List<Complex>();

            for (int i = 0; i < samples1.Count; i++)
            {
                result.Add(Complex.Multiply(Complex.Conjugate(samples1[i]), samples2[i]));
            }

            OutputNonNormalizedCorrelation = frequencyToTimeDomain(result);
            for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
            {
                OutputNonNormalizedCorrelation[i] /= OutputNonNormalizedCorrelation.Count;
            }

            OutputNormalizedCorrelation = new List<float>(OutputNonNormalizedCorrelation);

            float dominator = getNormalizingDominator(InputSignal1.Samples, InputSignal2.Samples);

            for (int i = 0; i < OutputNormalizedCorrelation.Count; i++)
            {
                OutputNormalizedCorrelation[i] = OutputNonNormalizedCorrelation[i] / dominator;
            }
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
    }
}
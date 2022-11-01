using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            List<Complex> dft = new List<Complex>();
            for (int i = 0; i < InputFreqDomainSignal.Frequencies.Count; i++)
            {
                double A = InputFreqDomainSignal.FrequenciesAmplitudes[i];
                double theta = InputFreqDomainSignal.FrequenciesPhaseShifts[i];
                dft.Add(new Complex(A* Math.Cos(theta), A* Math.Sin(theta)));
            }
            List<float> samples = frequencyToTimeDomain(dft);
            OutputTimeDomainSignal = new Signal(samples, false);
        }

        private List<float> frequencyToTimeDomain(List<Complex> freqSamples)
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
                timeSamples.Add(1.0f / N * Convert.ToSingle(sum.Magnitude));
            }

            return timeSamples;
        }
    }
}
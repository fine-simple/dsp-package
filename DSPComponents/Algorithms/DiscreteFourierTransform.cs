using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }
        
        public override void Run()
        {
            OutputFreqDomainSignal = new Signal(false, new List<float>(), new List<float>(), new List<float>());
            
            List<Complex> freqDomain =  timeToFrequencyDomain(InputTimeDomainSignal.Samples);
            
            for (int i = 0; i < freqDomain.Count; i++)
            {
                OutputFreqDomainSignal.Frequencies.Add(i);
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add(Convert.ToSingle(freqDomain[i].Magnitude));
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add(Convert.ToSingle(freqDomain[i].Phase));
            }
        }
        
        private List<Complex> timeToFrequencyDomain(List<float> freqSamples)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
using static DSPAlgorithms.Algorithms.AlgorithmsUtilities;

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
            
            List<Complex> freqDomain = timeToFrequencyDomain(InputTimeDomainSignal.Samples);
            
            for (int i = 0; i < freqDomain.Count; i++)
            {
                OutputFreqDomainSignal.Frequencies.Add(i);
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add(Convert.ToSingle(freqDomain[i].Magnitude));
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add(Convert.ToSingle(freqDomain[i].Phase));
            }
        }
    }
}
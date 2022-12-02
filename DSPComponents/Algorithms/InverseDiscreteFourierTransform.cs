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
    }
}
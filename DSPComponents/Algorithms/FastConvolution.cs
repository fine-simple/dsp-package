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
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            int length = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            appendZeros(InputSignal1, length - InputSignal1.Samples.Count);
            appendZeros(InputSignal2, length - InputSignal2.Samples.Count);
            List<Complex> samples1 = timeToFrequencyDomain(InputSignal1.Samples);
            List<Complex> samples2 = timeToFrequencyDomain(InputSignal2.Samples);
            List<Complex> result = new List<Complex>();
            for (int i = 0; i < samples1.Count; i++)
            {
                result.Add(samples1[i] * samples2[i]);
            }

            OutputConvolvedSignal = new Signal(frequencyToTimeDomain(result), false);
        }

        void appendZeros(Signal signal, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                signal.Samples.Add(0);
                signal.SamplesIndices.Add(signal.SamplesIndices[signal.SamplesIndices.Count - 1] + 1);
            }
        }
    }
}

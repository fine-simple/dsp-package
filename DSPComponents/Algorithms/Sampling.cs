﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
		{
			var lowFilter = initFilter();
            if(M == 0 && L != 0)
            {
                Signal output = sampleUp(InputSignal);
                lowFilter.InputTimeDomainSignal = output;
                lowFilter.Run();
                OutputSignal = lowFilter.OutputYn;
            }
            else if(M != 0 && L == 0)
            {
                lowFilter.InputTimeDomainSignal = InputSignal;
                lowFilter.Run();
                OutputSignal = sampleDown(lowFilter.OutputYn);
            }
            else if(M != 0 && L != 0)
            {
                Signal upSamples = sampleUp(InputSignal);
                lowFilter.InputTimeDomainSignal = upSamples;
                lowFilter.Run();
                OutputSignal = sampleDown(lowFilter.OutputYn);
            }
		}
        private Signal sampleUp(Signal input) {
            int N = L * input.Samples.Count + 1;
            List<float> samples = new List<float>(N);
            for (int i = 0; i < input.Samples.Count; i++)
            {
                samples.Add(input.Samples[i]);
                for (int j = 0; j < L - 1; j++)
                {
                    samples.Add(0);
                }
            }

            return new Signal(samples, input.Periodic);
        }

        private Signal sampleDown(Signal input) {
            int N = input.Samples.Count / M;
            List<float> samples = new List<float>(N);

            for (int i = 0; i < input.Samples.Count; i+= M)
            {
                samples.Add(input.Samples[i]);
            }

            return new Signal(samples, input.Periodic);
        }

		private static FIR initFilter()
		{
			var fir = new FIR();
			fir.InputFilterType = FILTER_TYPES.LOW;
			fir.InputFS = 8000;
			fir.InputStopBandAttenuation = 50;
			fir.InputCutOffFrequency = 1500;
            fir.InputTransitionBand = 500;
			return fir;
		}
	}

}
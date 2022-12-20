﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DSPAlgorithms.Algorithms
{
    public class PracticalTask2 : Algorithm
    {
        public String SignalPath { get; set; }
        public float Fs { get; set; }
        public float miniF { get; set; }
        public float maxF { get; set; }
        public float newFs { get; set; }
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            Signal InputSignal = LoadSignal(SignalPath);

            var fir = new FIR();
            fir.InputTimeDomainSignal = InputSignal;
            fir.InputStopBandAttenuation = 50; // From README
            fir.InputTransitionBand = 500; // From README
            fir.InputFilterType = FILTER_TYPES.BAND_PASS;
            fir.InputF1 = miniF;
            fir.InputF2 = maxF;
            fir.InputFS = Fs;
            fir.Run();
            Signal output = fir.OutputYn;
            // TODO: Save output to File

            if (newFs >= maxF * 2)
            {
                var sampling = new Sampling();
                sampling.InputSignal = output;
                sampling.L = L;
                sampling.M = M;
                sampling.Run();
                output = sampling.OutputSignal;
                // TODO: Save output to File
            }

            var dc = new DC_Component();
            dc.InputSignal = output;
            dc.Run();
            output = dc.OutputSignal;
            // TODO: Save output to File

            var normalizer = new Normalizer();
            normalizer.InputSignal = output;
            normalizer.InputMinRange = -1;
            normalizer.InputMaxRange = 1;
            normalizer.Run();
            output = normalizer.OutputNormalizedSignal;
            // TODO: Save output to File
            
            var dft = new DiscreteFourierTransform();
            dft.InputTimeDomainSignal = output;
            dft.InputSamplingFrequency = Fs;
            dft.Run();
            output = dft.OutputFreqDomainSignal;
            // TODO: Save output to File
            
            OutputFreqDomainSignal = output;
        }

        public Signal LoadSignal(string filePath)
        {
            Stream stream = File.Open(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite
            );
            var sr = new StreamReader(stream);

            var sigType = byte.Parse(sr.ReadLine());
            var isPeriodic = byte.Parse(sr.ReadLine());
            long N1 = long.Parse(sr.ReadLine());

            List<float> SigSamples = new List<float>(unchecked((int)N1));
            List<int> SigIndices = new List<int>(unchecked((int)N1));
            List<float> SigFreq = new List<float>(unchecked((int)N1));
            List<float> SigFreqAmp = new List<float>(unchecked((int)N1));
            List<float> SigPhaseShift = new List<float>(unchecked((int)N1));

            if (sigType == 1)
            {
                SigSamples = null;
                SigIndices = null;
            }

            for (int i = 0; i < N1; i++)
            {
                if (sigType == 0 || sigType == 2)
                {
                    var timeIndex_SampleAmplitude = sr.ReadLine().Split();
                    SigIndices.Add(int.Parse(timeIndex_SampleAmplitude[0]));
                    SigSamples.Add(float.Parse(timeIndex_SampleAmplitude[1]));
                }
                else
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            if (!sr.EndOfStream)
            {
                long N2 = long.Parse(sr.ReadLine());

                for (int i = 0; i < N2; i++)
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            stream.Close();
            return new Signal(
                SigSamples,
                SigIndices,
                isPeriodic == 1,
                SigFreq,
                SigFreqAmp,
                SigPhaseShift
            );
        }
    }
}

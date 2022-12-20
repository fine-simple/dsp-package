using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        private WINDOW_FUNCTION windowFunction { get; set; }
        private int N { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            initFc();
            initWindowFn();
            N = getN();

            OutputHn = new Signal(new List<float>(N), false);
            for (int i = -N / 2; i <= N / 2; i++)
            {
                float h = hDn(i);
                float w = wn(i);
                OutputHn.Samples.Add(h * w);
                OutputHn.SamplesIndices.Add(i);
            }

            DirectConvolution convolution = new DirectConvolution();

            convolution.InputSignal1 = InputTimeDomainSignal;
            convolution.InputSignal2 = OutputHn;
            convolution.Run();

            OutputYn = convolution.OutputConvolvedSignal;
        }

        private void initWindowFn()
        {
            if (InputStopBandAttenuation <= 21)
                windowFunction = WINDOW_FUNCTION.RECTANGULAR;
            else if (InputStopBandAttenuation <= 44)
                windowFunction = WINDOW_FUNCTION.HANNING;
            else if (InputStopBandAttenuation <= 53)
                windowFunction = WINDOW_FUNCTION.HAMMING;
            else
                windowFunction = WINDOW_FUNCTION.BLACKMAN;
        }

        private int getN()
        {
            float x = 0;
            switch (windowFunction)
            {
                case WINDOW_FUNCTION.RECTANGULAR:
                    x = 0.9f;
                    break;
                case WINDOW_FUNCTION.HANNING:
                    x = 3.1f;
                    break;
                case WINDOW_FUNCTION.HAMMING:
                    x = 3.3f;
                    break;
                case WINDOW_FUNCTION.BLACKMAN:
                    x = 5.5f;
                    break;
            }
            float N = x * (float)InputFS / InputTransitionBand;
            if ((int)N % 2 == 0)
                return (int)N + 1;
            else if (Math.Floor(N) < N)
                return (int)N + 2;
            else
                return (int)N;
        }

        private void initFc()
        {
            switch (InputFilterType)
            {
                case FILTER_TYPES.LOW:
                    InputCutOffFrequency = InputCutOffFrequency + InputTransitionBand / 2;
                    InputCutOffFrequency /= InputFS;
                    break;
                case FILTER_TYPES.HIGH:
                    InputCutOffFrequency = InputCutOffFrequency - InputTransitionBand / 2;
                    InputCutOffFrequency /= InputFS;
                    break;
                case FILTER_TYPES.BAND_PASS:
                    InputF1 = InputF1 - InputTransitionBand / 2;
                    InputF1 /= InputFS;
                    InputF2 = InputF2 + InputTransitionBand / 2;
                    InputF2 /= InputFS;
                    break;
                case FILTER_TYPES.BAND_STOP:
                    InputF1 = InputF1 + InputTransitionBand / 2;
                    InputF1 /= InputFS;
                    InputF2 = InputF2 - InputTransitionBand / 2;
                    InputF2 /= InputFS;
                    break;
            }
        }

        private float wn(int n)
        {
            switch (windowFunction)
            {
                case WINDOW_FUNCTION.RECTANGULAR:
                    return 1;
                case WINDOW_FUNCTION.HANNING:
                    return (float)(0.5 + 0.5 * Math.Cos(2 * Math.PI * n / N));
                case WINDOW_FUNCTION.HAMMING:
                    return (float)(0.54 + 0.46 * Math.Cos(2 * Math.PI * n / N));
                case WINDOW_FUNCTION.BLACKMAN:
                    return (float)(
                        0.42
                        + 0.5 * Math.Cos(2 * Math.PI * n / (N - 1))
                        + 0.08 * Math.Cos(4 * Math.PI * n / (N - 1))
                    );
                default:
                    return 0;
            }
        }

        private float hDn(int n)
        {
            if (n == 0)
                switch (InputFilterType)
                {
                    case FILTER_TYPES.LOW:
                        return 2 * (float)InputCutOffFrequency;
                    case FILTER_TYPES.HIGH:
                        return 1 - 2 * (float)InputCutOffFrequency;
                    case FILTER_TYPES.BAND_PASS:
                        return 2 * (float)(InputF2 - InputF1);
                    case FILTER_TYPES.BAND_STOP:
                        return 1 - 2 * (float)(InputF2 - InputF1);
                    default:
                        return 0;
                }
            else
                switch (InputFilterType)
                {
                    case FILTER_TYPES.LOW:
                    {
                        float wc = (float)(2 * Math.PI * InputCutOffFrequency);
                        return (float)(2 * InputCutOffFrequency * Math.Sin(n * wc) / (n * wc));
                    }
                    case FILTER_TYPES.HIGH:
                    {
                        float wc = (float)(2 * Math.PI * InputCutOffFrequency);
                        return (float)(-2 * InputCutOffFrequency * Math.Sin(n * wc) / (n * wc));
                    }
                    case FILTER_TYPES.BAND_PASS:
                    {
                        float wc1 = (float)(2 * Math.PI * InputF1);
                        float wc2 = (float)(2 * Math.PI * InputF2);
                        return (float)(
                            (2 * InputF2 * Math.Sin(n * wc2) / (n * wc2))
                            - (2 * InputF1 * Math.Sin(n * wc1) / (n * wc1))
                        );
                    }
                    case FILTER_TYPES.BAND_STOP:
                    {
                        float wc1 = (float)(2 * Math.PI * InputF1);
                        float wc2 = (float)(2 * Math.PI * InputF2);
                        return (float)(
                            (2 * InputF1 * Math.Sin(n * wc1) / (n * wc1))
                            - (2 * InputF2 * Math.Sin(n * wc2) / (n * wc2))
                        );
                    }
                    default:
                        return 0;
                }
        }
    }
}

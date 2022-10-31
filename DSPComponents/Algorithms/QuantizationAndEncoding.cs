using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }
        
        private void init()
        {
            OutputEncodedSignal = new List<string>();
            OutputIntervalIndices = new List<int>();
            OutputQuantizedSignal = new Signal(new List<float>(), false);
            OutputSamplesError = new List<float>();
        }

        public override void Run()
        {
            init();

            // assign missing of inputLevels or bits
            if(InputLevel > 0)
                InputNumBits = ((int)Math.Log2(InputLevel));
            else
                InputLevel = ((int)Math.Pow(2, InputNumBits));
            
            // calculate delta
            List<float> samples = InputSignal.Samples;
            float maxAmp = samples.Max();
            float minAmp = samples.Min();

            float delta = (maxAmp - minAmp) / InputLevel;

            // calculate intervals
            float[] intervals = new float[InputLevel + 1];
            for (int i = 0; i < intervals.Length; i++)
            {
                intervals[i] = minAmp + delta * i;
            }
            
            for (int i = 0; i < samples.Count; i++)
            {
                // get interval index
                int intervalIdx = getIntervalIndex(samples[i], intervals);
                OutputIntervalIndices.Add(intervalIdx + 1);

                // encoded values
                string binary = Convert.ToString(intervalIdx, 2);
                while(binary.Length < InputNumBits)
                    binary = "0" + binary;
                OutputEncodedSignal.Add(binary);

                // assign new signal values
                float mean = (intervals[intervalIdx] + intervals[intervalIdx + 1]) / 2;
                OutputQuantizedSignal.Samples.Add(mean);

                // assign error per sample
                OutputSamplesError.Add(OutputQuantizedSignal.Samples[i] - samples[i]);
            }
        }

        private int getIntervalIndex(float sample, float[] intervals)
        {
            int start = 0, end = intervals.Length - 1;
            int mid = (start + end) / 2;

            while(start < end)
            {
                mid = (start + end) / 2;
                if(sample > intervals[mid])
                {
                    if(sample <= intervals[mid+1])
                        return mid;
                    else
                        start = mid + 1;
                }
                else
                {
                    end = mid;
                }
            }
            return mid;
        }
    }
}
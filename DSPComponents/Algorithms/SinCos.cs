using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos : Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            samples = new List<float>();
            if (SamplingFrequency >= 2 * AnalogFrequency)
            {
                var _2_pi_f = 2 * Math.PI * (AnalogFrequency / SamplingFrequency);
                if (type == "sin")
                {
                    for (int i = 0; i < SamplingFrequency; i++)
                        samples.Add((float)(A * Math.Sin((_2_pi_f * i) + PhaseShift)));
                }
                else if (type == "cos")
                {
                    for (int i = 0; i < SamplingFrequency; i++)
                        samples.Add((float)(A * Math.Cos((_2_pi_f * i) + PhaseShift)));
                }
            }
        }
    }
}

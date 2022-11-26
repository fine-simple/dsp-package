using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            DirectCorrelation corr = new DirectCorrelation();
            corr.InputSignal1 = InputSignal1;
            corr.InputSignal2 = InputSignal2;
            corr.Run();

            int maxIndex = 0;
            float max = 0;
            for (int i = 0; i < corr.OutputNormalizedCorrelation.Count; i++)
            {
                float curr = Math.Abs(corr.OutputNormalizedCorrelation[i]);
                if (curr > max)
                {
                    max = curr;
                    maxIndex = i;
                }
            }

            OutputTimeDelay = maxIndex * InputSamplingPeriod;

        }
    }
}

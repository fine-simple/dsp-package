using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> values = new List<float>(InputSignals.Last().Samples.Count);
            for (int i = 0; i < InputSignals.Count; i++)
            {
                for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                {
                    if (values.Count - 1 < j)
                        values.Add(0);
                    values[j] += InputSignals[i].Samples[j];
                }
            }

            OutputSignal = new Signal(values, false);
        }
    }
}
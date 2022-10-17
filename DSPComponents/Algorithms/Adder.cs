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
            // get max number of elements
            int max = 0;
            for (int i = 0; i < InputSignals.Count; i++)
            {
                if (InputSignals[i].Samples.Count > max)
                    max = InputSignals[i].Samples.Count;
            }

            float[] values = new float[max];
            for (int i = 0; i < InputSignals.Count; i++)
            {
                for (int j = 0; j < InputSignals[i].Samples.Count; j++)
                {
                    values[j] += InputSignals[i].Samples[j];
                }
            }

            OutputSignal = new Signal(values.ToList(), false);
        }
    }
}
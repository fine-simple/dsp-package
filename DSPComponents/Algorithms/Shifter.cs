using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        // TODO: Task 3
        // FIXME: When Folded the sign of shift value is reversed
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            OutputShiftedSignal = new Signal(InputSignal.Samples.ToList(), false);
            
            var samplesN = OutputShiftedSignal.SamplesIndices;

            for (int i = 0; i < samplesN.Count; i++)
                samplesN[i] = InputSignal.SamplesIndices[i] - ShiftingValue;
        }
    }
}
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
        // TODO: Task 3 [Completed]
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            OutputShiftedSignal = new Signal(InputSignal.Samples.ToList(), InputSignal.SamplesIndices.ToList(), InputSignal.Periodic);
            
            var samplesN = OutputShiftedSignal.SamplesIndices;

            int sign = InputSignal.Periodic ? 1 : -1;
        
            for (int i = 0; i < samplesN.Count; i++)
                samplesN[i] += sign * ShiftingValue;
        }
    }
}
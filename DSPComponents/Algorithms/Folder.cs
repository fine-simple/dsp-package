using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        // TODO: Task 3 [Completed]
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            OutputFoldedSignal = new Signal(InputSignal.Samples.ToList(), InputSignal.SamplesIndices.ToList(), !InputSignal.Periodic);
            
            for (int i = 0; i < OutputFoldedSignal.SamplesIndices.Count; i++)
            {
                OutputFoldedSignal.SamplesIndices[i] *= -1; 
            }
            
            OutputFoldedSignal.Samples.Reverse();
            OutputFoldedSignal.SamplesIndices.Reverse();
        }
    }
}
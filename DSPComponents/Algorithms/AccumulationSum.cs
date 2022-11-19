using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        // Task 3 [Completed]
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);

            int accoumulator = 0;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                accoumulator += (int)InputSignal.Samples[i];

                OutputSignal.Samples.Add(accoumulator);
                OutputSignal.SamplesIndices.Add(InputSignal.SamplesIndices[i]);
            }
        }
    }
}

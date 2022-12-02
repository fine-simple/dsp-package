using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> dct = new List<float>();
            double N = InputSignal.Samples.Count;
            for (double k = 0; k < N; k++)
            {
                double sum = 0;
                for (double n = 0; n < N; n++)
                {
                    sum += InputSignal.Samples[(int)n] * Math.Cos(Math.PI * (2 * n - 1) * (2 * k - 1) / N / 4);
                }
                sum *= Math.Sqrt(2 / N);
                dct.Add((float)sum);
            }
            OutputSignal = new Signal(dct, false);
        }
    }
}

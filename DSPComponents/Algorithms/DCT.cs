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
            int N = InputSignal.Samples.Count;
            for (int k = 0; k < N; k++)
            {
                float sum = 0;
                for (int n = 0; n < N; n++)
                {
                    sum += InputSignal.Samples[n] * (float)Math.Cos(Math.PI * (2 * n - 1) * (2 * k - 1) / N / 4);
                }
                sum *= (float)Math.Sqrt(2f / N);
                dct.Add(sum);
            }
            OutputSignal = new Signal(dct, false);
        }
    }
}

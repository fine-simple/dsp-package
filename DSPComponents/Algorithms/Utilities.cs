using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class AlgorithmsUtilities
    {
        // Implement here any functionalities you need to share between Algorithm classes
        public static Dictionary<int, float> getSampleDict(List<int> samplesIndices, List<float> samples)
        {
            var result = new Dictionary<int, float>();
            for (int i = 0; i < samples.Count; i++)
            {
                int key = samplesIndices[i];
                float value = samples[i];
                result.Add(key, value);
            }
            return result;
        }
    }
}
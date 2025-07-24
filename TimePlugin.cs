using Microsoft.SemanticKernel;

namespace Semantic_Kernel_With_Ollama_Test
{
    public class TimePlugin
    {
        [KernelFunction]
        public string HoraAtual()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}